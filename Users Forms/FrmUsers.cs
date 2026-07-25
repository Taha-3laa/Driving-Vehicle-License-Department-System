using DLVDBusinessLayer;
using Drives_and_Vehicles_License.Users_Forms;
using System;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmUsers : Form
    {      
        DataView UsersView;
        DataRowView UserRecord;
        int Index;

        FrmAddUser AddOrEditUser;
        FrmUserInfo UserInfo;
        public FrmUsers()
        {
            InitializeComponent();
        }
        void ReloadUsersData()
        {
            UsersView = ClsUser.GetAllUsers().DefaultView;
            GV_Users.DataSource = UsersView;
        }
        private void FrmUsers_Load(object sender, EventArgs e)
        {
            ReloadUsersData();
            Cb_Filter.SelectedIndex = 0;
        }
        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearch.Text))
            {
                UsersView.RowFilter = "";
                GV_Users.DataSource = UsersView;
                return;
            }

            if (Cb_Filter.SelectedIndex <= 2)
            {
                UsersView.RowFilter = $"{Cb_Filter.SelectedItem} = {Convert.ToInt32(txSearch.Text)}";
            }
            else
            {
                UsersView.RowFilter = $"{Cb_Filter.SelectedItem} like '{txSearch.Text}%'";
            }

            GV_Users.DataSource = UsersView;

        }
        private void Cb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_Filter.SelectedIndex == 0)
                txSearch.Visible = cmbIsActive.Visible = false;

            else if (Cb_Filter.SelectedIndex < 5)
            {
                txSearch.Visible = true;
                cmbIsActive.Visible = false;
            }
            else
            {
                txSearch.Visible = false;
                cmbIsActive.Visible = true;
                cmbIsActive.SelectedIndex = 0;
            }
       
        }
        private void cmbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbIsActive.SelectedIndex == 0) 
            {
                UsersView.RowFilter = "";
                return;
            }
            //active
            if (cmbIsActive.SelectedIndex == 1)
            UsersView.RowFilter = "IsActive = true";
            else 
            UsersView.RowFilter = "IsActive = false";
        }
        private void txSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // number
            if (Cb_Filter.SelectedIndex <= 2)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else 
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
         
        }
        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GetUserRecord();
        }
        void AddUserToView(ref ClsUser User)
        {
            if (User != null) 
            {
                DataRow NewUser = UsersView.Table.NewRow();

                NewUser[0] = User.UserID;
                NewUser[1] = User.Person.PersonID;
                NewUser[2] = User.Person.FullName;
                NewUser[3] = User.UserName;
                NewUser[4] = User.IsActive;

                UsersView.Table.Rows.Add(NewUser);
                GV_Users.Refresh();
            }
        }
        void UpdateUserRecord (ref ClsUser User)
        {
            if (User != null) 
            {
                UsersView.Table.Columns[2].ReadOnly = false;
                UserRecord[2] = User.Person.FullName;
                UserRecord[3] = User.UserName;
                UserRecord[4] = User.IsActive;

                UserRecord.EndEdit();
                GV_Users.RefreshEdit();
            }
        }

        ClsUser User;
        void UpdateUser () 
        {
            User = ClsUser.Find(UserID:Convert.ToInt32(UserRecord[0]));
            using (AddOrEditUser = new FrmAddUser(ref User, ref UsersView))
            {
                AddOrEditUser.UserAdded += UpdateUserRecord;
                AddOrEditUser.ShowDialog();
            }       
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (AddOrEditUser = new FrmAddUser(ref UsersView))
            {
                AddOrEditUser.UserAdded += AddUserToView;
                AddOrEditUser.ShowDialog();
            }
        }
        private void AddNewUser_Click(object sender, EventArgs e)
        {
            btnAddorEdit.PerformClick();
        }
        private void EditUserInfo_Click(object sender, EventArgs e)
        {   
            UpdateUser();           
        }
        private void ViewUserDetails_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(UserRecord[0]);
            using (UserInfo = new FrmUserInfo(UserID))
            {
                UserInfo.ShowDialog();
                UpdateUserRecord(ref User);
            }
        }
        private void DeleteUser_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(UserRecord[0]);

            if (ClsGlobal.CurrentUser.UserID == UserID) 
            {
                MessageBox.Show("You Can Not Delete Current User !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show ($"Are You Sure You Want To Delete This User With ID {UserID}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information) ==DialogResult.OK) 
            {
                if (ClsUser.Delete(UserID))
                {
                    UsersView[Index].Delete();
                    UsersView.Table.AcceptChanges();
                    MessageBox.Show($"User With ID {UserID} Deleted Successfully !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show($"User With ID {UserID} Not Deleted !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void PhoneUser_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet");
        }
        private void SendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet");
        }

        void GetUserRecord () 
        {
            Index = GV_Users.SelectedRows[0].Index;
            if (Index >= 0)
            {
                UserRecord = UsersView[Index];
            }
        }
        private void GV_Users_DoubleClick(object sender, EventArgs e)
        {
            GetUserRecord();
            ViewUserDetails.PerformClick();
        }

        private void GV_Users_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblRecords.Text = GV_Users.Rows.Count.ToString();
        }
    } 
}
