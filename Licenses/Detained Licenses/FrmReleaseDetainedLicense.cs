using DLVDBusinessLayer;
using System;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmReleaseDetainedLicense : Form
    {
        int LicenseID;
        ClsDetainLicense DetainLicense = null;
        ClsApplication ReleaseApplication;
        int AppFees;
        DateTime AppDate;


        public delegate void ReleaseLicense(ref ClsDetainLicense DetainLicense);
        public event ReleaseLicense LicenseReleased;

        public FrmReleaseDetainedLicense (int LicenseID = -1) 
        {
            InitializeComponent();
            this.LicenseID = LicenseID;
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (ctrlLicenseSelector1.LocalLicense.IsDetained()) 
            {
                ReleaseApplication = new ClsApplication(enApplicationType.ReleaseDetainedLicense,ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.PersonID,
                    ClsGlobal.CurrentUser.UserID,AppDate,AppFees);

                ReleaseApplication.Save();

                DetainLicense.ReleaseApplicationID = ReleaseApplication.ApplicationID;
                DetainLicense.ReleaseDate = AppDate;
                DetainLicense.IsReleased = true;
                DetainLicense.ReleasedByUser = ClsGlobal.CurrentUser.UserID;

                DetainLicense.Save();

                MessageBox.Show("License Released Successfully ","Done",MessageBoxButtons.OK, MessageBoxIcon.Information);
               
                lblDetainAppID.Text = ReleaseApplication.ApplicationID.ToString();
                ctrlLicenseSelector1.ctrlLicenseinfo1.lblIsDetained.Text = "No";

                btnRelease.Enabled = false;
                LnkShowLicenseInfo.Enabled = true;
                LicenseReleased?.Invoke(ref DetainLicense);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmShowLicense frm = new FrmShowLicense(ctrlLicenseSelector1.LocalLicense))
                frm.ShowDialog();           
        }

        private void LnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.NationalNumber))
                frm.ShowDialog();
        }

        void EnableButtons() 
        {
            DetainLicense = ClsDetainLicense.Find(ctrlLicenseSelector1.LocalLicense.LicenseID);

            if (DetainLicense != null) 
            {
                lblDetainDate.Text = DetainLicense.DetainDate.ToString("dd / MM / yyyy");
                lblDetainID.Text = DetainLicense.DetainID.ToString();
                lblLicenseFees.Text = DetainLicense.FineFees.ToString();
                lblTotalFees.Text = (AppFees + DetainLicense.FineFees).ToString();
            }
            btnRelease.Enabled = true;
            LnkShowLicenseHistory.Enabled = true;
             
            lblLicenseID.Text = ctrlLicenseSelector1.LocalLicense.LicenseID.ToString();
        }

        private void FrmReleaseDetainedLicense_Load(object sender, EventArgs e)
        {
            AppFees = ClsApplicationType.GetFees((byte)enApplicationType.ReleaseDetainedLicense);
            AppDate = DateTime.Now;
            ctrlLicenseSelector1.OnLicenseFound += EnableButtons;
            lblUser.Text = ClsGlobal.CurrentUser.UserName;
            lblAppFees.Text = AppFees.ToString();

            if (LicenseID != -1) 
            {
                ctrlLicenseSelector1.txSearch.Text = LicenseID.ToString();
                ctrlLicenseSelector1.btnSearch.PerformClick();
            }

        }


    }
}
