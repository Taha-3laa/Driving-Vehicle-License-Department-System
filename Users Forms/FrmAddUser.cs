using System;
using DLVDBusinessLayer;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;

namespace Drives_and_Vehicles_License.Users_Forms
{
    public partial class FrmAddUser : Form
    {
        bool AllowMovingToPages = false;

        public delegate void SendUserBack(ref ClsUser User);
        public event SendUserBack UserAdded;

        DataView UsersView;
        ClsUser User;
        ClsPerson Person;
        public FrmAddUser(ref ClsUser User,ref DataView UsersView)
        {
            InitializeComponent();
            this.User = User;
            this.UsersView = UsersView;
        }
        public FrmAddUser(ref DataView UsersView) 
        {
            InitializeComponent();
            this.User = null;
            this.UsersView = UsersView; 
        }
        void EnableNavigationControls(ClsPerson Person)
        {
            btnSave.Enabled = btnNext.Enabled = true;
            this.Person = Person;
        }
        void EnableFormMode()
        {
            if (User != null)
            {
                lblTitle.Text = this.Text = "Update User";

                ctrlPersonSelecter1.txSearch.Text = User.Person.PersonID.ToString();
                ctrlPersonSelecter1.btnSearch.PerformClick();
                ctrlPersonSelecter1.panel1.Enabled = false; 

                lblUserID.Text = User.UserID.ToString();
                txUserName.Text = User.UserName;
                txPassword.Text = User.Password;
                txConfirmPassword.Text = User.Password;
                chkIsActive.Checked = User.IsActive;
            }
            else 
            {
                lblTitle.Text = this.Text = "Add New User";
            }
        }
        private void FrmAddUser_Load(object sender, EventArgs e)
        {
            ctrlPersonSelecter1.OnPersonFound += EnableNavigationControls;
            EnableFormMode();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            //Find Person if it is user
            if (User == null)
            {
                UsersView.Sort = "PersonID";
                if (UsersView.Find(Person.PersonID) != -1)
                {
                    MessageBox.Show("This Person Is Already User", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            AllowMovingToPages = true;
            ++TabControl.SelectedIndex;
            AllowMovingToPages = false;
        }
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !AllowMovingToPages;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            AllowMovingToPages = true;
            --TabControl.SelectedIndex;
            AllowMovingToPages = false;
        }
        private void ValidateInputs(object sender, KeyPressEventArgs e)
        {
            TextBox tx = sender as TextBox;
            if (tx.TabIndex > 2)
                e.Handled = char.IsLetter(e.KeyChar);
            else
                e.Handled = char.IsDigit(e.KeyChar);
        }
        private void txUserName_Validating(object sender, CancelEventArgs e)
        {
            if (TabControl.SelectedIndex == 0 ||User != null && User.UserName == txUserName.Text)
                return;

            bool IsValidLength = txUserName.Text.Length >= 4;

            UsersView.Sort = "UserName";
            if (!IsValidLength || UsersView.Find(txUserName.Text.ToUpper()) != -1)
            {
                e.Cancel = true;
                errorProvider1.SetError(txUserName, (!IsValidLength ? "Invalid Length Min[4],Max[18]" : "Invalid UserName"));
                txUserName.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txUserName, "");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void txPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txPassword.Text) || txPassword.TextLength < 4)
            {
                e.Cancel = true;
                errorProvider1.SetError(txPassword, "Invalid Password Min(4),Max(18)");
                txPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txPassword, "");
            }
        }
        private void txConfirmPassword_Validating(object sender, CancelEventArgs e)
        {                  
            if (txPassword.TextLength == 0|| txConfirmPassword.Text != txPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txConfirmPassword, "No Correct Password");
                txConfirmPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txConfirmPassword, "");
            }
        } 
        bool AreLoginFieldEmpty()
        {
            return (txUserName.TextLength == 0 || txPassword.TextLength == 0);
        }
      
        private void btnSave_Click(object sender, EventArgs e)
        {
            string Mess;
            if (User != null)
            {
                User.UserName = txUserName.Text;
                if(!string.IsNullOrEmpty(txUserName.Text))
                    User.Password = txPassword.Text;
                User.IsActive = chkIsActive.Checked;
                User.Person = Person;
                User.Save();
                Mess = $"User Updated Successfully With ID {User.UserID}!";
            }
            else
            {
                if (AreLoginFieldEmpty())
                {
                    MessageBox.Show("Login Fields Are Empty!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                User = new ClsUser(Person, txUserName.Text, txPassword.Text, chkIsActive.Checked);
                User.Save();
                lblUserID.Text = User.UserID.ToString();
                Mess = $"User Added Successfully With ID {User.UserID}";
            }
            UserAdded?.Invoke(ref User);
            MessageBox.Show(Mess, "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnSave.Enabled = false;
        }
    }
}

