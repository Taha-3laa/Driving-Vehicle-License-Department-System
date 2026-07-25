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
    public partial class FrmIssueInternationalLicense : Form
    {
        ClsInternationalLicense InterNationalLicense; 
        DateTime AppDate = DateTime.Now;
        int AppFees;
        void EnableIssued()
        {
            btnIssue.Enabled = true;
            lblLocalLicID.Text = ctrlLicenseSelector1.LocalLicense.LicenseID.ToString();
            LnkShowLicenseHistory.Enabled = true;
        } 
        public FrmIssueInternationalLicense()
        {
            InitializeComponent();
        }
        private void ctrlLicenseSelector1_Load(object sender, EventArgs e)
        {
            ctrlLicenseSelector1.OnLicenseFound += EnableIssued;
        }
        void ShowData () 
        {
            lblAppDate.Text = AppDate.ToString("dd / MM / yyyy");
            lblIssueDate.Text = lblAppDate.Text;
            lblExpDate.Text = DateTime.Now.AddYears(1).ToString("dd / MM / yyyy");
            lblUser.Text = ClsGlobal.CurrentUser.UserName ;
            lblFees.Text = AppFees.ToString();
        }
        private void FrmIssueInternationalLicense_Load(object sender, EventArgs e)
        {
            AppFees = ClsApplicationType.GetFees((byte)enApplicationType.NewInternationalLicense);
            ShowData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool IsLicenseValid() 
        {
            if (ctrlLicenseSelector1.LocalLicense.LicenseClass != 3)
            {
                MessageBox.Show("You Should Have Local License With Class 3","",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return false;
            }
            else if (!ctrlLicenseSelector1.LocalLicense.IsActive)
            {
                MessageBox.Show("This License Is Not Active ??","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (ctrlLicenseSelector1.LocalLicense.ExpirationDate < DateTime.Now)
            {
                MessageBox.Show("This License Is Expired ??","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            return true; 
        }
        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (IsLicenseValid()) 
            {                
                ClsApplication InternationalLicenseApplication = new ClsApplication(enApplicationType.NewInternationalLicense,ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.PersonID,
                    ClsGlobal.CurrentUser.UserID,AppDate, AppFees);

                InterNationalLicense = ClsInternationalLicense.Find(ctrlLicenseSelector1.LocalLicense.LicenseID);

                // it mean is there already one 
                if (InterNationalLicense != null)
                {
                    MessageBox.Show($"International License Issued SuccessFully With ID {InterNationalLicense.LicenseID}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    InternationalLicenseApplication.Save();
                    InterNationalLicense = new ClsInternationalLicense(InternationalLicenseApplication.ApplicationID, ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.DriverID, DateTime.Now, AppDate, ClsGlobal.CurrentUser.UserID,
                        ctrlLicenseSelector1.LocalLicense.LicenseID);

                    InterNationalLicense.AddNewInterNationalLicense();
                    MessageBox.Show($"International License Issued SuccessFully With ID {InterNationalLicense.LicenseID} !", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    lblAppID.Text = InternationalLicenseApplication.ApplicationID.ToString();
                    lblInterLiceID.Text = InterNationalLicense.LicenseID.ToString();
                }         
                btnIssue.Enabled = false;
                LnkShowLicenseInfo.Enabled = true; 
            }
        }
        private void LnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.NationalNumber))
            frm.ShowDialog();
        }
        private void LnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (InterNationalLicense != null )
            {
                using (FrmShowInternationalLicense frm = new FrmShowInternationalLicense(InterNationalLicense)) 
                frm.ShowDialog();
            }
        }
    }
}
