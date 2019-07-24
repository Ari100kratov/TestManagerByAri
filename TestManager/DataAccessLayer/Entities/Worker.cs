using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using TMAttributes;

namespace DataAccessLayer.Entities
{
    [DataContract]
    public sealed partial class Worker : IKeyedModel
    {
        private DataManager Dm => DataManager.Instance;

        [DataMember]
        [ColumnDB]
        [AutoIncrementDB]
        public int Id { get => this.id; set => this.id = value; }

        [DataMember]
        [ColumnDB]
        public string FirstName { get => this.firstName; set => this.firstName = value; }

        [DataMember]
        [ColumnDB]
        public string LastName { get => this.lastName; set => this.lastName = value; }

        [DataMember]
        [ColumnDB]
        public DateTime DateOfBirth { get => this.dateOfBirth; set => this.dateOfBirth = value; }

        [DataMember]
        [ColumnDB]
        public string PhoneNumber { get => this.phoneNumber; set => this.phoneNumber = value; }

        [DataMember]
        [ColumnDB]
        public int DepartmentId { get => this.departmentId; set => this.departmentId = value; }

        public Department Department => Dm.Department.GetItem(this.Id);

        private int id;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private int departmentId;
    }
}
