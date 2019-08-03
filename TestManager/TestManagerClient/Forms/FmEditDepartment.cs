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
        private TMDataManager Dm => TMDataManager.Instance;
        private Department Department = null;
        private bool IsAdd => this.Department.Id == 0;

        public FmEditDepartment(Department department)
        {
            InitializeComponent();
            this.Department = department;
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.tbNameDepartment.Text))
            {
                MessageBox.Show("Fill in the blank fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(!this.cbIsRoot.Checked && this.cbParentDepartment.SelectedValue == null)
            {
                MessageBox.Show("Select parent department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void EditDepartmentForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.IsAdd)
                {
                    this.cbParentDepartment.DataSource = Dm.TMService.GetAllDepartments();
                    return;
                }

                this.cbParentDepartment.DataSource = Dm.TMService.GetAllDepartments().ToList().FindAll(x => x.Id != this.Department.Id);
                this.tbNameDepartment.Text = this.Department.NameDepartment;

                if (this.Department.ParentId == null)
                    this.cbIsRoot.Checked = true;
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
                if (this.cbIsRoot.Checked)
                {
                    this.cbParentDepartment.Enabled = false;
                    this.cbParentDepartment.SelectedIndex = -1;
                }
                else
                {
                    this.cbParentDepartment.Enabled = true;
                    this.cbParentDepartment.SelectedValue = this.Department.ParentId ?? 0;
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
                if (!this.IsValid())
                {
                    this.DialogResult = DialogResult.None;
                    return;
                }

                var parentId = (int?)this.cbParentDepartment.SelectedValue;
                this.Department.NameDepartment = this.tbNameDepartment.Text;
                this.Department.ParentId = parentId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
