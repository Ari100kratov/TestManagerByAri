using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManagerClient.Forms;

namespace TestManagerClient.WcfServiceReference
{
   public partial class Worker
    {
        public int Age => DateTime.Now.Year - this.DateOfBirth.Year;

        public string FullName => $"{this.FirstName} {this.LastName}";

        public Department Department => Program.TMWcfService.GetDepartmentById(this.DepartmentId);
    }
}
