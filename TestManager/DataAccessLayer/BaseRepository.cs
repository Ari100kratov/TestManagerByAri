using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class BaseRepository<T> where T : IKeyedModel, new()
    {
        private readonly string _tableName;
        
        public BaseRepository()
        {
            this._tableName = typeof(T).Name;
        }

        /// <summary>
        /// Возвращает наименование таблицы
        /// </summary>
        /// <returns></returns>
        public string GetTableName()
        {
            return this._tableName;
        }

        /// <summary>
        /// Проверка целостности базы данных
        /// </summary>
        /// <param name="dataTable">Таблица базы данных</param>
        /// <param name="properties">Класс модели данных</param>
        /// <returns></returns>
        private bool IsIntegrityOfDatabase(DataTable dataTable, List<PropertyInfo> properties)
        {
            if (dataTable.Columns.Count != properties.Count())
                return false;

            foreach (var property in properties)
            {
                if (!dataTable.Columns.Contains(property.Name))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Заполнение из базы данных списка сущностей 
        /// </summary>
        public virtual List<T> GetList()
        {
            var sqlDataAdapter = new SqlDataAdapter($"SELECT * FROM [{this.GetTableName()}]", DataManager.ConnectionString);
            var dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            var properties = typeof(T).GetProperties().Where(x => x.IsDefined(typeof(ColumnDB), false)).ToList();

            if (!this.IsIntegrityOfDatabase(dataTable, properties))
                throw new Exception();

            var listEntities = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();
                foreach (var property in properties)
                {
                    var fieldName = property.Name;
                    var fieldValue = row[fieldName] == DBNull.Value ? null : row[fieldName];
                    property.SetValue(item, fieldValue);
                }
                listEntities.Add(item);
            }

            return listEntities;
        }

        /// <summary>
        /// Добавление сущности в базу данных
        /// </summary>
        /// <param name="item">Сущность</param>
        /// <returns>Код сущности</returns>
        public virtual int Add(T item)
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

             return Convert.ToInt32(this.SaveChanges(cmd));
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">Код сущности</param>
        public virtual void Delete(int id)
        {
            var cmd = new SqlCommand($"DELETE FROM [{this._tableName}] WHERE Id = @Id");
            cmd.Parameters.AddWithValue("@Id", id);
            this.SaveChanges(cmd);
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="item">Сущность</param>
        public virtual void Delete(T item)
        {
            this.Delete(item.Id);
        }

        /// <summary>
        /// Изменение сущности
        /// </summary>
        /// <param name="item">Сущность</param>
        public virtual void Update(T item)
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


