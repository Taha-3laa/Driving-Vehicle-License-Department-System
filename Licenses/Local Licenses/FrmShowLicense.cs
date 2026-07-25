using DLVDBusinessLayer;
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
    public partial class FrmShowLicense : Form
    {
        ClsLocalLicense License;
        public FrmShowLicense(int ID,bool ByLicenseID = false)
        {
            InitializeComponent();
            
            if (!ByLicenseID)
                License = ClsLocalLicense.FindByLocalDrivingAppID(ID);
            else
                License = ClsLocalLicense.Find(ID);
        }          
        public FrmShowLicense(ClsLocalLicense License)
        {
            InitializeComponent();
            
            this.License = License;
        }       


        private void FrmShowLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseinfo1.ShowLicenseInfo(License);
        }
    }
}
