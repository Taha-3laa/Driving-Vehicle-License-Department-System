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
    public partial class FrmRenewDrivingLicense : Form
    {
        ClsApplication RenewLicenseApplication;
        ClsLocalLicense NewLocalDrivingLicense;
        int AppFees,LicenseFees;
        DateTime AppDate;
        DateTime ExpirationDate;
        void EnableButtons ()
        {
            LicenseFees = LicenseClass.Fees(ctrlLicenseSelector1.LocalLicense.LicenseClass);
            ExpirationDate = AppDate.AddYears(LicenseClass.ValidityLength(ctrlLicenseSelector1.LocalLicense.LicenseClass));
            
            btnRenew.Enabled = true;
            lblLocalLicID.Text = ctrlLicenseSelector1.LocalLicense.LicenseID.ToString();
            lblLicenseFees.Text = LicenseFees.ToString();
            lblTotalFees.Text = ( AppFees + LicenseFees ).ToString();
            lblExpDate.Text = ExpirationDate.ToString("dd / MM / yyyy");
            LnkShowLicenseHistory.Enabled = true;
        }
        public FrmRenewDrivingLicense()
        {
            InitializeComponent();
        }
        void ShowData () 
        {
            lblAppDate.Text = AppDate.ToString("dd / MM / yyyy");
            lblIssueDate.Text = lblAppDate.Text;
            lblAppFees.Text = AppFees.ToString();
            lblUser.Text = ClsGlobal.CurrentUser.UserName;
        }
        private void FrmRenewDrivingLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseSelector1.OnLicenseFound += EnableButtons;

            AppDate = DateTime.Now;
            AppFees = ClsApplicationType.GetFees((byte)enApplicationType.RenewLocalDrivingLicense);
            ShowData();
        }

        bool IsValid () 
        {
            if (!ctrlLicenseSelector1.LocalLicense.IsActive)
            {
                MessageBox.Show("License Is Not Active ", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;   
            }
            if (ctrlLicenseSelector1.LocalLicense.IsDetained())
            {
                MessageBox.Show("License Is Detained", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if (ctrlLicenseSelector1.LocalLicense.ExpirationDate > DateTime.Now) 
            {
                DateTime ExpirationDate = ctrlLicenseSelector1.LocalLicense.ExpirationDate;
                MessageBox.Show($"License Is Not Expired It Will At {ExpirationDate.Day} / {ExpirationDate.Month} /{ExpirationDate.Year}","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;   
            }
            return true; 
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            if (IsValid())
            { 
                RenewLicenseApplication = new ClsApplication (enApplicationType.RenewLocalDrivingLicense,ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.PersonID,ClsGlobal.CurrentUser.UserID,AppDate,AppFees);
                RenewLicenseApplication.Save();

                NewLocalDrivingLicense = new ClsLocalLicense(RenewLicenseApplication.ApplicationID, ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.DriverID, ctrlLicenseSelector1.LocalLicense.LicenseClass,
                   AppDate, ExpirationDate, textBox1.Text, LicenseFees, enIssueReason.enRenewed, ClsGlobal.CurrentUser.UserID); 

                ctrlLicenseSelector1.LocalLicense.IsActive = false; 
                ctrlLicenseSelector1.LocalLicense.Save();
                NewLocalDrivingLicense.Save();

                lblAppID.Text= RenewLicenseApplication.ApplicationID.ToString();
                lblRenewedID.Text = NewLocalDrivingLicense.LicenseID.ToString();
                lblLocalLicID.Text = ctrlLicenseSelector1.LocalLicense.LicenseID.ToString();
          
                btnRenew.Enabled = false;
                LnkShowLicenseInfo.Enabled = true; 
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (NewLocalDrivingLicense != null)
            {
                using (FrmShowLicense frm = new FrmShowLicense(NewLocalDrivingLicense))
                    frm.ShowDialog();
            }
        }

        private void LnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {      
            using (FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.NationalNumber))
                frm.ShowDialog();          
        }
    }
}
