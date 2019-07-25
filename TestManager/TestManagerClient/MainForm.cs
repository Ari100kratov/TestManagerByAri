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
        private List<Worker> WorkerList = new List<Worker>();
        private List<Department> DepartmentList = new List<Department>();
        private Department SelectedDepartment = null;

        public MainForm()
        {
            InitializeComponent();
            this.dgvWorkers.AutoGenerateColumns = false;
            this.FillTvlDepartments();
        }

        private void FillTreeView(TreeNode parentNode = null)
        {
            if (parentNode == null)
            {
                parentNode = new TreeNode();
                parentNode.Text = "Departments";
                parentNode.Tag = null;
                var parentDepartments = this.DepartmentList.Where(t => t.ParentId == 0).ToList();
                foreach (var department in parentDepartments)
                {
                    var node = new TreeNode()
                    {
                        Text = department.NameDepartment,
                        Tag = department.Id,
                    };
                    FillTreeView(node);
                    parentNode.Nodes.Add(node);
                    this.tvDepartments.Nodes.Add(parentNode);
                }
            }
            else
            {
                var id = (int)parentNode.Tag;
                var childDepartments = this.DepartmentList.Where(t => t.ParentId == id).ToList();
                foreach (var department in childDepartments)
                {
                    var node = new TreeNode()
                    {
                        Text = department.NameDepartment,
                        Tag = department.Id,
                    };
                    this.FillTreeView(node);
                    parentNode.Nodes.Add(node);
                }
            }

            this.tvDepartments.ExpandAll();
        }

        private void FillDgvWorkers()
        {
            this.WorkerList = Program.TMWcfService.GetAllWorkers().ToList();
            this.dgvWorkers.DataSource = null;

            if (this.SelectedDepartment != null)
                this.dgvWorkers.DataSource = this.WorkerList.Where(x => x.DepartmentId == this.SelectedDepartment.Id).ToList();
            else
                this.dgvWorkers.DataSource = this.WorkerList;
        }

        private void FillTvlDepartments()
        {
            this.tvDepartments.Nodes.Clear();
            this.SelectedDepartment = null;
            this.DepartmentList = Program.TMWcfService.GetAllDepartments().ToList();
            this.FillTreeView();
            this.FillDgvWorkers();
        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            var addNewWorker = new AddWorkerForm(this.SelectedDepartment);
            if (addNewWorker.ShowDialog() == DialogResult.OK)
                this.FillDgvWorkers();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddDepartment_Click(object sender, EventArgs e)
        {
            var addNewDepatment = new AddDepartmentForm();
            if (addNewDepatment.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Hi");
            }

        }

        private void btnEditDepartment_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteDepartment_Click(object sender, EventArgs e)
        {

        }

        private void btnEditWorker_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteWorker_Click(object sender, EventArgs e)
        {

        }

        private void tvDepartments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvDepartments.SelectedNode.Tag != null)
            {
                this.SelectedDepartment = this.DepartmentList.First(x => x.Id == (int)this.tvDepartments.SelectedNode.Tag);
                this.FillDgvWorkers();
            }
            else
            {
                this.SelectedDepartment = null;
                this.FillDgvWorkers();
            }
        }
    }
}
