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
            /*
            var departments = Dm.Department.GetList().FindAll(x => x.ParentId == id || x.Id == id);
            this.DeleteWorkersFromDepartments(departments);

            foreach (var dept in departments)
                base.Delete(dept.Id);
                */

            var department = Dm.Department.GetItem(id);

            foreach (var worker in department.Workers)
                Dm.Worker.Delete(worker.Id);

            foreach (var childDepartment in department.ChildDepartments)
                base.Delete(childDepartment.Id);

            base.Delete(id);
        }

        /*
        private void DeleteWorkersFromDepartments(List<Department> departmentList)
        {
            var departmentListId = departmentList.Select(x => x.Id).ToList();
            foreach (var departmentId in departmentListId)
            {
                var workerListId = Dm.Worker.GetList().FindAll(x => x.DepartmentId == departmentId).Select(x => x.Id).ToList();
                foreach (var workerId in workerListId)
                {
                    Dm.Worker.Delete(workerId);
                }
            }
        }
        */
    }
}
