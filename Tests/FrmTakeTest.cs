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
    public partial class FrmTakeTest : Form
    {
        ClsTestAppointment TestAppointment ;
        int  LocalDrivingLicAppID;
        string FullName;
        byte DrivingClassTypeID;

        public bool IsPassed = false;      
        public FrmTakeTest(ClsTestAppointment TestAppointment,string FullName,int LocalDrivingLicAppID,byte DrivingClassTypeID)
        {
            InitializeComponent();
             
            this.TestAppointment = TestAppointment;
            this.LocalDrivingLicAppID = LocalDrivingLicAppID;
            this.FullName = FullName;
            this.DrivingClassTypeID = DrivingClassTypeID;
        }
        void EnableFormMode()
        {
            switch (TestAppointment.TestType)
            {
                case enTestType.enWritten:
                    this.Text = this.lblTitle.Text = "Written Test";
                    PbTestImg.Image = Image.FromFile(@"C:\Projects\Drives and Vehicles License Project\Local Driving License Photoes\Written.png");
                    break;
                case enTestType.enStreet:
                    this.Text = this.lblTitle.Text = "Street Test";
                    PbTestImg.Image = Image.FromFile(@"C:\Projects\Drives and Vehicles License Project\Local Driving License Photoes\Street Test.png");
                    break;
            }
        }
        void ShowTestData() 
        {
            lblDLAppID.Text = LocalDrivingLicAppID.ToString();
            lblDrivingClass.Text = LicenseClass.LicenseName(DrivingClassTypeID);
            lblName.Text = FullName;
            lblTrails.Text = ClsTest.Trails(LocalDrivingLicAppID, TestAppointment.TestType).ToString();
            lblTestDate.Text = TestAppointment.AppointmentDate.ToString("dd / MM / yyyy");
            lblTestFees.Text = ClsTestType.GetTestFees((byte)TestAppointment.TestType).ToString();     
        }
        private void FrmTakeTest_Load(object sender, EventArgs e)
        {
            EnableFormMode();
            ShowTestData();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want Save This Test ?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int TestID = ClsTest.InsertTest(TestAppointment.TestAppointmentID, rbPass.Checked, txNotes.Text, ClsGlobal.CurrentUser.UserID);
                if (TestID != -1)
                {
                    lblTestID.Text = TestID.ToString();
                    IsPassed = rbPass.Checked;
                    MessageBox.Show("Test Saved Successfully!");
                    TestAppointment.IsLocked = true;
                    TestAppointment.Save();
                }
            }
            btnSave.Enabled = false;
        }

        private void rbPass_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = true; 
        }
    }
}
