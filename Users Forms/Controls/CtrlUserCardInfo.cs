using DLVDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class CtrlUserCardInfo : UserControl
    {
        ClsPerson Person;
        ClsUser User;
        public CtrlUserCardInfo()
        {
            InitializeComponent();
        }
        void ShowUserInfo () 
        {
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblIsActive.Text = User.IsActive ? "Yes":"No";       
        }
        void ShowPersonInfo() 
        {
            ctrlPersonCardInfo1.ViewPerson(ref User);
        }

        public void ViewUser(int UserID) 
        {
            this.User = ClsUser.Find(UserID:UserID);            
            ShowUserInfo();
            ShowPersonInfo();
        }

    }
}
