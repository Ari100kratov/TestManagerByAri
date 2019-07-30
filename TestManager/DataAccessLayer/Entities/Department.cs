using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMAttributes;
using System.Runtime.Serialization;


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
        /// Код родительского подразделения (0 = корневой)
        /// </summary>
        [DataMember]
        [ColumnDB]
        public int ParentId { get; set; }

        /// <summary>
        /// Список сотрудников подразделения
        /// </summary>
        public List<Worker> Workers => this.Dm.Worker.GetList().FindAll(x => x.DepartmentId == this.Id);

        /// <summary>
        /// Родительское подразделение
        /// </summary>
        public Department ParentDepartment => this.Dm.Department.GetItem(this.ParentId);

        //public List<Department> ChildDepartments => this.Dm.Department.GetList().Where(x=>x.ParentId == this.Id).SelectMany(x=>x.ParentDepartment)
    }
}
