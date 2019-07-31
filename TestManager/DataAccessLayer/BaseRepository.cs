using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAttributes;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace DataAccessLayer
{
    /// <summary>
    /// Базовый репозиторий для работы с базой данных
    /// </summary>
    /// <typeparam name="T">Сущность базы данных</typeparam>
    internal class BaseRepository<T> where T : IKeyedModel, new()
    {
        private readonly string _tableName;
        private List<T> _listOfEntities = new List<T>();

        public BaseRepository()
        {
            this._tableName = typeof(T).Name;
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
            return this._listOfEntities.First(x => x.Id == id);
        }

        /// <summary>
        /// Заполнение из базы данных списка сущностей 
        /// </summary>
        private void FillList()
        {
            var sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM [{this._tableName}]", DataManager.ConnectionString);
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
        /// Добавление сущности в базу данных
        /// </summary>
        /// <param name="item">Сущность</param>
        /// <returns>Код сущности</returns>
        internal virtual int Add(T item)
        {
            var fieldList = new List<string>();
            var valueList = new List<object>();

            var type = item.GetType();
            var properties = type.GetProperties().Where(x => !x.IsDefined(typeof(AutoIncrementDB), false) && x.IsDefined(typeof(ColumnDB), false)).ToList();

            foreach (var property in properties)
            {
                var value = property.GetValue(item);
                if (value == null)
                    continue;

                fieldList.Add(property.Name);
                valueList.Add(value);
            }

            var cmdText = $"INSERT INTO [{this._tableName}]({string.Join(",", fieldList)}) "
                + $"VALUES(@{ string.Join(", @", fieldList)}) ;  SELECT SCOPE_IDENTITY();";

            var cmd = new SqlCommand();
            cmd.CommandText = cmdText;

            for (var i = 0; i < fieldList.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@{fieldList[i]}", valueList[i]);
            }

            int lastId = Convert.ToInt32(this.SaveChanges(cmd));
            item.Id = lastId;
            this._listOfEntities.Add(item);
            return lastId;
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">Код сущности</param>
        internal virtual void Delete(int id)
        {
            var cmd = new SqlCommand($"DELETE FROM [{this._tableName}] WHERE Id = @Id");
            cmd.Parameters.AddWithValue("@Id", id);

            this.SaveChanges(cmd);

            var foundItem = this._listOfEntities.FirstOrDefault(x => x.Id == id);
            if (foundItem != null)
                this._listOfEntities.Remove(foundItem);
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="item">Сущность</param>
        internal virtual void Delete(T item)
        {
            var cmd = new SqlCommand($"DELETE FROM [{this._tableName}] WHERE Id = @Id");
            cmd.Parameters.AddWithValue("@Id", item.Id);

            this.SaveChanges(cmd);

            var foundItem = this._listOfEntities.FirstOrDefault(x => x.Id == item.Id);
            if (foundItem != null)
                this._listOfEntities.Remove(foundItem);
        }

        /// <summary>
        /// Изменение сущности
        /// </summary>
        /// <param name="item">Сущность</param>
        internal virtual void Update(T item)
        {
            var fieldList = new List<string>();
            var valueList = new List<object>();
            var updateQuery = new List<string>();

            var type = item.GetType();
            var properties = type.GetProperties().Where(x => !x.IsDefined(typeof(AutoIncrementDB), false) && x.IsDefined(typeof(ColumnDB), false)).ToList();

            foreach (var property in properties)
            {
                var value = property.GetValue(item) ?? DBNull.Value;
                fieldList.Add(property.Name);
                valueList.Add(value);

                updateQuery.Add($"{property.Name} = @{property.Name}");
            }

            var cmdText = $"UPDATE [{this._tableName}] SET {string.Join(",", updateQuery)} WHERE Id = @Id";
            var cmd = new SqlCommand();
            cmd.CommandText = cmdText;

            for (var i = 0; i < fieldList.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@{fieldList[i]}", valueList[i]);
            }
            cmd.Parameters.AddWithValue("@Id", item.Id);

            this.SaveChanges(cmd);

            var foundItem = this._listOfEntities.FirstOrDefault(x => x.Id == item.Id);
            var index = this._listOfEntities.IndexOf(foundItem);
            if (index == -1)
                return;

            this._listOfEntities[index] = item;
        }

        /// <summary>
        /// Сохранение результата в базу данных
        /// </summary>
        /// <param name="cmd">Обьект SqlCommand</param>
        /// <returns></returns>
        private object SaveChanges(SqlCommand cmd)
        {
            using (var con = new SqlConnection(DataManager.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                return cmd.ExecuteScalar();
            }
        }
    }


}


