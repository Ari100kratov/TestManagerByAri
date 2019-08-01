using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace TMWcfService
{
    /// <summary>
    /// Реализация интерфейса-контракта WCF службы
    /// </summary>
    internal class Service : IService
    {
        private DataManager Dm => DataManager.Instance;

        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        /// <returns>Код добавленного сотрудника</returns>
        public void AddWorker(Worker worker)
        {
            try
            {
                worker.Id =  Dm.Worker.Add(worker);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        public void EditWorker(Worker worker)
        {
            try
            {
                Dm.Worker.Update(worker);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Код сотрудника</param>
        public void DeleteWorker(int id)
        {
            try
            {
                Dm.Worker.Delete(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Добавление подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        /// <returns>Код добавленного подразделения</returns>
        public void AddDepartment(Department department)
        {
            try
            {
                department.Id = Dm.Department.Add(department);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Изменение подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        public void EditDepartment(Department department)
        {
            try
            {
                Dm.Department.Update(department);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="id">Код подрзделения</param>
        public void DeleteDepartment(int id)
        {
            try
            {
                Dm.Department.Delete(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Получение всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        public List<Worker> GetAllWorkers()
        {
            try
            {
                return Dm.Worker.GetList();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Получение сотрудника
        /// </summary>
        /// <param name="id">Код сотрудника</param>
        /// <returns>Сотрудник</returns>
        public Worker GetWorker(int id)
        {
            try
            {
                return Dm.Worker.GetItem(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Получение всех подразделений
        /// </summary>
        /// <returns>Список подразделений</returns>
        public List<Department> GetAllDepartments()
        {
            try
            {
                return Dm.Department.GetList();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        /// <summary>
        /// Получение подразделения
        /// </summary>
        /// <param name="id">Код подразделения</param>
        /// <returns>Подразделение</returns>
        public Department GetDepartment(int id)
        {
            try
            {
                return Dm.Department.GetItem(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
