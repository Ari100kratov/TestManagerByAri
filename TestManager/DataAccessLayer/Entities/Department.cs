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
        public int Id { get => id; set => this.id = value; }

        /// <summary>
        /// Наименование подразделения
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string NameDepartment { get => this.nameDepartment; set => this.nameDepartment = value; }

        /// <summary>
        /// Код родительского подразделения (0 = корневой)
        /// </summary>
        [DataMember]
        [ColumnDB]
        public int ParentId { get => this.parentId; set => this.parentId = value; }

        /// <summary>
        /// Список сотрудников подразделения
        /// </summary>
        public List<Worker> Workers => this.Dm.Worker.GetList().Where(x => x.DepartmentId == this.Id).ToList();

        private int id;
        private string nameDepartment;
        private int parentId;
    }
}
