using System;
using System.Collections.Generic;
using System.Linq;
using TestManagerClient.WcfServiceReference;

namespace TestManagerClient.WcfRepositories
{
    /// <summary>
    /// Доступ к операциям WCF связанные с подразделением
    /// </summary>
    public class DepartmentRepository
    {
        private ServiceClient _service = new ServiceClient();

        /// <summary>
        /// Добавление подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        public void Add(Department department)
        {
            department.Id = this._service.AddDepartment(department);
        }

        /// <summary>
        /// Изменение подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        public void Edit(Department department)
        {
            this._service.EditDepartment(department);
        }

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="id">Код подрзделения</param>
        public void Delete(int id)
        {
            this._service.DeleteDepartment(id);
        }

        /// <summary>
        /// Удаление подразделения
        /// </summary>
        /// <param name="department">Подразделение</param>
        public void Delete(Department department)
        {
            this._service.DeleteDepartment(department.Id);
        }

        /// <summary>
        /// Получение всех подразделений
        /// </summary>
        /// <returns>Список подразделений</returns>
        public List<Department> GetList()
        {
            return this._service.GetAllDepartments().ToList();
        }

        /// <summary>
        /// Получение подразделения
        /// </summary>
        /// <param name="id">Код подразделения</param>
        /// <returns>Подразделение</returns>
        public Department Get(int id)
        {
            return this._service.GetDepartment(id);
        }

        public Department FirstOrDefault(Func<Department, bool> filter)
        {
            if (filter == null)
                throw new ArgumentException();

            return this._service.GetAllDepartments().FirstOrDefault(filter);
        }

        public List<Department> Where(Func<Department, bool> filter)
        {
            if (filter == null)
                throw new ArgumentException();

            return this._service.GetAllDepartments().Where(filter).ToList();
        }

        public Department SingleOrDefault(Func<Department,bool> filter)
        {
            if (filter == null)
                throw new ArgumentException();

            return this._service.GetAllDepartments().SingleOrDefault(filter);
        }
    }
}
