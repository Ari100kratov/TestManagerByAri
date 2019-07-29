using DataAccessLayer.Interfaces;
using System;
using System.Runtime.Serialization;
using TMAttributes;

namespace DataAccessLayer.Entities
{
    /// <summary>
    /// Сотрудник
    /// </summary>
    [DataContract]
    public partial class Worker : IKeyedModel
    {
        private DataManager Dm => DataManager.Instance;

        /// <summary>
        /// Код сотрудника
        /// </summary>
        [DataMember]
        [ColumnDB]
        [AutoIncrementDB]
        public int Id { get => this.id; set => this.id = value; }

        /// <summary>
        /// Имя
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string FirstName { get => this.firstName; set => this.firstName = value; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string LastName { get => this.lastName; set => this.lastName = value; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DataMember]
        [ColumnDB]
        public DateTime DateOfBirth { get => this.dateOfBirth; set => this.dateOfBirth = value; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string PhoneNumber { get => this.phoneNumber; set => this.phoneNumber = value; }

        /// <summary>
        /// Код подразделения
        /// </summary>
        [DataMember]
        [ColumnDB]
        public int DepartmentId { get => this.departmentId; set => this.departmentId = value; }

        /// <summary>
        /// Подразделение
        /// </summary>
        public Department Department => Dm.Department.GetItem(this.Id);

        private int id;
        private string firstName;
        private string lastName;
        private DateTime dateOfBirth;
        private string phoneNumber;
        private int departmentId;
    }
}
