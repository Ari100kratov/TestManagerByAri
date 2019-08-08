using System;
using System.Collections.Generic;
using System.ServiceModel;
using DataAccessLayer.Entities;

namespace TMWcfService
{
    /// <summary>
    /// Интерфейс-контракт WCF службы
    /// </summary>
    [ServiceContract]
    internal interface IService
    {
        /// <summary>
        /// Добавление сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        /// <returns>Id добавленного сотрудника</returns>
        [OperationContract]
        int AddWorker(Worker worker);

        /// <summary>
        /// Изменение сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        [OperationContract]
        void EditWorker(Worker worker);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id">Код сотрудника</param>
        [OperationContract]
        void DeleteWorker(int id);

        /// <summary>
        /// Добавление подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        /// <returns>Id добавленного подразделения</returns>
        [OperationContract]
        int AddDepartment(Department department);

        /// <summary>
        /// Изменение подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        [OperationContract]
        void EditDepartment(Department department);

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="id">Код подрзделения</param>
        [OperationContract]
        void DeleteDepartment(int id);

        /// <summary>
        /// Получение всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        [OperationContract]
        List<Worker> GetAllWorkers();

        /// <summary>
        /// Получение сотрудника
        /// </summary>
        /// <param name="id">Код сотрудника</param>
        /// <returns>Сотрудник</returns>
        [OperationContract]
        Worker GetWorker(int id);

        /// <summary>
        /// Получение всех подразделений
        /// </summary>
        /// <returns>Список подразделений</returns>
        [OperationContract]
        List<Department> GetAllDepartments();

        /// <summary>
        /// Получение подразделения
        /// </summary>
        /// <param name="id">Код подразделения</param>
        /// <returns>Подразделение</returns>
        [OperationContract]
        Department GetDepartment(int id);
    }
}
