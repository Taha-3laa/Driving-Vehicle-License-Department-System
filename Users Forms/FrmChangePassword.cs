using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmChangePassword : Form
    {
        public FrmChangePassword()
        {
            InitializeComponent();
        }

        private void txCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txCurrentPassword.Text != ClsGlobal.CurrentUser.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txCurrentPassword, "Invalid Password,Enter Correct Password");
                txCurrentPassword.Focus();
            }
            else
            { 
                e.Cancel = false;
                errorProvider1.SetError(txCurrentPassword, "");
            }
        }

        private void txNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txNewPassword.Text.Length < 4 )
            {
                e.Cancel = true;
                errorProvider1.SetError(txNewPassword, "Short Password,Enter Long One");
                txNewPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txNewPassword, "");
            }
        }

        private void txConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txConfirmPassword.Text != txNewPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txConfirmPassword, "Invliad Password");
                txConfirmPassword.Focus();
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txConfirmPassword, "");
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txCurrentPassword.Text) || string.IsNullOrEmpty(txNewPassword.Text) || string.IsNullOrEmpty(txConfirmPassword.Text))
            { 
                MessageBox.Show("Failed To Change Password", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ClsGlobal.CurrentUser.Password = txNewPassword.Text;
            if (ClsGlobal.CurrentUser.Save()) 
            {
                MessageBox.Show("Password Changed Successfully!","Done",MessageBoxButtons.OK, MessageBoxIcon.Information);              
            }
             
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {
    
        }

        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserCardInfo1.ViewUser(ClsGlobal.CurrentUser.UserID);
        }
    }
}
