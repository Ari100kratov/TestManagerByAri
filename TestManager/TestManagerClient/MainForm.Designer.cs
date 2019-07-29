namespace TestManagerClient
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.tvDepartments = new System.Windows.Forms.TreeView();
            this.panel = new System.Windows.Forms.Panel();
            this.btnAddWorker = new System.Windows.Forms.Button();
            this.btnEditWorker = new System.Windows.Forms.Button();
            this.btnDeleteWorker = new System.Windows.Forms.Button();
            this.btnDeleteDepartment = new System.Windows.Forms.Button();
            this.btnEditDepartment = new System.Windows.Forms.Button();
            this.btnAddDepartment = new System.Windows.Forms.Button();
            this.dgvWorkers = new System.Windows.Forms.DataGridView();
            this.colFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAge = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhoneNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).BeginInit();
            this.SuspendLayout();
            // 
            // tvDepartments
            // 
            this.tvDepartments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tvDepartments.HideSelection = false;
            this.tvDepartments.Location = new System.Drawing.Point(12, 12);
            this.tvDepartments.Name = "tvDepartments";
            this.tvDepartments.Size = new System.Drawing.Size(237, 305);
            this.tvDepartments.TabIndex = 0;
            this.tvDepartments.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvDepartments_AfterSelect);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.btnAddWorker);
            this.panel.Controls.Add(this.btnEditWorker);
            this.panel.Controls.Add(this.btnDeleteWorker);
            this.panel.Controls.Add(this.btnDeleteDepartment);
            this.panel.Controls.Add(this.btnEditDepartment);
            this.panel.Controls.Add(this.btnAddDepartment);
            this.panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel.Location = new System.Drawing.Point(0, 323);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(787, 49);
            this.panel.TabIndex = 1;
            // 
            // btnAddWorker
            // 
            this.btnAddWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddWorker.Location = new System.Drawing.Point(538, 14);
            this.btnAddWorker.Name = "btnAddWorker";
            this.btnAddWorker.Size = new System.Drawing.Size(75, 23);
            this.btnAddWorker.TabIndex = 0;
            this.btnAddWorker.Text = "Add";
            this.btnAddWorker.UseVisualStyleBackColor = true;
            this.btnAddWorker.Click += new System.EventHandler(this.btnAddWorker_Click);
            // 
            // btnEditWorker
            // 
            this.btnEditWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEditWorker.Location = new System.Drawing.Point(619, 14);
            this.btnEditWorker.Name = "btnEditWorker";
            this.btnEditWorker.Size = new System.Drawing.Size(75, 23);
            this.btnEditWorker.TabIndex = 0;
            this.btnEditWorker.Text = "Edit";
            this.btnEditWorker.UseVisualStyleBackColor = true;
            this.btnEditWorker.Click += new System.EventHandler(this.btnEditWorker_Click);
            // 
            // btnDeleteWorker
            // 
            this.btnDeleteWorker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteWorker.Location = new System.Drawing.Point(700, 14);
            this.btnDeleteWorker.Name = "btnDeleteWorker";
            this.btnDeleteWorker.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteWorker.TabIndex = 0;
            this.btnDeleteWorker.Text = "Delete";
            this.btnDeleteWorker.UseVisualStyleBackColor = true;
            this.btnDeleteWorker.Click += new System.EventHandler(this.btnDeleteWorker_Click);
            // 
            // btnDeleteDepartment
            // 
            this.btnDeleteDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteDepartment.Location = new System.Drawing.Point(174, 14);
            this.btnDeleteDepartment.Name = "btnDeleteDepartment";
            this.btnDeleteDepartment.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteDepartment.TabIndex = 0;
            this.btnDeleteDepartment.Text = "Delete";
            this.btnDeleteDepartment.UseVisualStyleBackColor = true;
            this.btnDeleteDepartment.Click += new System.EventHandler(this.btnDeleteDepartment_Click);
            // 
            // btnEditDepartment
            // 
            this.btnEditDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEditDepartment.Location = new System.Drawing.Point(93, 14);
            this.btnEditDepartment.Name = "btnEditDepartment";
            this.btnEditDepartment.Size = new System.Drawing.Size(75, 23);
            this.btnEditDepartment.TabIndex = 0;
            this.btnEditDepartment.Text = "Edit";
            this.btnEditDepartment.UseVisualStyleBackColor = true;
            this.btnEditDepartment.Click += new System.EventHandler(this.btnEditDepartment_Click);
            // 
            // btnAddDepartment
            // 
            this.btnAddDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddDepartment.Location = new System.Drawing.Point(12, 14);
            this.btnAddDepartment.Name = "btnAddDepartment";
            this.btnAddDepartment.Size = new System.Drawing.Size(75, 23);
            this.btnAddDepartment.TabIndex = 0;
            this.btnAddDepartment.Text = "Add";
            this.btnAddDepartment.UseVisualStyleBackColor = true;
            this.btnAddDepartment.Click += new System.EventHandler(this.btnAddDepartment_Click);
            // 
            // dgvWorkers
            // 
            this.dgvWorkers.AllowUserToAddRows = false;
            this.dgvWorkers.AllowUserToDeleteRows = false;
            this.dgvWorkers.AllowUserToOrderColumns = true;
            this.dgvWorkers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvWorkers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFirstName,
            this.colLastName,
            this.colAge,
            this.colPhoneNumber});
            this.dgvWorkers.Location = new System.Drawing.Point(255, 12);
            this.dgvWorkers.MultiSelect = false;
            this.dgvWorkers.Name = "dgvWorkers";
            this.dgvWorkers.ReadOnly = true;
            this.dgvWorkers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWorkers.Size = new System.Drawing.Size(520, 305);
            this.dgvWorkers.TabIndex = 2;
            // 
            // colFirstName
            // 
            this.colFirstName.DataPropertyName = "FirstName";
            this.colFirstName.HeaderText = "First Name";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.ReadOnly = true;
            // 
            // colLastName
            // 
            this.colLastName.DataPropertyName = "LastName";
            this.colLastName.HeaderText = "Last Name";
            this.colLastName.Name = "colLastName";
            this.colLastName.ReadOnly = true;
            // 
            // colAge
            // 
            this.colAge.DataPropertyName = "Age";
            this.colAge.HeaderText = "Age";
            this.colAge.Name = "colAge";
            this.colAge.ReadOnly = true;
            // 
            // colPhoneNumber
            // 
            this.colPhoneNumber.DataPropertyName = "PhoneNumber";
            this.colPhoneNumber.HeaderText = "Phone Number";
            this.colPhoneNumber.Name = "colPhoneNumber";
            this.colPhoneNumber.ReadOnly = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 372);
            this.Controls.Add(this.dgvWorkers);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.tvDepartments);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Manager";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvDepartments;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button btnAddWorker;
        private System.Windows.Forms.Button btnEditWorker;
        private System.Windows.Forms.Button btnDeleteWorker;
        private System.Windows.Forms.Button btnDeleteDepartment;
        private System.Windows.Forms.Button btnEditDepartment;
        private System.Windows.Forms.Button btnAddDepartment;
        private System.Windows.Forms.DataGridView dgvWorkers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAge;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhoneNumber;
    }
}

