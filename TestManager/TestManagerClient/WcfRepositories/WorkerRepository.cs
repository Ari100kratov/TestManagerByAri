using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using TestManagerClient.WcfServiceReference;

namespace TestManagerClient.WcfRepositories
{
    /// <summary>
    /// Доступ к операциям WCF связанные с сотрудником
    /// </summary>
    public class WorkerRepository
    {
        private ServiceClient _service = new ServiceClient();

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        public void Add(Worker worker)
        {
            worker.Id = this._service.AddWorker(worker);
        }

        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        public void Edit(Worker worker)
        {
            this._service.EditWorker(worker);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Код сотрудника</param>
        public void Delete(int id)
        {
            this._service.DeleteWorker(id);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        public void Delete(Worker worker)
        {
            this._service.DeleteWorker(worker.Id);
        }

        /// <summary>
        /// Получение всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        public List<Worker> GetList()
        {
            return this._service.GetAllWorkers().ToList();
        }

        /// <summary>
        /// Получение сотрудника
        /// </summary>
        /// <param name="id">Код сотрудника</param>
        /// <returns>Сотрудник</returns>
        public Worker GetWorker(int id)
        {
            return this._service.GetWorker(id);
        }

        public Worker FirstOrDefault(Expression<Func<Worker, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentException();

            return this._service.GetAllWorkers().AsQueryable().FirstOrDefault(filter);
        }

        public List<Worker> Where(Expression<Func<Worker, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentException();

            return this._service.GetAllWorkers().AsQueryable().Where(filter).ToList();
        }

        public Worker SingleOrDefault(Expression<Func<Worker, bool>> filter)
        {
            if (filter == null)
                throw new ArgumentException();

            return this._service.GetAllWorkers().AsQueryable().SingleOrDefault(filter);
        }
    }
}
