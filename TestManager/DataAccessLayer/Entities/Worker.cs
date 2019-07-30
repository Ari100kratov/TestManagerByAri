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
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string LastName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        [DataMember]
        [ColumnDB]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        [DataMember]
        [ColumnDB]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Код подразделения
        /// </summary>
        [DataMember]
        [ColumnDB]
        public int DepartmentId { get; set; }

        /// <summary>
        /// Пол сотрудника
        /// </summary>
        [DataMember]
        [ColumnDB]
        public int Sex { get; set; }

        /// <summary>
        /// Подразделение
        /// </summary>
        public Department Department => Dm.Department.GetItem(this.DepartmentId);
    }
}
