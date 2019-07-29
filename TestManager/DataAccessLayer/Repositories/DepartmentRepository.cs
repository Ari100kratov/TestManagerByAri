using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Репозиторий подразделение
    /// </summary>
    internal class DepartmentRepository : BaseRepository<Department>
    {
        /// <summary>
        /// Удаление подразделения и его дочерних обьектов
        /// </summary>
        /// <param name="id"></param>
        internal override void Delete(int id)
        {
            var childDepartmentsId = DataManager.Instance.Department.GetList().Where(x => x.ParentId == id).Select(x => x.Id).ToList();
            foreach (var departmentId in childDepartmentsId)
            {
                /*
                var workersId = DataManager.Instance.Worker.GetList().Where(x => x.DepartmentId == departmentId).Select(x => x.Id).ToList();
                foreach (var workerId in workersId)
                {
                    DataManager.Instance.Worker.Delete(workerId);
                }
                */
                DataManager.Instance.Department.Delete(departmentId);
            }

            /*
            var workerIds = DataManager.Instance.Worker.GetList().Where(x => x.DepartmentId == id).Select(x => x.Id).ToList();
            foreach (var workerId in workerIds)
            {
                DataManager.Instance.Worker.Delete(workerId);
            }
            */   

            base.Delete(id);
        }
    }
}
