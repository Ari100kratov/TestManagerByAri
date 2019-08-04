using System;
using TMEnums;

namespace TestManagerClient.WcfServiceReference
{
   public partial class Worker
    {
        private TMDataManager Dm => TMDataManager.Instance;

        /// <summary>
        /// Возраст сотрудника
        /// </summary>
        public int Age => DateTime.Now.Year - this.DateOfBirth.Year;

        /// <summary>
        /// Полное имя сотрудника (Имя Фамилия)
        /// </summary>
        public string FullName => $"{this.FirstName} {this.LastName}";

        /// <summary>
        /// Подразделение сотрудника
        /// </summary>
        public Department Department => Dm.TMService.GetDepartment(this.DepartmentId);

        /// <summary>
        /// Пол сотрудника
        /// </summary>
        public WorkerEnums.Sex Sex => (WorkerEnums.Sex)this.SexId;

        /// <summary>
        /// Пол сотрудника на русском языке
        /// </summary>
        public string SexRUS => WorkerEnums.GetSexRUS(this.Sex);
    }
}
