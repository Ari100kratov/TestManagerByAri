using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMAttributes;
using System.Runtime.Serialization;
using TMExtensions;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Подразделение
    /// </summary>
    [DataContract]
    public sealed partial class Department : IKeyedModel
    {
        private DataManager Dm => DataManager.Instance;

        /// <summary>
        /// Код подразделения
        /// </summary>
        [DataMember]
        [ColumnDB]
        [AutoIncrementDB]
        public int Id { get; set; }

        /// <summary>
        /// Наименование подразделения
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string NameDepartment { get; set; }

        /// <summary>
        /// Код родительского подразделения (null - корневой)
        /// </summary>
        [DataMember]
        [ColumnDB]
        public int? ParentId { get; set; }

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
                departments.Add(Dm.Department.GetItem(this.Id));
                return departments;
            }
        }

        /// <summary>
        /// Список сотрудников из текущего и дочерних подразделений
        /// </summary>
        public List<Worker> Workers => Dm.Worker.GetList().Where(x => this.CurrDepartmentWithChildren.Select(w => w.Id).Contains(x.DepartmentId)).ToList();
    }
}
