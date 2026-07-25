using DLVDBusinessLayer;
using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmMain : Form
    {
        FrmUsers Users = new FrmUsers();
        FrmApplicationType ApplicationsType = new FrmApplicationType();
        FrmTestTypes TestTypes = new FrmTestTypes();
        FrmAddLocalLicenseApp FrmAddLocalLicenseApp = new FrmAddLocalLicenseApp();
        FrmUserInfo UserInfo;
        public FrmMain()
        {
            InitializeComponent();
        }            
        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReleaseDetainedLicense frm = new FrmReleaseDetainedLicense();
            frm.ShowDialog();   
        }

        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmPeople frm = new FrmPeople())
                frm.ShowDialog();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmChangePassword frm = new FrmChangePassword())
                frm.ShowDialog();
        }

        private void manageApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmApplicationType frm = new FrmApplicationType())
                frm.ShowDialog();
        }

        private void manageTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmTestTypes frm = new FrmTestTypes())
                frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmUsers frm = new FrmUsers())
                frm.ShowDialog();
        }

        private void currentUserInformationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (UserInfo = new FrmUserInfo(ClsGlobal.CurrentUser.UserID))
                UserInfo.ShowDialog();
        }

        private void localDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAddLocalLicenseApp frm = new FrmAddLocalLicenseApp())
                frm.ShowDialog();
        }

        private void manageApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageDetainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ManageDetainedLicenses frm = new ManageDetainedLicenses())
                frm.ShowDialog();
        }

        private void detainLicensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmDetainLicense frm = new FrmDetainLicense())
               frm.ShowDialog();
        }

        private void releaseDetainLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FrmReleaseDetainedLicense frm = new FrmReleaseDetainedLicense())
                frm.ShowDialog();
        }

        private void localDrivingApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManagLocalApplications frm = new FrmManagLocalApplications())
                frm.ShowDialog();
        }

        private void internationalDrivingApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManageInternationalLicenses frm = new FrmManageInternationalLicenses())
                frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmShowDrivers frmShowDrivers = new FrmShowDrivers())
                frmShowDrivers.ShowDialog();
        }

        private void internationalDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmIssueInternationalLicense frm = new FrmIssueInternationalLicense())
                frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmRenewDrivingLicense frm = new FrmRenewDrivingLicense())
                frm.ShowDialog();
        }

        private void replacementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmReplacementforLostorDamaged frm = new FrmReplacementforLostorDamaged())
                frm.ShowDialog();   
        }

        private void driveringToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmManagLocalApplications frm = new FrmManagLocalApplications()) 
            frm.ShowDialog();
        }
    }
}
