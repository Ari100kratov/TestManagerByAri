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

namespace TestManagerClient
{
    public partial class MainForm : Form
    {
        private BindingList<Worker> WorkerBindingList = new BindingList<Worker>();
        private List<Department> DepartmentList = new List<Department>();
        private TreeNode SelectedNode = null;
        internal Worker AddedWorker = null;
        internal Department AddedDepartment = null;

        public MainForm()
        {
            InitializeComponent();
            this.dgvWorkers.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Получение всех сотрудников выбранного подразделения и его дочерних подразделений
        /// </summary>
        /// <returns></returns>
        private List<Worker> GetListWorkerInDepartment()
        {
            //Преобразуем последовательность узлов в список Name полученных узлов дерева
            var nameList = GetChildrens(this.tvDepartments.SelectedNode).Select(x => x.Name).ToList();
            //Добавляем в этот список Name выбранного узла
            nameList.Add(this.tvDepartments.SelectedNode.Name);

            return this.WorkerBindingList.Where(x => nameList.Contains(x.DepartmentId.ToString())).ToList();

            //Получаем последовательность дочерних узлов
            IEnumerable<TreeNode> GetChildrens(TreeNode Parent)
            {
                return Parent.Nodes.Cast<TreeNode>().Concat(
                      Parent.Nodes.Cast<TreeNode>().SelectMany(GetChildrens));
            }
        }

        /// <summary>
        /// Рекурсивное заполнение дерева подразделений
        /// </summary>
        /// <param name="parentNode">Родительский узел дерева</param>
        private void FillTreeView(TreeNode parentNode = null)
        {
            if (parentNode == null)
            {
                parentNode = new TreeNode
                {
                    Text = "Departments",
                    Tag = null,
                    Name = "0"
                };

                var parentDepartments = this.DepartmentList.Where(t => t.ParentId == 0).ToList();
                foreach (var department in parentDepartments)
                {
                    var node = new TreeNode()
                    {
                        Text = department.NameDepartment,
                        Tag = department,
                        Name = department.Id.ToString()
                    };
                    FillTreeView(node);
                    parentNode.Nodes.Add(node);
                }
                this.tvDepartments.Nodes.Add(parentNode);
            }
            else
            {
                var id = (parentNode.Tag as Department).Id;
                var childDepartments = this.DepartmentList.Where(t => t.ParentId == id).ToList();
                foreach (var department in childDepartments)
                {
                    var node = new TreeNode()
                    {
                        Text = department.NameDepartment,
                        Tag = department,
                        Name = department.Id.ToString()
                    };
                    this.FillTreeView(node);
                    parentNode.Nodes.Add(node);
                }
            }

            this.tvDepartments.ExpandAll();
        }

        private void RefreshDgvWorkers()
        {
            if (this.SelectedNode == null || this.SelectedNode.Tag == null)
                this.dgvWorkers.DataSource = this.WorkerBindingList;
            else
                this.dgvWorkers.DataSource = new BindingList<Worker>(this.GetListWorkerInDepartment());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                this.WorkerBindingList = new BindingList<Worker>(Program.TMWcfService.GetAllWorkers().ToList());
                this.DepartmentList = Program.TMWcfService.GetAllDepartments().ToList();
                this.FillTreeView();
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
                var addDepartmentForm = new AddDepartmentForm(this, this.SelectedNode.Tag as Department);

                if (addDepartmentForm.ShowDialog() == DialogResult.Cancel)
                    return;

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
        }

        private void btnEditDepartment_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SelectedNode == null || this.SelectedNode.Tag == null)
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Создаем, чтобы получить изменения по ссылке
                var selectedDepartment = this.SelectedNode.Tag as Department;

                var editDepartmentForm = new EditDepartmentForm(selectedDepartment);
                if (editDepartmentForm.ShowDialog() == DialogResult.Cancel)
                    return;

                //Создаем копию устаревшего узла со всеми дочерними элементами
                var changingNode = (TreeNode)this.SelectedNode.Clone();
                //Удаляем устаревший узел
                this.tvDepartments.SelectedNode.Remove();

                //Присваиваем новому узлу актуальные данные
                changingNode.Tag = selectedDepartment;
                changingNode.Text = selectedDepartment.NameDepartment;


                if (selectedDepartment.ParentId == 0)
                    this.tvDepartments.Nodes[0].Nodes.Add(changingNode);
                else
                {
                    //Находим необходимый родительский узел для нового и добавляем в него актуальную ветку
                    var parentNode = this.tvDepartments.Nodes.Find(selectedDepartment.ParentId.ToString(), true).First();
                    parentNode.Nodes.Add(changingNode);
                }

                this.tvDepartments.SelectedNode = changingNode;
                this.tvDepartments.ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {
            if (this.SelectedNode == null || this.SelectedNode.Tag == null)
            {
                MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete the selected and all dependent departments?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //Удаляем сотрудников в выбранном и дочерних подразделениях
            foreach (var worker in this.GetListWorkerInDepartment())
                this.WorkerBindingList.Remove(worker);

            Program.TMWcfService.DeleteDepartment((this.SelectedNode.Tag as Department).Id);
            this.tvDepartments.SelectedNode.Remove();
        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            try
            {
                if (Program.TMWcfService.GetAllDepartments().Count() == 0)
                    return;

                var addWorkerForm = new AddWorkerForm(this, this.SelectedNode.Tag as Department);

                if (addWorkerForm.ShowDialog() == DialogResult.Cancel)
                    return;

                this.WorkerBindingList.Add(this.AddedWorker);
                this.AddedWorker = null;
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
                if (this.dgvWorkers.Rows.Count == 0 || !(this.dgvWorkers.CurrentRow.DataBoundItem is Worker selectedWorker))
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var editWorkerForm = new EditWorkerForm(selectedWorker);
                if (editWorkerForm.ShowDialog() == DialogResult.Cancel)
                    return;

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

                if (this.dgvWorkers.Rows.Count == 0 || !(this.dgvWorkers.CurrentRow.DataBoundItem is Worker selectedWorker))
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Do you really want to delete the selected record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                Program.TMWcfService.DeleteWorker(selectedWorker.Id);
                this.WorkerBindingList.Remove(selectedWorker);
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
                this.SelectedNode = this.tvDepartments.SelectedNode;
                this.RefreshDgvWorkers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
