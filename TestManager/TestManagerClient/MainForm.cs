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
        internal Worker AddedWorker = null;
        internal Department AddedDepartment = null;

        public MainForm()
        {
            InitializeComponent();
            this.dgvWorkers.AutoGenerateColumns = false;
        }

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

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.WorkerBindingList = new BindingList<Worker>(Program.TMWcfService.GetAllWorkers().ToList());
            this.DepartmentList = Program.TMWcfService.GetAllDepartments().ToList();
            this.FillTreeView();
        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            var addDepartmentForm = new AddDepartmentForm(this);

            if (addDepartmentForm.ShowDialog() == DialogResult.Cancel)
                return;

            if (this.AddedDepartment.ParentId == 0)
            {
                var parentNode = new TreeNode
                {
                    Text = this.AddedDepartment.NameDepartment,
                    Tag = this.AddedDepartment,
                    Name = this.AddedDepartment.Id.ToString()
                };
                this.tvDepartments.Nodes.Add(parentNode);
            }
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
        }

        private void btnEditDepartment_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {

        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            var addWorkerForm = new AddWorkerForm(this);

            if (addWorkerForm.ShowDialog() == DialogResult.Cancel)
                return;

            this.WorkerBindingList.Add(this.AddedWorker);
            this.AddedWorker = null;

        }

        private void btnEditWorker_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedWorker = this.dgvWorkers.CurrentRow.DataBoundItem as Worker;

                if (selectedWorker == null)
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var editWorkerForm = new EditWorkerForm(selectedWorker);
                if (editWorkerForm.ShowDialog() == DialogResult.OK)
                    this.dgvWorkers.DataSource = new BindingList<Worker>(this.WorkerBindingList.Where
                        (x => x.DepartmentId == (this.tvDepartments.SelectedNode.Tag as Department).Id).ToList());
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
                var selectedWorker = this.dgvWorkers.CurrentRow.DataBoundItem as Worker;

                if (selectedWorker == null)
                {
                    MessageBox.Show("No record selected", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Do you really want to delete the selected record?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                Program.TMWcfService.DeleteWorker(selectedWorker.Id);
                this.WorkerBindingList.Remove(selectedWorker);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvDepartments.SelectedNode == null || this.tvDepartments.SelectedNode.Tag == null)
            {
                // this.WorkerBindingList = new BindingList<Worker>(Program.TMWcfService.GetAllWorkers().ToList());
                this.dgvWorkers.DataSource = new BindingList<Worker>(Program.TMWcfService.GetAllWorkers().ToList());
            }
            else
            {
                //this.WorkerBindingList = new BindingList<Worker>(this.WorkerBindingList.Where(x => x.DepartmentId == (int)this.tvDepartments.SelectedNode.Tag).ToList());
                this.dgvWorkers.DataSource = new BindingList<Worker>(this.WorkerBindingList.Where(x => x.DepartmentId == (this.tvDepartments.SelectedNode.Tag as Department).Id).ToList());
            }
        }
    }
}
