﻿using System;
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
        private bool IsAdd => this.Worker.Id == 0;

        private Worker Worker = null;
        private Department Department = null;

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
        internal static bool WorkerIsChanged(Worker worker, Department selectedDepartment = null)
        {
            var fmEditWorker = new FmEditWorker();
            fmEditWorker.Worker = worker?? throw new Exception("Worker is null");
            fmEditWorker.Department = selectedDepartment;

            if (fmEditWorker.ShowDialog() == DialogResult.Cancel)
                return false;

            return true;
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

                this.Worker.FirstName = this.tbFirstName.Text;
                this.Worker.LastName = this.tbLastName.Text;
                this.Worker.DateOfBirth = this.dtpDateOfBirth.Value.Date;
                this.Worker.PhoneNumber = this.mtbPhoneNumber.Text;
                this.Worker.SexId = (int)this.cbSex.SelectedValue;
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
                this.FillCbSex();

                if (this.IsAdd)
                {
                    if (this.Department != null)
                    {
                        this.cbDepartment.SelectedValue = this.Department.Id;
                    }

                    return;
                }

                this.tbFirstName.Text = this.Worker.FirstName;
                this.tbLastName.Text = this.Worker.LastName;
                this.dtpDateOfBirth.Value = this.Worker.DateOfBirth;
                this.cbSex.SelectedValue = this.Worker.Sex;
                this.mtbPhoneNumber.Text = this.Worker.PhoneNumber;
                this.cbDepartment.SelectedValue = this.Worker.DepartmentId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Заполнение comboBox перечислением (Description)
        /// </summary>
        private void FillCbSex()
        {
            this.cbSex.DataSource = Enum.GetValues(typeof(WorkerEnums.Sex))
               .Cast<Enum>()
               .Select(value => new
               {
                   (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), 
                   typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description, value
               })
               .ToList();
        }
    }
}
