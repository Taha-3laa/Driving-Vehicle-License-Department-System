using DLVDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmUserInfo : Form
    {
        int UserID;
        public FrmUserInfo(int UserID)
        {
            InitializeComponent();
            this.UserID = UserID;
        }                  
        private void FrmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserCardInfo1.ViewUser(UserID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
