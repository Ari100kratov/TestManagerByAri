using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestManagerClient.Forms;
using TestManagerClient.WcfServiceReference;

namespace TestManagerClient.Forms
{
    public partial class MainForm : Form
    {
        private TMDataManager Dm => TMDataManager.Instance;

        private List<Worker> WorkerList = new List<Worker>();
        private Department SelectedDepartment => this.tvDepartments.SelectedNode?.Tag as Department;
        private Worker SelectedWorker => this.dgvWorkers.CurrentRow?.DataBoundItem as Worker;

        public MainForm()
        {
            InitializeComponent();
            this.dgvWorkers.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Рекурсивное заполнение дерева подразделений
        /// </summary>
        /// <param name="departmentList">Список подразделений</param>
        /// <param name="rootNode">Корневой узел дерева</param>
        private void FillTreeView(List<Department> departmentList, TreeNode rootNode = null)
        {
            var selector = rootNode == null ? (int?)null : (rootNode.Tag as Department).Id;
            var departments = departmentList.FindAll(x => x.ParentId == selector);

            foreach (var dept in departments)
            {
                var node = new TreeNode
                {
                    Text = dept.NameDepartment,
                    Name = dept.Id.ToString(),
                    Tag = dept
                };

                if (rootNode == null)
                {
                    this.tvDepartments.Nodes.Add(node);
                }
                else
                {
                    rootNode.Nodes.Add(node);
                }

                this.FillTreeView(departmentList, node);
            }
            //this.tvDepartments.ExpandAll();
        }

        /// <summary>
        /// Обновление данных в таблице сотрудников
        /// </summary>
        private void RefreshDgvWorkers()
        {
            if (this.SelectedDepartment == null)
                this.dgvWorkers.DataSource = this.WorkerList;
            else
                this.dgvWorkers.DataSource = this.SelectedDepartment.Workers;
        }

        private void UpdateTreeView(Department editedDepartment)
        {
            var node = (TreeNode)this.tvDepartments.SelectedNode.Clone();
            this.tvDepartments.SelectedNode.Remove();
            node.Text = editedDepartment.NameDepartment;
            node.Tag = editedDepartment;

            if (editedDepartment.ParentId == null)
            {
                this.tvDepartments.Nodes.Add(node);
            }
            else
            {
                var parentNode = this.tvDepartments.Nodes.Find(editedDepartment.ParentId.ToString(), true).First();
                parentNode.Nodes.Add(node);
            }

            this.tvDepartments.ExpandAll();
            this.tvDepartments.SelectedNode = node;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.WorkerList = Dm.TMService.GetAllWorkers().ToList();
                this.FillTreeView(Dm.TMService.GetAllDepartments().ToList());
                this.tvDepartments.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                // var addDepartmentForm = new AddDepartmentForm(this, this.SelectedNode.Tag as Department);

                //  if (addDepartmentForm.ShowDialog() == DialogResult.Cancel)
                //  return;

                //Если корневой узел
                if (this.AddedDepartment.ParentId == 0)
                {
                    var parentNode = new TreeNode
                    {
                        Text = this.AddedDepartment.NameDepartment,
                        Tag = this.AddedDepartment,
                        Name = this.AddedDepartment.Id.ToString()
                    };
                    this.tvDepartments.Nodes[0].Nodes.Add(parentNode);
                }
                //Если дочерний узел
                else
                {
                    var parentNode = this.tvDepartments.Nodes.Find(this.AddedDepartment.ParentId.ToString(), true).First();
                    var childNode = new TreeNode
                    {
                        Text = this.AddedDepartment.NameDepartment,
                        Tag = this.AddedDepartment,
                        Name = this.AddedDepartment.Id.ToString()
                    };
                    parentNode.Nodes.Add(childNode);
                    parentNode.Expand();
                }

                this.AddedDepartment = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            */
        }

        private void btnEditDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedDepartment == null)
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var editDepartmentForm = new FmEditDepartment(this.SelectedDepartment);
                if (editDepartmentForm.ShowDialog() == DialogResult.Cancel)
                    return;

                //Сохраняем изменения в базе данных
                Dm.TMService.EditDepartment(this.SelectedDepartment);
                //Обновляем дерево подразделений
                this.UpdateTreeView(this.SelectedDepartment);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            if (this.SelectedDepartment == null)
            {
                MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete the selected and all dependent departments?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //Удаляем подразделение из базы данных
            this.Dm.TMService.DeleteDepartment(this.SelectedDepartment.Id);
            //Удаляем из списка сотрудников этого подразделения
            this.WorkerList.RemoveAll(x => this.SelectedDepartment.Workers.Contains(x));
            //Удаляем узел дерева подразделения
            this.tvDepartments.SelectedNode.Remove();
        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            try
            {
                var worker = new Worker();
                var fmAddWorker = new FmEditWorker(worker, this.SelectedDepartment);
                if (fmAddWorker.ShowDialog(this) == DialogResult.Cancel)
                    return;

                //Добавляем сотрудника в базу данных
                this.Dm.TMService.AddWorker(worker);
                //Добавляем в список
                this.WorkerList.Add(worker);
                //Обновляем таблицу сотрудников
                this.RefreshDgvWorkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditWorker_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedWorker == null)
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var editWorkerForm = new FmEditWorker(this.SelectedWorker);
                if (editWorkerForm.ShowDialog() == DialogResult.Cancel)
                    return;

                //Вносим изменения в базу данных
                this.Dm.TMService.EditWorker(this.SelectedWorker);
                //Обновляем таблицу сотрудников
                this.dgvWorkers.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteWorker_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedWorker == null)
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Do you really want to delete the selected record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                //Удаляем сотрудника из базы данных
                this.Dm.TMService.DeleteWorker(this.SelectedWorker.Id);
                //Удаляем из списка
                this.WorkerList.Remove(this.SelectedWorker);
                //Обновляем таблицу сотрудников
                this.RefreshDgvWorkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                this.RefreshDgvWorkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
