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
    public partial class FrmReplacementforLostorDamaged : Form
    {
        enApplicationType AppType = enApplicationType.ReplacementForLost;
        enIssueReason IssueReason = enIssueReason.enLost;

        ClsApplication ReplacementApplication;
        ClsLocalLicense ReplacedLicense;
      
        int AppFees;
        DateTime AppDate = DateTime.Now;
        DateTime ExpirationDate;
        public FrmReplacementforLostorDamaged()
        {
            InitializeComponent();
        }

        void ShowData () 
        {
            lblAppDate.Text = AppDate.ToString("dd / MM / yyyy");
            lblFees.Text = AppFees.ToString();
            lblIssueDate.Text = lblAppDate.Text;
            lblUser.Text = ClsGlobal.CurrentUser.UserName;
        }
        void EnableButtons()
        {
            btnReplace.Enabled = true;
            LnkShowLicenseHistory.Enabled = true;
            ExpirationDate = AppDate.AddYears(LicenseClass.ValidityLength(ctrlLicenseSelector1.LocalLicense.LicenseClass));

            lblExpDate.Text = ExpirationDate.ToString("dd / MM / yyyy");
            lblOldLicID.Text = ctrlLicenseSelector1.LocalLicense.LicenseID.ToString();
          
        }

        private void FrmReplacementforLostorDamaged_Load(object sender, EventArgs e)
        {
            AppFees = ClsApplicationType.GetFees((byte)enApplicationType.ReplacementForDamaged);
            ctrlLicenseSelector1.OnLicenseFound += EnableButtons;
            ShowData();
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (ctrlLicenseSelector1.LocalLicense.IsValid()) 
            {
                ReplacementApplication = new ClsApplication(AppType,
                    ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.PersonID, ClsGlobal.CurrentUser.UserID, AppDate, AppFees);
                
                ReplacementApplication.Save();

                ReplacedLicense = new ClsLocalLicense(ReplacementApplication.ApplicationID,ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.DriverID,
                    ctrlLicenseSelector1.LocalLicense.LicenseClass,AppDate,ctrlLicenseSelector1.LocalLicense.ExpirationDate,ctrlLicenseSelector1.LocalLicense.Notes,0,IssueReason,ClsGlobal.CurrentUser.UserID);
            
                ctrlLicenseSelector1.LocalLicense.IsActive =false;
                ctrlLicenseSelector1.LocalLicense.Save();

                ReplacedLicense.Save();

                LnkShowLicenseInfo.Enabled = true;
                MessageBox.Show($"License Replaced Successfully With ID {ReplacedLicense.LicenseID} ","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                lblAppID.Text = ReplacementApplication.ApplicationID.ToString();
                lblReplacedID.Text = ReplacedLicense.LicenseID.ToString();       
            
            }
            else
                MessageBox.Show(ctrlLicenseSelector1.LocalLicense.ErrorMess, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LnkShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ReplacedLicense != null) 
            {
                using (FrmShowLicense frm = new FrmShowLicense(ReplacedLicense))
                    frm.ShowDialog();
            
            }
        }

        private void LnkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(ctrlLicenseSelector1.ctrlLicenseinfo1.Driver.NationalNumber))
                frm.ShowDialog();
        }

        private void ReplacmentReason(object sender, EventArgs e)
        {
            if (rdbtnLost.Checked)
            {
                AppType = enApplicationType.ReplacementForLost;
                IssueReason = enIssueReason.enLost;
            }
            else 
            {
                AppType = enApplicationType.ReplacementForDamaged;
                IssueReason = enIssueReason.enDamaged;
            }
        }
    }
}
