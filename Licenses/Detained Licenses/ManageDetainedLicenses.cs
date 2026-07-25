using DLVDBusinessLayer;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class ManageDetainedLicenses : Form
    {
        int Index = 0;
        DataRowView DetainedLicenseRecord = null; 
        FrmDetainLicense frmDetainLicense;
        FrmReleaseDetainedLicense frmReleaseLicense; 
        DataView DetainLicenses;
        public ManageDetainedLicenses()
        {
            InitializeComponent();
        }

        void LoadDetainedLicenses() 
        {
            DetainLicenses = ClsDetainLicense.ListAllDetainedLicense().DefaultView;
            GV_DetainLicenses.DataSource = DetainLicenses;
        }

        private void ManageDetainedLicenses_Load(object sender, EventArgs e)
        {
            LoadDetainedLicenses();
            Cb_Filter.SelectedIndex = 0;
        }

        private void GV_DetainLicenses_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblRecords.Text = GV_DetainLicenses.Rows.Count.ToString();
        }

        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearch.Text)) 
            {
                DetainLicenses.RowFilter = "";
                GV_DetainLicenses.DataSource = DetainLicenses;
                return;
            }
            switch (Cb_Filter.SelectedIndex) 
            {
                case 0:
                    return;
                case 1: case 4:
                    DetainLicenses.RowFilter = $"{Cb_Filter.SelectedItem} = {Convert.ToInt32(txSearch.Text)}";
                    break;
                case 2: case 3:
                    DetainLicenses.RowFilter = $"{Cb_Filter.SelectedItem} like '{txSearch.Text}%'";
                    break;                
                default: 
                    break;     
            }
            GV_DetainLicenses.DataSource = DetainLicenses;

        }

        private void txSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Cb_Filter.SelectedIndex == 1) 
            e.Handled = e.KeyChar != (char)Keys.Back && char.IsLetter(e.KeyChar);
        }

        private void Cb_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_Status.SelectedIndex == 0)
            {
                DetainLicenses.RowFilter = $"{Cb_Filter.SelectedItem} = true";
            }
            else 
            {
                DetainLicenses.RowFilter = $"{Cb_Filter.SelectedItem} = false";
            }
            GV_DetainLicenses.DataSource = DetainLicenses;
        }

        private void Cb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_Filter.SelectedIndex == 0) 
            {
                txSearch.Visible = false;
                Cb_Status.Visible = false;
                return;
            }

            if (Cb_Filter.SelectedIndex > 0 && Cb_Filter.SelectedIndex < 5)
            {
                txSearch.Visible = true;
                Cb_Status.Visible = false;
            }
            else
            {
                txSearch.Visible = false;
                Cb_Status.Visible = true;
            }
        }
        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense = new FrmDetainLicense();
            frmDetainLicense.ShowDialog();
            if (frmDetainLicense.isDetained)
                LoadDetainedLicenses();
        }

        void UpdateDetaineddLicense(ref ClsDetainLicense ReleasedLicense) 
        {
            if (ReleasedLicense.IsReleased)
            {
                DetainedLicenseRecord[4] = ReleasedLicense.ReleaseDate;
                DetainedLicenseRecord[7] = ReleasedLicense.ReleaseApplicationID;
                DetainedLicenseRecord[8] = ReleasedLicense.IsReleased;
                DetainedLicenseRecord.EndEdit();
                GV_DetainLicenses.Refresh();
            }
        }
        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (DetainedLicenseRecord != null)
            frmReleaseLicense = new FrmReleaseDetainedLicense(Convert.ToInt32(DetainedLicenseRecord[1]));
            else
            frmReleaseLicense = new FrmReleaseDetainedLicense();
            frmReleaseLicense.LicenseReleased += UpdateDetaineddLicense;
            frmReleaseLicense.ShowDialog();         
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Index = GV_DetainLicenses.SelectedRows[0].Index;
            if (Index >= 0) 
            {
                DetainedLicenseRecord = DetainLicenses[Index];    
                contextMenuStrip1.Items[4].Enabled = !Convert.ToBoolean(DetainedLicenseRecord[8]);             
            }
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsPerson Person = ClsPerson.Find(NationalNum: Convert.ToString(DetainedLicenseRecord[5]));
            FrmShowPersonInfo frm = new FrmShowPersonInfo(ref Person);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClsLocalLicense LocalLicense = ClsLocalLicense.Find(Convert.ToInt32(DetainedLicenseRecord[1]));
            FrmShowLicense frm = new FrmShowLicense(LocalLicense);
            frm.ShowDialog();
        }

        private void showLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(Convert.ToString(DetainedLicenseRecord[5]));
            frm.ShowDialog();
        }

        private void releaseDetainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRelease.PerformClick();
        }
    }
}
