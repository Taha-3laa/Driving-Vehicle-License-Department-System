using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmShowApplicationInfo : Form
    {
        int LocalDrivingLicenseAppID; 
        public FrmShowApplicationInfo(int localDrivingLicenseAppID)
        {
            InitializeComponent();
            LocalDrivingLicenseAppID = localDrivingLicenseAppID;    
        }

        private void FrmShowApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.ViewApplicationInfo(LocalDrivingLicenseAppID);
        }
    }
}
