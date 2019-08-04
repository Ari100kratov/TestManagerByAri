using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using TMAttributes;

namespace DataAccessLayer
{
    /// <summary>
    /// Базовый репозиторий с кэшированием данных
    /// </summary>
    /// <typeparam name="T">Сущность базы данных</typeparam>
    internal class CacheBaseRepository<T> : BaseRepository<T> where T : IKeyedModel, new()
    {
        private List<T> _listOfEntities = new List<T>();

        public CacheBaseRepository() : base()
        {
            this.FillList();
        }

        /// <summary>
        /// Проверка целостности базы данных
        /// </summary>
        /// <param name="dataTable">Таблица базы данных</param>
        /// <param name="properties">Класс модели данных</param>
        /// <returns></returns>
        private bool IsIntegrityOfDatabase(DataTable dataTable, List<PropertyInfo> properties)
        {
            foreach (var property in properties)
            {
                if (!dataTable.Columns.Contains(property.Name))
                    return false;
            }

            foreach (DataColumn column in dataTable.Columns)
            {
                if (!properties.Select(x => x.Name).Contains(column.ColumnName))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Получить список сущностей
        /// </summary>
        /// <returns></returns>
        internal virtual List<T> GetList()
        {
            return this._listOfEntities;
        }

        /// <summary>
        /// Получить сущность
        /// </summary>
        /// <param name="id">Код сущности</param>
        /// <returns></returns>
        internal virtual T GetItem(int id)
        {
            return this._listOfEntities.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Заполнение из базы данных списка сущностей 
        /// </summary>
        private void FillList()
        {
            var sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM [{this.GetTableName()}]", DataManager.ConnectionString);
            var dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            var properties = typeof(T).GetProperties().Where(x => x.IsDefined(typeof(ColumnDB), false)).ToList();

            if (!this.IsIntegrityOfDatabase(dataTable, properties))
                throw new Exception();

            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();
                foreach (var property in properties)
                {
                    var fieldName = property.Name;
                    var fieldValue = row[fieldName] == DBNull.Value ? null : row[fieldName];
                    property.SetValue(item, fieldValue);
                }
                this._listOfEntities.Add(item);
            }
        }

        /// <summary>
        /// Добавление сущности в базу данных и кэширование
        /// </summary>
        /// <param name="item">Сущность</param>
        /// <returns>Код сущности</returns>
        internal override int Add(T item)
        {
            var lastId = base.Add(item);
            item.Id = lastId;
            this._listOfEntities.Add(item);
            return lastId;
        }

        /// <summary>
        /// Удаление сущности и удаление из кэша
        /// </summary>
        /// <param name="id">Код сущности</param>
        internal override void Delete(int id)
        {
            base.Delete(id);

            var deletedItem = this._listOfEntities.FirstOrDefault(x => x.Id == id);
            if (deletedItem != null)
                this._listOfEntities.Remove(deletedItem);
        }

        /// <summary>
        /// Удаление сущности и удаление из кэша
        /// </summary>
        /// <param name="item">Сущность</param>
        internal override void Delete(T item)
        {
            base.Delete(item);

            var deletedItem = this._listOfEntities.FirstOrDefault(x => x.Id == item.Id);
            if (deletedItem != null)
                this._listOfEntities.Remove(deletedItem);
        }

        /// <summary>
        /// Изменение сущности и в кэше
        /// </summary>
        /// <param name="item">Сущность</param>
        internal override void Update(T item)
        {
            base.Update(item);

            var editedItem = this._listOfEntities.FirstOrDefault(x => x.Id == item.Id);
            var index = this._listOfEntities.IndexOf(editedItem);

            if (index == -1)
                return;

            this._listOfEntities[index] = item;
        }
    }
}
