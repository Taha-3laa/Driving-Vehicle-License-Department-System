using DLVDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls.WebParts;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class CtrlApplicationInfo : UserControl
    {
        public ClsLocalDrivingLicenseApplication LocalDrivingLicenseApp;
        public ClsPerson Person;
        
        public int PassedTests;
        public CtrlApplicationInfo()
        {
            InitializeComponent();
        }
        void ShowApplicationInfo () 
        {
            PassedTests = ClsTest.GetPassedTests(LocalDrivingLicenseApp.LocalDrivingLicenseAppID);
            lblLocalID.Text = LocalDrivingLicenseApp.LocalDrivingLicenseAppID.ToString();
            lblDrivingClass.Text = LicenseClass.LicenseName(LocalDrivingLicenseApp.DrivingClass);
            lblPassedTests.Text = PassedTests.ToString()+" / 3";

            lblAppID.Text = LocalDrivingLicenseApp.ApplicationID.ToString();
            lblStatus.Text = LocalDrivingLicenseApp.AppStatus.ToString();
            lblFees.Text = LocalDrivingLicenseApp.PaidFees.ToString();
            lblType.Text = ClsApplicationType.GetApplicationName(LocalDrivingLicenseApp.AppType);
            lblApplicant.Text = LocalDrivingLicenseApp.ApplicationID.ToString(); 
            lblData.Text = LocalDrivingLicenseApp.Date.ToString("dd/MM/yyyy");
            lblLastStatusDate.Text = LocalDrivingLicenseApp.LastStatusDate.ToString("dd/MM/yyyy");
            lblUserName.Text = ClsUser.GetUserName(LocalDrivingLicenseApp.CreatedByUser);
            lblApplicant.Text = Person.FullName;
        }

        public void ViewApplicationInfo(int ApplicationID) 
        {
            LocalDrivingLicenseApp = ClsLocalDrivingLicenseApplication.Find(ApplicationID);
            if (LocalDrivingLicenseApp != null) 
            {
                Person = ClsPerson.Find(PersonID:LocalDrivingLicenseApp.ApplicationPersonID);
                ShowApplicationInfo();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Person != null)       
            {
                using (FrmShowPersonInfo showPersonInfo = new FrmShowPersonInfo(ref Person))
                { 
                    showPersonInfo.ShowDialog();
                    lblApplicant.Text = Person.FullName;
                } 
            } 
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClsLocalLicense License = ClsLocalLicense.FindByLocalDrivingAppID(LocalDrivingLicenseApp.LocalDrivingLicenseAppID);

            if (License != null)
            {
                using (FrmShowLicense frm = new FrmShowLicense(License))
                    frm.ShowDialog();
            }
            else 
            {
                MessageBox.Show("There is Not License Found !" ,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
