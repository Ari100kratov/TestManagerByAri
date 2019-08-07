using System;
using System.Linq;
using System.Windows.Forms;
using TestManagerClient.WcfServiceReference;

namespace TestManagerClient.Forms
{
    public partial class FmEditDepartment : Form
    {
        private TMDataManager Dm => TMDataManager.Instance;
        private bool IsAdd => this._department.Id == 0;

        private Department _department = null;


        public FmEditDepartment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Открывает форму изменения и добавления сотрудника
        /// </summary>
        /// <param name="department">Подразделение</param>
        /// <returns>Статус изменений</returns>
        internal static bool Execute(Department department)
        {
            var fmEditDepartment = new FmEditDepartment();
            fmEditDepartment._department = department ?? throw new ArgumentNullException();
            return fmEditDepartment.ShowDialog() == DialogResult.OK;
        }

        /// <summary>
        /// Проверка корректности полей подразделения
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (string.IsNullOrWhiteSpace(this.tbNameDepartment.Text))
            {
                MessageBox.Show("Fill in the blank fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!this.cbIsRoot.Checked && this.cbParentDepartment.SelectedValue == null)
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
                    this.cbParentDepartment.DataSource = Dm.Department.GetList();
                    return;
                }

                this.cbParentDepartment.DataSource = Dm.Department.GetList().FindAll(x => x.Id != this._department.Id);
                this.tbNameDepartment.Text = this._department.Name;

                if (this._department.ParentId == null)
                    this.cbIsRoot.Checked = true;
                else
                    this.cbParentDepartment.SelectedValue = this._department.ParentId;
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
                    this.cbParentDepartment.SelectedValue = this._department.ParentId ?? 0;
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
                this._department.Name = this.tbNameDepartment.Text;
                this._department.ParentId = parentId;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
