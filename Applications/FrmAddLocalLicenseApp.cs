using DLVDBusinessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Configuration;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmAddLocalLicenseApp : Form
    {

        enMode FrmMode;

        bool AllowMoving = false;
        int ApplicationFees, ClassFees, AppID;

        public delegate void SendApplicationBack(ref ClsLocalDrivingLicenseApplication Application);
        public event SendApplicationBack ApplicationAddedOrEdited;

        ClsLocalDrivingLicenseApplication LDLApp;
        Dictionary<string,int> LicenseClasses;
        ClsPerson Person;
        void EnableButtons (ClsPerson Person) 
        {
            this.Person = Person;
            btnNext.Enabled = btnSave.Enabled = true;
        }
        void EnableFormMode()
        {

            ctrlPersonSelecter1.OnPersonFound += EnableButtons;
            ApplicationFees = ClsApplicationType.GetFees((byte)enApplicationType.NewLocalDrivingLicense);
            lblAppFees.Text = ApplicationFees.ToString();
            lblUserName.Text = ClsGlobal.CurrentUser.UserName;

            if (FrmMode != enMode.AddNew)
            {
                this.Text = lblTitle.Text = "Update Local Driving License Application";

                LDLApp = ClsLocalDrivingLicenseApplication.Find(AppID);
                lblAppID.Text = LDLApp.ApplicationID.ToString();
                ctrlPersonSelecter1.txSearch.Text = LDLApp.ApplicationPersonID.ToString();
                ctrlPersonSelecter1.btnSearch.PerformClick();
                ctrlPersonSelecter1.panel1.Enabled = false;
                cmbLicenseClasses.SelectedIndex = LDLApp.DrivingClass - 1;
                lblAppDate.Text = LDLApp.Date.ToString("dd / MM / yyyy");
            }
            else 
            {
                this.Text = lblTitle.Text = "New Local Driving License Application";
                cmbLicenseClasses.SelectedIndex = 2;
                lblAppDate.Text = DateTime.Now.ToString("dd / MM / yyyy");
            }
        }
        public FrmAddLocalLicenseApp()
        {
            InitializeComponent();
            this.FrmMode = enMode.AddNew;
        }

        public FrmAddLocalLicenseApp(int LocalDrivingLicenseAppID)
        {
            InitializeComponent();
            this.AppID = LocalDrivingLicenseAppID;
            this.FrmMode = enMode.Update;
        }

        private void FrmAddLocalLicenseApp_Load(object sender, EventArgs e)
        {
            LoadLicenseClasses();
            EnableFormMode();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            AllowMoving = true;
            ++tabControl1.SelectedIndex;
            AllowMoving = false;
            btnSave.Enabled = true;
        }
        private void TabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !AllowMoving;
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            AllowMoving = true;
            --tabControl1.SelectedIndex;
            AllowMoving = false;
        }
        private void ValidateInputs(object sender, KeyPressEventArgs e)
        {
            TextBox tx = sender as TextBox;
            if (tx.TabIndex > 2)
                e.Handled = char.IsLetter(e.KeyChar);
            else
                e.Handled = char.IsDigit(e.KeyChar);
        }
        void LoadLicenseClasses () 
        {
            LicenseClasses = LicenseClass.GetLicenseClasses();

            cmbLicenseClasses.DataSource = LicenseClasses.ToList();
            cmbLicenseClasses.ValueMember = "Value";
            cmbLicenseClasses.DisplayMember = "Key";
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FrmMode != enMode.AddNew) {
                LDLApp.CreatedByUser = ClsGlobal.CurrentUser.UserID;
                LDLApp.LastStatusDate = DateTime.Now;
                LDLApp.DrivingClass = Convert.ToByte(cmbLicenseClasses.SelectedIndex + 1);         
            }
            else 
                LDLApp = new ClsLocalDrivingLicenseApplication(Person.PersonID, ClsGlobal.CurrentUser.UserID, DateTime.Now, ApplicationFees, Convert.ToByte(cmbLicenseClasses.SelectedIndex + 1));

            if (LDLApp.Save())
            {
                MessageBox.Show($"Application Saved Successfully With ID [{LDLApp.LocalDrivingLicenseAppID}]", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblAppID.Text = LDLApp.LocalDrivingLicenseAppID.ToString();
                ApplicationAddedOrEdited?.Invoke(ref LDLApp);
            }
            else
            {
                MessageBox.Show($"Application Not Saved\nPerson With ID [ {Person.PersonID} ] Apply For Active Application With ID[ {LDLApp.LocalDrivingLicenseAppID} ]", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmbLicenseClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassFees = ((KeyValuePair<string, int>)cmbLicenseClasses.SelectedItem).Value;
            lblClassFees.Text = ClassFees.ToString();
            lblTotalFees.Text = (ClassFees + ApplicationFees).ToString();
        }
    }
}
