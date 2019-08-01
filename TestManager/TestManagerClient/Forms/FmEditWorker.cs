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
    public partial class FmEditWorker : Form
    {
        private TMDataManager Dm => TMDataManager.Instance;

        private bool IsAdd => this.Worker.Id == 0;
        private Worker Worker = null;
        private Department Department = null;

        public FmEditWorker(Worker worker, Department department =null)
        {
            InitializeComponent();
            this.Worker = worker;
        }

        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.tbFirstName.Text) || string.IsNullOrWhiteSpace(this.tbLastName.Text))
            {
                MessageBox.Show("Fill in the blank fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (DateTime.Now.Year - this.dtpDateOfBirth.Value.Year < 18)
            {
                MessageBox.Show("Date of birth is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!this.mtbPhoneNumber.MaskFull)
            {
                MessageBox.Show("Phone number is incorrect", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (this.cbDepartment.SelectedItem == null)
            {
                MessageBox.Show("Choose department", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
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
               
                this.Worker.FirstName = this.tbFirstName.Text;
                this.Worker.LastName = this.tbLastName.Text;
                this.Worker.DateOfBirth = this.dtpDateOfBirth.Value.Date;
                this.Worker.PhoneNumber = this.mtbPhoneNumber.Text;
                this.Worker.DepartmentId = (int)this.cbDepartment.SelectedValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EditWorkerForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.cbDepartment.DataSource = Dm.TMService.GetAllDepartments().ToList();

                if (this.IsAdd)
                {
                    this.cbDepartment.SelectedValue = this.Department?.Id;
                    return;
                }

                this.tbFirstName.Text = this.Worker.FirstName;
                this.tbLastName.Text = this.Worker.LastName;
                this.dtpDateOfBirth.Value = this.Worker.DateOfBirth;
                this.mtbPhoneNumber.Text = this.Worker.PhoneNumber;
                this.cbDepartment.SelectedValue = this.Worker.DepartmentId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
