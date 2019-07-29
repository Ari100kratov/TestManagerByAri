namespace TestManagerClient.Forms
{
    partial class AddDepartmentForm
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
            this.tbNameDepartment = new System.Windows.Forms.TextBox();
            this.lblNameDepartment = new System.Windows.Forms.Label();
            this.checkboxUpper = new System.Windows.Forms.CheckBox();
            this.lblParentDepartment = new System.Windows.Forms.Label();
            this.cbParentDepartment = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbNameDepartment
            // 
            this.tbNameDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNameDepartment.Location = new System.Drawing.Point(112, 12);
            this.tbNameDepartment.MaxLength = 50;
            this.tbNameDepartment.Name = "tbNameDepartment";
            this.tbNameDepartment.Size = new System.Drawing.Size(285, 20);
            this.tbNameDepartment.TabIndex = 1;
            // 
            // lblNameDepartment
            // 
            this.lblNameDepartment.AutoSize = true;
            this.lblNameDepartment.Location = new System.Drawing.Point(12, 15);
            this.lblNameDepartment.Name = "lblNameDepartment";
            this.lblNameDepartment.Size = new System.Drawing.Size(94, 13);
            this.lblNameDepartment.TabIndex = 1;
            this.lblNameDepartment.Text = "Name department:";
            // 
            // checkboxUpper
            // 
            this.checkboxUpper.AutoSize = true;
            this.checkboxUpper.Location = new System.Drawing.Point(112, 38);
            this.checkboxUpper.Name = "checkboxUpper";
            this.checkboxUpper.Size = new System.Drawing.Size(105, 17);
            this.checkboxUpper.TabIndex = 2;
            this.checkboxUpper.Text = "Root department";
            this.checkboxUpper.UseVisualStyleBackColor = true;
            this.checkboxUpper.CheckedChanged += new System.EventHandler(this.checkboxUpper_CheckedChanged);
            // 
            // lblParentDepartment
            // 
            this.lblParentDepartment.AutoSize = true;
            this.lblParentDepartment.Location = new System.Drawing.Point(12, 63);
            this.lblParentDepartment.Name = "lblParentDepartment";
            this.lblParentDepartment.Size = new System.Drawing.Size(94, 13);
            this.lblParentDepartment.TabIndex = 1;
            this.lblParentDepartment.Text = "Name department:";
            // 
            // cbParentDepartment
            // 
            this.cbParentDepartment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbParentDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParentDepartment.FormattingEnabled = true;
            this.cbParentDepartment.Location = new System.Drawing.Point(112, 61);
            this.cbParentDepartment.Name = "cbParentDepartment";
            this.cbParentDepartment.Size = new System.Drawing.Size(285, 21);
            this.cbParentDepartment.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(322, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(241, 88);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // AddDepartmentForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(409, 122);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbParentDepartment);
            this.Controls.Add(this.checkboxUpper);
            this.Controls.Add(this.lblParentDepartment);
            this.Controls.Add(this.lblNameDepartment);
            this.Controls.Add(this.tbNameDepartment);
            this.Name = "AddDepartmentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Department";
            this.Load += new System.EventHandler(this.AddDepartmentForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbNameDepartment;
        private System.Windows.Forms.Label lblNameDepartment;
        private System.Windows.Forms.CheckBox checkboxUpper;
        private System.Windows.Forms.Label lblParentDepartment;
        private System.Windows.Forms.ComboBox cbParentDepartment;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
    }
}