using DLVDBusinessLayer;
using System;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmDetainLicense : Form
    {
        public bool isDetained = false;
        ClsDetainLicense DetainLicense = null; 
        int FineFees = 0;
        DateTime DetainDate = DateTime.Now;
        public FrmDetainLicense()
        {
            InitializeComponent();
        }

        void ShowData ()
        {
            lblDetainDate.Text = DetainDate.ToString("dd / MM / yyyy");
            lblUser.Text = ClsGlobal.CurrentUser.UserName;
        }

        void EnableButtons () 
        {
            LnkShowLicenseHistory.Enabled = true;
            btnDetain.Enabled = true; 
            lblLocalID.Text = ctrlLicenseSelector1.LocalLicense.LicenseID.ToString();
        }
        private void FrmDetainLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseSelector1.OnLicenseFound += EnableButtons;
            ShowData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txFees.Text)) 
            {
                MessageBox.Show("Please Enter a Fine Fees To Detain Selected License","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return; 
            }

            FineFees = Convert.ToInt32(txFees.Text);
            if (ctrlLicenseSelector1.LocalLicense.IsValid()) 
            {
                DetainLicense = new ClsDetainLicense(ctrlLicenseSelector1.LocalLicense.LicenseID, DetainDate, FineFees,ClsGlobal.CurrentUser.UserID);;   
                DetainLicense.Save();

                MessageBox.Show($"License Is Detianed With ID {DetainLicense.DetainID}", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                isDetained = true;  
                ctrlLicenseSelector1.ctrlLicenseinfo1.lblIsDetained.Text = "Yes";
                lblDetainID.Text = DetainLicense.DetainID.ToString();
                LnkShowLicenseInfo.Enabled = true;
                btnDetain.Enabled = false;
            }
            else
                MessageBox.Show(ctrlLicenseSelector1.LocalLicense.ErrorMess, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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

        private void txFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back;
        }


    }
}
