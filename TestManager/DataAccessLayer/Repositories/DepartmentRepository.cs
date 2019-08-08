using DataAccessLayer.Entities;
using System;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Репозиторий подразделение
    /// </summary>
    public class DepartmentRepository : CacheBaseRepository<Department>
    {
        private DataManager Dm => DataManager.Instance;

        /// <summary>
        /// Удаление подразделения и его дочерних обьектов
        /// </summary>
        /// <param name="id">Код подразделения</param>
        public override void Delete(int id)
        {
            var department = Dm.Department.GetItem(id);

            foreach (var worker in department.Workers)
            {
                Dm.Worker.Delete(worker.Id);
            }

            var children = this.Where(x => x.ParentId == department.Id);

            foreach (var child in children)
            {
                this.Delete(child.Id);
            }

            base.Delete(id);
        }

        /// <summary>
        /// Удаление подразделения и его дочерних обьектов
        /// </summary>
        /// <param name="item">Подразделение</param>
        public override void Delete(Department item)
        {
            this.Delete(item.Id);
        }
    }
}
