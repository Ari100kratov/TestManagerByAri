using System;
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

        public Department ParentDepartment => Dm.TMService.GetDepartment(this.ParentId??0);
        /// <summary>
        /// Список дочерних подразделений
        /// </summary>
        public List<Department> ChildDepartments
        {
            get
            {
                var lookup = Dm.TMService.GetAllDepartments().ToLookup(x => x.ParentId);
                return lookup[null].SelectRecursive(x => lookup[x.Id]).ToList();
            }
        }

        /// <summary>
        /// Список дочерних подразделений вместе с текущим подразделением
        /// </summary>
        public List<Department> CurrDepartmentWithChilds
        {
            get
            {
                var departments = this.ChildDepartments;
                departments.Add(Dm.TMService.GetDepartment(this.Id));
                return departments;
            }
        }
        
        /// <summary>
        /// Список сотрудников из текущего и дочерних подразделений
        /// </summary>
        public List<Worker> Workers => Dm.TMService.GetAllWorkers().Where(x => this.CurrDepartmentWithChilds.Contains(x.Department)).ToList();
    }
}
