using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Репозиторий подразделение
    /// </summary>
    internal class DepartmentRepository : BaseRepository<Department>
    {
        private DataManager Dm => DataManager.Instance;

        /// <summary>
        /// Удаление подразделения и его дочерних обьектов
        /// </summary>
        /// <param name="id">Код подразделения</param>
        internal override void Delete(int id)
        {
            var department = Dm.Department.GetItem(id);

            foreach (var worker in department.Workers)
            {
                Dm.Worker.Delete(worker.Id);
            }

            foreach (var childDepartment in department.ChildDepartments)
            {
                base.Delete(childDepartment.Id);
            }

            base.Delete(id);
        }
    }
}
