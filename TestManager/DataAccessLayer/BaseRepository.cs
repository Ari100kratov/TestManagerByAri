using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMAttributes;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    internal class BaseRepository<T> where T : IKeyedModel, new()
    {
        private readonly string tableName;
        private List<T> listOfEntities = new List<T>();

        public BaseRepository()
        {
            this.tableName = typeof(T).Name;
        }

        internal virtual List<T> GetList()
        {
            return this.listOfEntities;
        }

        internal virtual T GetItem(int id)
        {
            return this.listOfEntities.First(x=>x.Id == id);
        }

        private void FillList()
        {
            var da = new SqlDataAdapter($"SELECT * FROM [{this.tableName}]", Properties.Settings.Default.ConnectionString);
            var dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                var item = new T();
                var properties = typeof(T).GetProperties();

                foreach (var property in properties)
                {
                    //Проверяем содержится ли в базе данных такое поле
                    if (!dt.Columns.Contains(property.Name))
                        continue;

                    var fieldName = property.Name;
                    var fieldValue = row[fieldName] == DBNull.Value ? null : row[fieldName];
                    property.SetValue(item, fieldValue);
                }

                this.listOfEntities.Add(item);
            }
        }

        internal virtual int Add(T item)
        {
            var insertQuery = $"INSERT INTO [{this.tableName}](";
            var valuesQuery = $" VALUES (";
            var fieldList = new List<string>();
            var valueList = new List<object>();

            var type = item.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                //Проверяем существует ли такое поле в базе данных и оно не автоинкрементно
                if (System.Attribute.IsDefined(property, typeof(AutoIncrementDB))
                     || !System.Attribute.IsDefined(property, typeof(ColumnDB)))
                    continue;

                //Проверяем существуют ли данные для добавления
                var value = property.GetValue(item);
                if (value == null)
                    continue;

                fieldList.Add(property.Name);
                valueList.Add(value);

                insertQuery += $"{property.Name}, ";
                valuesQuery += $"@{property.Name}, ";
            }

            insertQuery = insertQuery.Remove(insertQuery.Length - 2, 1) + ")";
            valuesQuery = valuesQuery.Remove(valuesQuery.Length - 2, 1) + ")";

            var finalQuery = insertQuery + valuesQuery;
            var cmd = new SqlCommand();
            cmd.CommandText = $"{finalQuery} ; SELECT SCOPE_IDENTITY();";

            for (var i = 0; i < fieldList.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@{fieldList[i]}", valueList[i]);
            }

            //Получаем и сохраняем ID добавленной записи
            int lastId = Convert.ToInt32(this.SaveChanges(cmd));
            item.Id = lastId;
            this.listOfEntities.Add(item);
            return lastId;
        }

        internal virtual void Delete(int id)
        {
            var cmd = new SqlCommand($"DELETE FROM {this.tableName} WHERE Id = @Id");
            cmd.Parameters.AddWithValue("@Id", id);

            this.SaveChanges(cmd);

            var foundItem = this.listOfEntities.First(x => x.Id == id);
            if (foundItem != null)
                this.listOfEntities.Remove(foundItem);
        }

        internal virtual void Delete(T item)
        {
            var cmd = new SqlCommand($"DELETE FROM [{this.tableName}] WHERE Id = @Id");
            cmd.Parameters.AddWithValue("@Id", item.Id);

            this.SaveChanges(cmd);

            var foundItem = this.listOfEntities.FirstOrDefault(x => x.Id == item.Id);
            if (foundItem != null)
                this.listOfEntities.Remove(foundItem);
        }

        internal virtual void Update(T item)
        {
            var updateQuery = $"UPDATE [{this.tableName}] SET ";
            var fieldList = new List<string>();
            var valueList = new List<object>();

            var type = item.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                //Проверяем существует ли такое поле в базе данных и оно не автоинкрементно
                if (System.Attribute.IsDefined(property, typeof(AutoIncrementDB))
                     || !System.Attribute.IsDefined(property, typeof(ColumnDB)))
                    continue;

                var value = property.GetValue(item) ?? DBNull.Value;

                fieldList.Add(property.Name);
                valueList.Add(value);

                updateQuery += $"{property.Name} = @{property.Name}, ";

            }

            updateQuery = updateQuery.Remove(updateQuery.Length - 2, 1);

            var cmd = new SqlCommand();
            cmd.CommandText = $"{updateQuery} WHERE Id = @Id;";

            for (var i = 0; i < fieldList.Count; i++)
            {
                cmd.Parameters.AddWithValue($"@{fieldList[i]}", valueList[i]);
            }
            cmd.Parameters.AddWithValue("@Id", item.Id);

            this.SaveChanges(cmd);

            var foundItem = this.listOfEntities.FirstOrDefault(x => x.Id == item.Id);
            var index = this.listOfEntities.IndexOf(foundItem);
            if (index == -1)
                return;

            this.listOfEntities[index] = item;
        }


        private object SaveChanges(SqlCommand cmd)
        {
            using (var con = new SqlConnection(Properties.Settings.Default.ConnectionString))
            {
                cmd.Connection = con;
                con.Open();
                return cmd.ExecuteScalar();
            }
        }
    }


}


