using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Department GetDepartment(int id)
        {
            return this._service.GetDepartment(id);
        }
    }
}
