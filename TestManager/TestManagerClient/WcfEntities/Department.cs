using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestManagerClient.Forms;

namespace TestManagerClient.WcfServiceReference
{
    public partial class Department
    {
        public Department ParentDepartment => Program.TMWcfService.GetDepartmentById(this.ParentId);

        public List<Worker> Workers => Program.TMWcfService.GetAllWorkers().Where(x => x.DepartmentId == this.Id).ToList();
    }
}
