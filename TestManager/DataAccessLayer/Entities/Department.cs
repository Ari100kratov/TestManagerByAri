using DataAccessLayer.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMAttributes;
using System.Runtime.Serialization;


namespace DataAccessLayer.Entities
{
    [DataContract]
    public sealed partial class Department : IKeyedModel
    {
        private DataManager Dm => DataManager.Instance;

        [DataMember]
        [ColumnDB]
        [AutoIncrementDB]
        public int Id { get => id; set => this.id = value; }

        [DataMember]
        [ColumnDB]
        public string NameDepartment { get => this.nameDepartment; set => this.nameDepartment = value; }

        [DataMember]
        [ColumnDB]
        public int ParentId { get => this.parentId; set => this.parentId = value; }

        public List<Worker> Workers => this.Dm.Worker.GetList().Where(x => x.DepartmentId == this.Id).ToList();

        private int id;
        private string nameDepartment;
        private int parentId;
    }
}
