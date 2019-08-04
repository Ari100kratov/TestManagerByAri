using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TMAttributes;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Базовый репозиторий для работы с базой данных
    /// </summary>
    /// <typeparam name="T">Сущность базы данных</typeparam>
    internal class BaseRepository<T> where T : IKeyedModel, new()
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
        private protected string GetTableName()
        {
            return this._tableName;
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

             return Convert.ToInt32(this.SaveChanges(cmd));
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


