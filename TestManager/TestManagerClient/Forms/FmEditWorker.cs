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
        private Worker Worker = null;

        public FmEditWorker(Worker worker)
        {
            InitializeComponent();
            this.Worker = worker;
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

                if (DateTime.Now.Year - this.dtpDateOfBirth.Value.Year < 18)
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

                this.Worker.FirstName = this.tbFirstName.Text;
                this.Worker.LastName = this.tbLastName.Text;
                this.Worker.DateOfBirth = this.dtpDateOfBirth.Value.Date;
                this.Worker.PhoneNumber = this.mtbPhoneNumber.Text;
                this.Worker.DepartmentId = (int)this.cbDepartment.SelectedValue;

                Program.TMWcfService.EditWorker(this.Worker);
                this.DialogResult = DialogResult.OK;
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
                this.cbDepartment.ValueMember = "Id";
                this.cbDepartment.DisplayMember = "NameDepartment";
                this.cbDepartment.DataSource = Program.TMWcfService.GetAllDepartments().ToList();

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
