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
        private MainForm MainForm = null;

        public AddDepartmentForm(MainForm mainForm)
        {
            InitializeComponent();
            this.MainForm = mainForm;
        }

        private void AddDepartmentForm_Load(object sender, EventArgs e)
        {
            var departmentList = Program.TMWcfService.GetAllDepartments().ToList();

            if (departmentList.Count() == 0)
            {
                this.checkboxUpper.Checked = true;
                this.checkboxUpper.Enabled = false;
            }

            this.cbParentDepartment.ValueMember = "Id";
            this.cbParentDepartment.DisplayMember = "NameDepartment";
            this.cbParentDepartment.DataSource = departmentList;
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

            department.Id = Program.TMWcfService.AddNewDepartment(department);
            this.MainForm.AddedDepartment = department;

        }
    }
}
