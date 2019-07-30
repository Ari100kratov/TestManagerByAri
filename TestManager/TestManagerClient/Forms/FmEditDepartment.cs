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
    public partial class FmEditDepartment : Form
    {
        private Department Department = new Department();
        public FmEditDepartment(Department department)
        {
            InitializeComponent();
            this.Department = department;
        }

        private void EditDepartmentForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.tbNameDepartment.Text = this.Department.NameDepartment;
                this.cbParentDepartment.ValueMember = "Id";
                this.cbParentDepartment.DisplayMember = "NameDepartment";
                this.cbParentDepartment.DataSource = Program.TMWcfService.GetAllDepartments().Where(x => x.Id != this.Department.Id).ToList();

                if (this.Department.ParentId == 0)
                    this.checkboxUpper.Checked = true;
                else
                    this.cbParentDepartment.SelectedValue = this.Department.ParentId;

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkboxUpper_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.checkboxUpper.Checked)
                {
                    this.cbParentDepartment.Enabled = false;
                    this.cbParentDepartment.SelectedIndex = -1;
                }
                else
                {
                    this.cbParentDepartment.Enabled = true;
                    this.cbParentDepartment.SelectedIndex = this.Department.ParentId;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.tbNameDepartment.Text))
                {
                    this.DialogResult = DialogResult.None;
                    MessageBox.Show("Fill in the blank fields", "Attetion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var parentId = this.checkboxUpper.Checked ? 0 : (int)this.cbParentDepartment.SelectedValue;

                this.Department.NameDepartment = this.tbNameDepartment.Text;
                this.Department.ParentId = parentId;

                Program.TMWcfService.EditDepartment(this.Department);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
