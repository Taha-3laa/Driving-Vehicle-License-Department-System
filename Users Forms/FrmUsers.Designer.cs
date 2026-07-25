namespace Drives_and_Vehicles_License
{
    partial class FrmUsers
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUsers));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddorEdit = new System.Windows.Forms.Button();
            this.txSearch = new System.Windows.Forms.TextBox();
            this.Cb_Filter = new System.Windows.Forms.ComboBox();
            this.GV_Users = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ViewUserDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.AddNewUser = new System.Windows.Forms.ToolStripMenuItem();
            this.EditUserInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteUser = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.PhoneUser = new System.Windows.Forms.ToolStripMenuItem();
            this.SendEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbIsActive = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GV_Users)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 28);
            this.label2.TabIndex = 12;
            this.label2.Text = "Filter By";
            // 
            // btnAddorEdit
            // 
            this.btnAddorEdit.Image = ((System.Drawing.Image)(resources.GetObject("btnAddorEdit.Image")));
            this.btnAddorEdit.Location = new System.Drawing.Point(698, 180);
            this.btnAddorEdit.Name = "btnAddorEdit";
            this.btnAddorEdit.Size = new System.Drawing.Size(59, 47);
            this.btnAddorEdit.TabIndex = 11;
            this.btnAddorEdit.UseVisualStyleBackColor = true;
            this.btnAddorEdit.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txSearch
            // 
            this.txSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txSearch.Location = new System.Drawing.Point(218, 190);
            this.txSearch.Name = "txSearch";
            this.txSearch.Size = new System.Drawing.Size(161, 34);
            this.txSearch.TabIndex = 10;
            this.txSearch.Visible = false;
            this.txSearch.TextChanged += new System.EventHandler(this.txSearch_TextChanged);
            this.txSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txSearch_KeyPress);
            // 
            // Cb_Filter
            // 
            this.Cb_Filter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cb_Filter.FormattingEnabled = true;
            this.Cb_Filter.Items.AddRange(new object[] {
            "None",
            "UserID",
            "PersonID",
            "FullName",
            "UserName",
            "IsActive"});
            this.Cb_Filter.Location = new System.Drawing.Point(95, 190);
            this.Cb_Filter.Name = "Cb_Filter";
            this.Cb_Filter.Size = new System.Drawing.Size(117, 36);
            this.Cb_Filter.TabIndex = 9;
            this.Cb_Filter.SelectedIndexChanged += new System.EventHandler(this.Cb_Filter_SelectedIndexChanged);
            // 
            // GV_Users
            // 
            this.GV_Users.AllowUserToAddRows = false;
            this.GV_Users.AllowUserToDeleteRows = false;
            this.GV_Users.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.GV_Users.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.GV_Users.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.GV_Users.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GV_Users.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.GV_Users.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GV_Users.ContextMenuStrip = this.contextMenuStrip1;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.GV_Users.DefaultCellStyle = dataGridViewCellStyle1;
            this.GV_Users.Location = new System.Drawing.Point(12, 232);
            this.GV_Users.Name = "GV_Users";
            this.GV_Users.ReadOnly = true;
            this.GV_Users.RowHeadersVisible = false;
            this.GV_Users.RowHeadersWidth = 66;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GV_Users.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.GV_Users.RowTemplate.Height = 30;
            this.GV_Users.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.GV_Users.Size = new System.Drawing.Size(745, 373);
            this.GV_Users.TabIndex = 8;
            this.GV_Users.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.GV_Users_DataBindingComplete);
            this.GV_Users.DoubleClick += new System.EventHandler(this.GV_Users_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewUserDetails,
            this.toolStripMenuItem1,
            this.AddNewUser,
            this.EditUserInfo,
            this.DeleteUser,
            this.toolStripMenuItem2,
            this.PhoneUser,
            this.SendEmail});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(219, 224);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // ViewUserDetails
            // 
            this.ViewUserDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ViewUserDetails.Image = ((System.Drawing.Image)(resources.GetObject("ViewUserDetails.Image")));
            this.ViewUserDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ViewUserDetails.Name = "ViewUserDetails";
            this.ViewUserDetails.Size = new System.Drawing.Size(218, 30);
            this.ViewUserDetails.Text = "Show Details";
            this.ViewUserDetails.Click += new System.EventHandler(this.ViewUserDetails_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(215, 6);
            // 
            // AddNewUser
            // 
            this.AddNewUser.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddNewUser.Image = ((System.Drawing.Image)(resources.GetObject("AddNewUser.Image")));
            this.AddNewUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AddNewUser.Name = "AddNewUser";
            this.AddNewUser.Size = new System.Drawing.Size(218, 30);
            this.AddNewUser.Text = "Add New User";
            this.AddNewUser.Click += new System.EventHandler(this.AddNewUser_Click);
            // 
            // EditUserInfo
            // 
            this.EditUserInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditUserInfo.Image = ((System.Drawing.Image)(resources.GetObject("EditUserInfo.Image")));
            this.EditUserInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.EditUserInfo.Name = "EditUserInfo";
            this.EditUserInfo.Size = new System.Drawing.Size(218, 30);
            this.EditUserInfo.Text = "Edit ";
            this.EditUserInfo.Click += new System.EventHandler(this.EditUserInfo_Click);
            // 
            // DeleteUser
            // 
            this.DeleteUser.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteUser.Image = ((System.Drawing.Image)(resources.GetObject("DeleteUser.Image")));
            this.DeleteUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteUser.Name = "DeleteUser";
            this.DeleteUser.Size = new System.Drawing.Size(218, 30);
            this.DeleteUser.Text = "Delete ";
            this.DeleteUser.Click += new System.EventHandler(this.DeleteUser_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(215, 6);
            // 
            // PhoneUser
            // 
            this.PhoneUser.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PhoneUser.Image = ((System.Drawing.Image)(resources.GetObject("PhoneUser.Image")));
            this.PhoneUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PhoneUser.Name = "PhoneUser";
            this.PhoneUser.Size = new System.Drawing.Size(218, 30);
            this.PhoneUser.Text = "Phone Call";
            this.PhoneUser.Click += new System.EventHandler(this.PhoneUser_Click);
            // 
            // SendEmail
            // 
            this.SendEmail.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SendEmail.Image = ((System.Drawing.Image)(resources.GetObject("SendEmail.Image")));
            this.SendEmail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SendEmail.Name = "SendEmail";
            this.SendEmail.Size = new System.Drawing.Size(218, 30);
            this.SendEmail.Text = "Send Email";
            this.SendEmail.Click += new System.EventHandler(this.SendEmail_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(252, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(279, 54);
            this.label1.TabIndex = 7;
            this.label1.Text = "Manage Users";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Drives_and_Vehicles_License.Properties.Resources.Users;
            this.pictureBox1.Location = new System.Drawing.Point(344, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(72, 72);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // cmbIsActive
            // 
            this.cmbIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsActive.FormattingEnabled = true;
            this.cmbIsActive.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cmbIsActive.Location = new System.Drawing.Point(218, 190);
            this.cmbIsActive.Name = "cmbIsActive";
            this.cmbIsActive.Size = new System.Drawing.Size(117, 36);
            this.cmbIsActive.TabIndex = 14;
            this.cmbIsActive.SelectedIndexChanged += new System.EventHandler(this.cmbIsActive_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 608);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 28);
            this.label3.TabIndex = 15;
            this.label3.Text = "#Records";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Location = new System.Drawing.Point(111, 608);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(93, 28);
            this.lblRecords.TabIndex = 15;
            this.lblRecords.Text = "#Records";
            // 
            // FrmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 645);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmbIsActive);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddorEdit);
            this.Controls.Add(this.Cb_Filter);
            this.Controls.Add(this.GV_Users);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txSearch);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmUsers";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Manage Users";
            this.Load += new System.EventHandler(this.FrmUsers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GV_Users)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddorEdit;
        private System.Windows.Forms.TextBox txSearch;
        private System.Windows.Forms.ComboBox Cb_Filter;
        private System.Windows.Forms.DataGridView GV_Users;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cmbIsActive;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ViewUserDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem AddNewUser;
        private System.Windows.Forms.ToolStripMenuItem EditUserInfo;
        private System.Windows.Forms.ToolStripMenuItem DeleteUser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem PhoneUser;
        private System.Windows.Forms.ToolStripMenuItem SendEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecords;
    }
}