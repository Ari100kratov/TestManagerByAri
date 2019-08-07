using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TestManagerClient.WcfServiceReference;

namespace TestManagerClient.Forms
{
    public partial class MainForm : Form
    {
        private TMDataManager Dm => TMDataManager.Instance;
        private Department SelectedDepartment => this.tvDepartments.SelectedNode?.Tag as Department;
        private Worker SelectedWorker => this.dgvWorkers.CurrentRow?.DataBoundItem as Worker;

        private List<Worker> _workerList = new List<Worker>();

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
            var treeNodeCollection = rootNode == null ? this.tvDepartments.Nodes : rootNode.Nodes;
            var rootNodeDepartmentId = rootNode == null ? (int?)null : (rootNode.Tag as Department).Id;
            var departments = departmentList.FindAll(x => x.ParentId == rootNodeDepartmentId);

            foreach (var dept in departments)
            {
                var node = new TreeNode
                {
                    Text = dept.Name,
                    Name = dept.Id.ToString(),
                    Tag = dept
                };

                treeNodeCollection.Add(node);

                this.FillTreeView(departmentList, node);
            }
        }

        /// <summary>
        /// Обновление данных в таблице сотрудников
        /// </summary>
        private void RefreshDgvWorkers()
        {
            this.dgvWorkers.DataSource = this.SelectedDepartment == null ? 
                this._workerList : this.SelectedDepartment.Workers;
        }

        /// <summary>
        /// Обновление дерева подразделений
        /// </summary>
        /// <param name="savedDepartment">Измененное подразделение</param>
        private void UpdateTreeView(Department savedDepartment)
        {
            var node = new TreeNode();

            //Если происходит изменение существующего узла
            if (savedDepartment.Id == this.SelectedDepartment.Id)
            {
                node = (TreeNode)this.tvDepartments.SelectedNode.Clone();
                this.tvDepartments.SelectedNode.Remove();
            }
            else
            {
                node.Name = savedDepartment.Id.ToString();
            }

            node.Text = savedDepartment.Name;
            node.Tag = savedDepartment;

            if (savedDepartment.ParentId == null)
            {
                this.tvDepartments.Nodes.Add(node);
            }
            else
            {
                var parentNode = this.tvDepartments.Nodes.Find(savedDepartment.ParentId.ToString(), true).First();
                parentNode.Nodes.Add(node);
            }

            this.tvDepartments.SelectedNode = node;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this._workerList = Dm.Worker.GetList();
                this.FillTreeView(Dm.Department.GetList());
                this.tvDepartments.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                var department = new Department();
                if (!FmEditDepartment.Execute(department))
                    return;

                //Сохранение в базу данных
                this.Dm.Department.Add(department);
                //Обновление дерева подразделений
                this.UpdateTreeView(department);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                if (!FmEditDepartment.Execute(this.SelectedDepartment))
                    return;

                //Сохраняем изменения в базе данных
                Dm.Department.Edit(this.SelectedDepartment);
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

            var questionString = "Are you sure you want to delete the selected and all dependent departments?";
            if (MessageBox.Show(questionString, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //Удаляем подразделение из базы данных
            this.Dm.Department.Delete(this.SelectedDepartment.Id);
            //Удаляем из списка сотрудников этого подразделения
            this._workerList.RemoveAll(x => this.SelectedDepartment.Workers.Contains(x));
            //Удаляем узел дерева подразделения
            this.tvDepartments.SelectedNode.Remove();
        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            try
            {
                var worker = new Worker();
                if (!FmEditWorker.Execute(worker, this.SelectedDepartment))
                    return;

                //Добавляем сотрудника в базу данных
                this.Dm.Worker.Add(worker);
                //Добавляем в список
                this._workerList.Add(worker);
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

                if (!FmEditWorker.Execute(this.SelectedWorker))
                    return;

                //Вносим изменения в базу данных
                this.Dm.Worker.Edit(this.SelectedWorker);
                //Обновляем таблицу сотрудников
                this.RefreshDgvWorkers();
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

                var questionString = "Do you really want to delete the selected record?";
                if (MessageBox.Show(questionString, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                //Удаляем сотрудника из базы данных
                this.Dm.Worker.Delete(this.SelectedWorker.Id);
                //Удаляем из списка
                this._workerList.Remove(this.SelectedWorker);
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
