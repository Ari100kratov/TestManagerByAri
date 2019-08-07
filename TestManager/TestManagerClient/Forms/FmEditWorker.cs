using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TestManagerClient.WcfServiceReference;
using TMEnums;

namespace TestManagerClient.Forms
{
    public partial class FmEditWorker : Form
    {
        private TMDataManager Dm => TMDataManager.Instance;
        private bool IsAdd => this._worker.Id == 0;

        private Worker _worker = null;
        private Department _department = null;

        public FmEditWorker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Открывает форму изменения и добавления сотрудника
        /// </summary>
        /// <param name="worker">Сотрудник</param>
        /// <param name="department">Выбранное подразделение</param>
        /// <returns>Статус изменений</returns>
        internal static bool Execute(Worker worker, Department selectedDepartment = null)
        {
            var fmEditWorker = new FmEditWorker();
            fmEditWorker._worker = worker?? throw new ArgumentNullException();
            fmEditWorker._department = selectedDepartment;
            return fmEditWorker.ShowDialog() == DialogResult.OK;
        }

        /// <summary>
        /// Проверка корректности полей сотрудника
        /// </summary>
        /// <returns></returns>
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

                this._worker.FirstName = this.tbFirstName.Text;
                this._worker.LastName = this.tbLastName.Text;
                this._worker.DateOfBirth = this.dtpDateOfBirth.Value.Date;
                this._worker.PhoneNumber = this.mtbPhoneNumber.Text;
                this._worker.SexId = (int)this.cbSex.SelectedValue;
                this._worker.DepartmentId = (int)this.cbDepartment.SelectedValue;
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
                this.cbDepartment.DataSource = Dm.Department.GetList().ToList();
                this.cbSex.DataSource = this.GetDescFromSex().ToList();

                if (this.IsAdd)
                {
                    if (this._department != null)
                    {
                        this.cbDepartment.SelectedValue = this._department.Id;
                    }

                    return;
                }

                this.tbFirstName.Text = this._worker.FirstName;
                this.tbLastName.Text = this._worker.LastName;
                this.dtpDateOfBirth.Value = this._worker.DateOfBirth;
                this.cbSex.SelectedValue = this._worker.Sex;
                this.mtbPhoneNumber.Text = this._worker.PhoneNumber;
                this.cbDepartment.SelectedValue = this._worker.DepartmentId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Возвращает коллекцию description пола сотрудника
        /// </summary>
        /// <returns></returns>
        private IEnumerable<object> GetDescFromSex()
        {
            return Enum.GetValues(typeof(Sex))
               .Cast<Enum>()
               .Select(value => new
               {
                   (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()),
                   typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description,
                   value
               });
        }
    }
}
