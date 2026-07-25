using DLVDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmManagLocalApplications : Form
    {
        DataView ApplicationsView;
        FrmTestAppointment TestAppointment;

        DataRowView ApplicationRecord; 

        ClsLocalDrivingLicenseApplication LDLApp; 
        int LocalDrivingLicAppID;
        int Index;
        public FrmManagLocalApplications( )
        {
            InitializeComponent();
        }
        void ReloadLocalApplications() 
        {
            ApplicationsView = ClsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications().DefaultView;

            ApplicationsView.Table.Columns[3].ReadOnly = false;
            ApplicationsView.Table.Columns[5].ReadOnly = false;
            ApplicationsView.Table.Columns[6].ReadOnly = false;

            GV_Applications.DataSource = ApplicationsView;
        }
        private void FrmManagLocalApplications_Load(object sender, EventArgs e)
        {
            ReloadLocalApplications();
            Cb_Filter.SelectedIndex = 0;
        }
        private void Cb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_Filter.SelectedIndex == 0)
            {
                ReturnToDefaultView(); 
                txSearch.Visible = Cb_Status.Visible = false;
            }

            else if (Cb_Filter.SelectedIndex > 0 && Cb_Filter.SelectedIndex < 6)
            {
                txSearch.Visible = true;
                Cb_Status.Visible = false;
            }
            else {
                Cb_Status.Visible = true;
                txSearch.Visible = false;
            }

        }
        void ReturnToDefaultView () 
        {
            ApplicationsView.RowFilter = "";
            GV_Applications.DataSource = ApplicationsView;
        }
        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearch.Text))
            {
                ReturnToDefaultView();
                return;
            }

            if (Cb_Filter.SelectedIndex == 1)
                ApplicationsView.RowFilter = $"{Cb_Filter.SelectedItem} = {Convert.ToInt32(txSearch.Text)}";
            else 
                ApplicationsView.RowFilter = $"{Cb_Filter.SelectedItem} like '{txSearch.Text}%'";

            GV_Applications.DataSource = ApplicationsView;
        }
        private void txSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Cb_Filter.SelectedIndex == 1)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void Cb_Status_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplicationsView.RowFilter = $"{Cb_Filter.SelectedItem} = '{Cb_Status.SelectedItem}'";
            GV_Applications.DataSource = ApplicationsView;
        }
        private void GV_Applications_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblRecords.Text = ApplicationsView.Count.ToString();
        }

        void AddApplication(ref ClsLocalDrivingLicenseApplication Application) 
        {
            if (Application != null) 
            {
                ReloadLocalApplications();   
            }        
        }

        void EditApplication(ref ClsLocalDrivingLicenseApplication Application)   
        {
            if (Application != null)
            {
                ApplicationRecord[1] = LicenseClass.LicenseName(Application.DrivingClass);
                ApplicationRecord[3] = ClsPerson.Find(PersonID:Application.ApplicationPersonID).FullName;

                ApplicationRecord.EndEdit();
                GV_Applications.Refresh();
            }
        } 
        private void btnAddorEdit_Click(object sender, EventArgs e)
        {
            FrmAddLocalLicenseApp AddLocalLicense;
            using (AddLocalLicense = new FrmAddLocalLicenseApp())
            {
                AddLocalLicense.ApplicationAddedOrEdited += AddApplication;
                AddLocalLicense.ShowDialog();
            }
        }
        void UpdateApplicationRecord()
        {
            int PassedTests = ClsTest.GetPassedTests(LocalDrivingLicAppID);

            LDLApp = ClsLocalDrivingLicenseApplication.Find(LocalDrivingLicAppID);
            ApplicationRecord[5] = PassedTests;
            ApplicationRecord[1] = LicenseClass.LicenseName(LDLApp.DrivingClass);

            if (PassedTests == 3)
            {
                LDLApp.AppStatus = enApplicationStatus.Complete;
                LDLApp.Save();
            }

            ApplicationRecord.EndEdit();
            GV_Applications.Refresh();
        }
        void TakeTestAppointment (enTestType TestType)
        {
            using (TestAppointment = new FrmTestAppointment(LocalDrivingLicAppID, TestType))
            {
                TestAppointment.ShowDialog();
                if (TestAppointment.IsPassed)
                    UpdateApplicationRecord();               
            }
        }
        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestAppointment(enTestType.enVision);
        }

        void GetApplicationRecord () 
        {
            Index = GV_Applications.CurrentRow.Index;
            if (Index >= 0) 
            {
                ApplicationRecord = ApplicationsView[Index];
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            GetApplicationRecord();
            LocalDrivingLicAppID = Convert.ToInt32(ApplicationRecord[0]);
            string Status = Convert.ToString(ApplicationRecord[6]);

            switch (Status) 
            {
                case "New":

                    // Issue license first Time 
                    ToolStripMenuItem fileMenu = (ToolStripMenuItem)contextMenuStrip1.Items[6];
                    int PassedTests = Convert.ToInt32(ApplicationRecord[5]);
                    if (PassedTests == 3)
                    {
                        for (short i = 0; i < contextMenuStrip1.Items.Count; i++)
                        {
                            contextMenuStrip1.Items[i].Enabled = (i != 6 && i != 9);
                        }
                        return;
                    }

                    for (short i = 0; i < contextMenuStrip1.Items.Count; i++)
                    {
                       contextMenuStrip1.Items[i].Enabled = (i != 8 && i != 9);

                            if (i < fileMenu.DropDownItems.Count)
                       fileMenu.DropDownItems[i].Enabled = i == PassedTests;

                    }
                break;

                case "Completed":

                    for (short i = 0; i < contextMenuStrip1.Items.Count; i++)
                    {
                        if (i > 0 && i < 9)
                            contextMenuStrip1.Items[i].Enabled = false;
                    }
                    break;

                default:
                    for (short i = 0; i < contextMenuStrip1.Items.Count; i++)
                    {
                        switch (i) 
                        {
                            case 0: case 2: case 11:
                                contextMenuStrip1.Items[i].Enabled = true;
                                break;
                            default: 
                                contextMenuStrip1.Items[i].Enabled = false;
                                break;
                        }
                    }
                    break;
            }

        }
        private void writtenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestAppointment(enTestType.enWritten);
        }
        private void streetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTestAppointment(enTestType.enStreet);
        }
        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShowApplicationInfo ShowApplicationInfo;
            using (ShowApplicationInfo = new FrmShowApplicationInfo(LocalDrivingLicAppID)) 
            {
                ShowApplicationInfo.ShowDialog();
            }
        }
        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want Cancel This Application ? ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                ApplicationRecord[6] = "Cancelled";
                LDLApp = ClsLocalDrivingLicenseApplication.Find(LocalDrivingLicAppID);
                
                LDLApp.AppStatus = enApplicationStatus.Cancelled;
                LDLApp.Save();

                ApplicationRecord.EndEdit();
                GV_Applications.Refresh();
            }
        }
        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNum = Convert.ToString(ApplicationsView[Index][2]);
            using (FrmShowPersonLicenseHistory frm = new FrmShowPersonLicenseHistory(NationalNum))
            {
                frm.ShowDialog();
            }

        }
        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmShowLicense frmShowLicense = new FrmShowLicense(LocalDrivingLicAppID))
                frmShowLicense.ShowDialog();

        }    
        void UpdateStatus (ref ClsLocalDrivingLicenseApplication LDLApp) 
        {
            ApplicationsView.Table.Columns[6].ReadOnly = false;
            ApplicationsView.Table.Columns[6].MaxLength = 50;
            DataRowView dt = ApplicationsView[Index];
            dt[6] = "Completed";
            
            LDLApp.AppStatus = enApplicationStatus.Complete;

            dt.EndEdit();
            LDLApp.Save();
            GV_Applications.Refresh();
        }
        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmIssueLicense IssueLicense = new FrmIssueLicense(LocalDrivingLicAppID)) 
            {
                IssueLicense.Issued += UpdateStatus;
                IssueLicense.ShowDialog();
            }

        }
        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Status = Convert.ToString(ApplicationRecord[6]);

            if (Status == "Completed")
            {
                MessageBox.Show($"You Can Not Delete This Application With ID {LocalDrivingLicAppID}\nbecause Its Completed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(LocalDrivingLicAppID.ToString(), "");

            if (MessageBox.Show($"Are You Sure You Want Delete This Application With ID {LocalDrivingLicAppID}", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            {
                ClsApplication.DeleteLocalApplication(LocalDrivingLicAppID);
                ApplicationsView[Index].Delete();
                ApplicationsView.Table.AcceptChanges();
                MessageBox.Show($"Application With ID {LocalDrivingLicAppID} Deleted SuccessFully !","",MessageBoxButtons.OK,MessageBoxIcon.Information);    
            }            
        }
        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmAddLocalLicenseApp frm = new FrmAddLocalLicenseApp(LocalDrivingLicAppID)) 
            {
                frm.ApplicationAddedOrEdited += EditApplication;               
                frm.ShowDialog(); 
            }
        }


    }
}
