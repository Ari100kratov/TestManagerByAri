using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    /// <summary>
    /// Базовый репозиторий с кэшированием данных
    /// </summary>
    /// <typeparam name="T">Сущность базы данных</typeparam>
    public class CacheBaseRepository<T> : BaseRepository<T> where T : IKeyedModel, new()
    {
        private List<T> _listOfEntities = new List<T>();

        public CacheBaseRepository() : base()
        {
            this.FillList();
        }

        private void FillList()
        {
            this._listOfEntities = base.GetList();
        }

        /// <summary>
        /// Получить список сущностей
        /// </summary>
        /// <returns></returns>
        public override List<T> GetList()
        {
            return this._listOfEntities;
        }

        /// <summary>
        /// Получить сущность
        /// </summary>
        /// <param name="id">Код сущности</param>
        /// <returns></returns>
        public virtual T GetItem(int id)
        {
            return this._listOfEntities.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Добавление сущности в базу данных и кэширование
        /// </summary>
        /// <param name="item">Сущность</param>
        /// <returns>Код сущности</returns>
        public override int Add(T item)
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
        public override void Delete(int id)
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
        public override void Delete(T item)
        {
            this.Delete(item.Id);
        }

        /// <summary>
        /// Изменение сущности и в кэше
        /// </summary>
        /// <param name="item">Сущность</param>
        public override void Update(T item)
        {
            base.Update(item);

            var editedItem = this._listOfEntities.FirstOrDefault(x => x.Id == item.Id);
            var indexOfEditedItem = this._listOfEntities.IndexOf(editedItem);

            if (indexOfEditedItem == -1)
                return;

            this._listOfEntities[indexOfEditedItem] = item;
        }
    }
}
