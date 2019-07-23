using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public sealed partial class Worker : IKeyedModel
    {
        public int Id { get => this.id; set => this.id = value; }

        public string FirstName { get => this.firstName; set => this.firstName = value; }

        public string LastName { get => this.lastName; set => this.lastName = value; }

        public DateTime DateOfBirth { get => this.dateOfBirth; set => this.dateOfBirth = value; }

        public string PhoneNumber { get => this.phoneNumber; set => this.phoneNumber = value; }

        public int DepartmentId { get => this.departmentId; set => this.departmentId = value; }


        private int id;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private int departmentId;
    }
}
