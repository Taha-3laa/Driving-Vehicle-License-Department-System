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
    public partial class FrmIssueLicense : Form
    {
        public delegate void IssueLicense(ref ClsLocalDrivingLicenseApplication LDLApp);
        public event IssueLicense Issued;

        ClsLocalDrivingLicenseApplication LDLApp;     
        int LocalDrivingLicAppID;
        public FrmIssueLicense(int localDrivingLicAppID)
        {
            InitializeComponent();
            LocalDrivingLicAppID = localDrivingLicAppID;    
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            ClsDriver Driver = ClsDriver.Find(PersonID:ctrlApplicationInfo1.Person.PersonID);

            if (Driver == null)
            {
                Driver = ClsDriver.InsertNewDriver(ctrlApplicationInfo1.Person.PersonID, ClsGlobal.CurrentUser.UserID, DateTime.Now);
            }

            DateTime ExpirationDate =  DateTime.Now.AddYears(LicenseClass.ValidityLength(LDLApp.DrivingClass));

            ClsLocalLicense License = new ClsLocalLicense(LDLApp.ApplicationID, Driver.DriverID, LDLApp.DrivingClass, DateTime.Now, ExpirationDate, txNotes.Text
                , LDLApp.PaidFees, enIssueReason.enFirstTime,ClsGlobal.CurrentUser.UserID);
            if (License.Save())
            {
                MessageBox.Show($"License Saved Successfully With ID {License.LicenseID} ", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Issued?.Invoke(ref LDLApp);
                btnIssue.Enabled = false;
            }
            else
                MessageBox.Show($"License Does Not Save Successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmIssueLicense_Load(object sender, EventArgs e)
        {
            ctrlApplicationInfo1.ViewApplicationInfo(LocalDrivingLicAppID);
            LDLApp = ctrlApplicationInfo1.LocalDrivingLicenseApp;
        }
    }
}
