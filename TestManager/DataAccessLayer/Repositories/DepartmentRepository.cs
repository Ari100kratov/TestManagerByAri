using DataAccessLayer.Entities;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    /// <summary>
    /// Репозиторий подразделение
    /// </summary>
    internal class DepartmentRepository : CacheBaseRepository<Department>
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

            RecursiveDeleteDepartment(department);
        }

        /// <summary>
        /// Удаление переданного подразделение и его дочерних объектов без нарушения целостности базы данных
        /// </summary>
        /// <param name="parent">Удаляемое подразделение</param>
        private void RecursiveDeleteDepartment(Department parent)
        {
            if (parent.Children.Count() != 0)
            {
                var children = Dm.Department.GetList().FindAll(x => x.ParentId == parent.Id);

                foreach (var child in children)
                {
                    RecursiveDeleteDepartment(child);
                }
            }

            base.Delete(parent);
        }
    }
}
