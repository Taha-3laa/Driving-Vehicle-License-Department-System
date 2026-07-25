using DLVDBusinessLayer;
using System;
using System.Windows.Forms;
using System.IO;
using System.Web.UI.WebControls;

namespace Drives_and_Vehicles_License
{


    public partial class FrmLogin : Form
    {
        ClsUser User;
        FrmMain FrmMain;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            string UserName ="", Password="";
            if (ClsGlobal.GetUserNamePassword(ref UserName, ref Password)) 
            {
                txUserName.Text = UserName;
                txPassword.Text = Password;
                chbRememberMe.Checked = true; 
            }
            else 
                chbRememberMe.Checked = false;
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txPassword.Text) || string.IsNullOrEmpty(txUserName.Text))
            {
                MessageBox.Show("Some Fields Are Empty","Warning",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            User = ClsUser.GetUserByUserNamePassword(txUserName.Text.Trim(), txPassword.Text.Trim());
            
            if (chbRememberMe.Checked)
                ClsGlobal.RememberUserNamePassword(txUserName.Text.Trim(), txPassword.Text.Trim());
            else
                ClsGlobal.DeleteFile();

            if (User != null)
            {
                if (!User.IsActive)
                {
                    MessageBox.Show("Invalid Login User Not Active!", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClsGlobal.CurrentUser = User;
                FrmMain = new FrmMain();
                FrmMain.FormClosed += FrmMain_FormClosed;
                this.Hide();
                FrmMain.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid UserName / Password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
