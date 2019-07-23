using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public sealed partial class Department : IKeyedModel
    {
        public int Id { get => id; set => this.id = value; }

        public string NameDepartment { get => this.nameDepartment; set => this.nameDepartment = value; }

        public int ParentId { get => this.parentId; set => this.parentId = value; }

        private int id;
        private string nameDepartment;
        private int parentId;
    }
}
