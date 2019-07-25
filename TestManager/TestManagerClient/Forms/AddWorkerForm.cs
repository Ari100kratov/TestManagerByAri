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
    public partial class AddWorkerForm : Form
    {
        private Department Department = new Department();
        public AddWorkerForm(Department department = null)
        {
            InitializeComponent();
            this.Department = department;
        }

        private void AddWorkerForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.cbDepartment.ValueMember = "Id";
                this.cbDepartment.DisplayMember = "NameDepartment";
                this.cbDepartment.DataSource = Program.TMWcfService.GetAllDepartments().ToList();
                this.cbDepartment.SelectedIndex = 0;

                if (this.Department != null)
                    this.cbDepartment.SelectedItem = this.Department;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {

                this.DialogResult = DialogResult.None;
                if (string.IsNullOrWhiteSpace(this.tbFirstName.Text) || string.IsNullOrWhiteSpace(this.tbLastName.Text))
                {
                    MessageBox.Show("Fill in the blank fields", "Attetion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.dtpDateOfBirth.Value.Date >= DateTime.Now.Date)
                {
                    MessageBox.Show("Date of birth is incorrect", "Attetion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!this.mtbPhoneNumber.MaskFull)
                {
                    MessageBox.Show("Phone number is incorrect", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.cbDepartment.SelectedItem == null)
                {
                    MessageBox.Show("Choose department", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var worker = new Worker
                {
                    FirstName = this.tbFirstName.Text,
                    LastName = this.tbLastName.Text,
                    DateOfBirth = this.dtpDateOfBirth.Value.Date,
                    PhoneNumber = this.mtbPhoneNumber.Text,
                    DepartmentId = (int)this.cbDepartment.SelectedValue
                };

                Program.TMWcfService.AddNewWorker(worker);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
        }
    }
}
