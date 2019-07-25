using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManagerClient.WcfServiceReference;

namespace TestManagerClient.Forms
{
    public partial class AddDepartmentForm : Form
    {
        public AddDepartmentForm()
        {
            InitializeComponent();
        }

        private void AddDepartmentForm_Load(object sender, EventArgs e)
        {
            if (Program.TMWcfService.GetAllDepartments().Count() == 0)
                return;

            this.cbParentDepartment.ValueMember = "Id";
            this.cbParentDepartment.DisplayMember = "NameDepartment";
            this.cbParentDepartment.DataSource = Program.TMWcfService.GetAllDepartments().ToList();
            this.cbParentDepartment.SelectedIndex = 0;
        }

        private void checkboxUpper_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkboxUpper.Checked)
            {
                this.cbParentDepartment.Enabled = false;
                this.cbParentDepartment.SelectedIndex = -1;
            }
            else
            {
                this.cbParentDepartment.Enabled = true;
                this.cbParentDepartment.SelectedIndex = 0;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.tbNameDepartment.Text))
            {
                this.DialogResult = DialogResult.None;
                MessageBox.Show("Fill in the blank fields", "Attetion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var parentId = this.checkboxUpper.Checked ? 0 : (int)this.cbParentDepartment.SelectedValue;

            var department = new Department
            {
                NameDepartment = this.tbNameDepartment.Text,
                ParentId = parentId
            };

            Program.TMWcfService.AddNewDepartment(department);

        }
    }
}
