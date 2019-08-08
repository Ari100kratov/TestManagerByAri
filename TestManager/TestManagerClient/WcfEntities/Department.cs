using System.Collections.Generic;
using System.Linq;
using TMExtensions;

namespace TestManagerClient.WcfServiceReference
{
    /// <summary>
    /// Подразделение
    /// </summary>
    public partial class Department
    {
        private TMDataManager Dm => TMDataManager.Instance;

        /// <summary>
        /// Список дочерних подразделений
        /// </summary>
        public List<Department> Children
        {
            get
            {
                var lookup = Dm.Department.GetList().ToLookup(x => x.ParentId);
                return lookup[this.Id].SelectRecursive(x => lookup[x.Id]).ToList();
            }
        }

        /// <summary>
        /// Список дочерних подразделений вместе с текущим подразделением
        /// </summary>
        public List<Department> CurrDepartmentWithChildren
        {
            get
            {
                var departments = this.Children;
                var currDepartment = Dm.Department.Get(this.Id);
                if (currDepartment != null)
                {
                    departments.Add(currDepartment);
                }
                return departments;
            }
        }

        /// <summary>
        /// Список сотрудников из текущего и дочерних подразделений
        /// </summary>
        public List<Worker> Workers => Dm.Worker.Where(x => this.CurrDepartmentWithChildren.Select(t => t.Id).Contains(x.DepartmentId));
    }
}
