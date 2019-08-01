namespace TestManagerClient.Forms
{
    partial class FmEditDepartment
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbParentDepartment = new System.Windows.Forms.ComboBox();
            this.cbIsRoot = new System.Windows.Forms.CheckBox();
            this.lblParentDepartment = new System.Windows.Forms.Label();
            this.lblNameDepartment = new System.Windows.Forms.Label();
            this.tbNameDepartment = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(321, 108);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(429, 108);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbParentDepartment
            // 
            this.cbParentDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbParentDepartment.DisplayMember = "NameDepartment";
            this.cbParentDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParentDepartment.FormattingEnabled = true;
            this.cbParentDepartment.Location = new System.Drawing.Point(149, 75);
            this.cbParentDepartment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbParentDepartment.Name = "cbParentDepartment";
            this.cbParentDepartment.Size = new System.Drawing.Size(379, 24);
            this.cbParentDepartment.TabIndex = 10;
            this.cbParentDepartment.ValueMember = "Id";
            // 
            // cbIsRoot
            // 
            this.cbIsRoot.AutoSize = true;
            this.cbIsRoot.Location = new System.Drawing.Point(149, 47);
            this.cbIsRoot.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbIsRoot.Name = "cbIsRoot";
            this.cbIsRoot.Size = new System.Drawing.Size(136, 21);
            this.cbIsRoot.TabIndex = 9;
            this.cbIsRoot.Text = "Root department";
            this.cbIsRoot.UseVisualStyleBackColor = true;
            this.cbIsRoot.CheckedChanged += new System.EventHandler(this.checkboxUpper_CheckedChanged);
            // 
            // lblParentDepartment
            // 
            this.lblParentDepartment.AutoSize = true;
            this.lblParentDepartment.Location = new System.Drawing.Point(16, 78);
            this.lblParentDepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblParentDepartment.Name = "lblParentDepartment";
            this.lblParentDepartment.Size = new System.Drawing.Size(125, 17);
            this.lblParentDepartment.TabIndex = 6;
            this.lblParentDepartment.Text = "Name department:";
            // 
            // lblNameDepartment
            // 
            this.lblNameDepartment.AutoSize = true;
            this.lblNameDepartment.Location = new System.Drawing.Point(16, 18);
            this.lblNameDepartment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNameDepartment.Name = "lblNameDepartment";
            this.lblNameDepartment.Size = new System.Drawing.Size(125, 17);
            this.lblNameDepartment.TabIndex = 7;
            this.lblNameDepartment.Text = "Name department:";
            // 
            // tbNameDepartment
            // 
            this.tbNameDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNameDepartment.Location = new System.Drawing.Point(149, 15);
            this.tbNameDepartment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbNameDepartment.MaxLength = 50;
            this.tbNameDepartment.Name = "tbNameDepartment";
            this.tbNameDepartment.Size = new System.Drawing.Size(379, 22);
            this.tbNameDepartment.TabIndex = 8;
            // 
            // FmEditDepartment
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(545, 150);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbParentDepartment);
            this.Controls.Add(this.cbIsRoot);
            this.Controls.Add(this.lblParentDepartment);
            this.Controls.Add(this.lblNameDepartment);
            this.Controls.Add(this.tbNameDepartment);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FmEditDepartment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit Department";
            this.Load += new System.EventHandler(this.EditDepartmentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cbParentDepartment;
        private System.Windows.Forms.CheckBox cbIsRoot;
        private System.Windows.Forms.Label lblParentDepartment;
        private System.Windows.Forms.Label lblNameDepartment;
        private System.Windows.Forms.TextBox tbNameDepartment;
    }
}