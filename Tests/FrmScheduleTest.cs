using DLVDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.UI.Design;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmScheduleTest : Form
    {
        enMode TestMode;
        enTestType TestType;
        int RetakeTestID = -1 ;
        int LDLAppID,TestFees = 0;
        byte DrivingClassTypeID;
        string FullName;

        ClsTestAppointment TestAppointment;

        public delegate void SendAppointment(ref ClsTestAppointment TestAppointment);
        public event SendAppointment AppointmentBack;


        public FrmScheduleTest(enTestType TestType,enMode Mode,string FullName,int LDLAppID,byte DrivingClassTypeID,ref ClsTestAppointment TestAppointment, int RetakeTestID =-1) 
        {
            InitializeComponent();

            this.TestMode = Mode;
            this.TestType = TestType;
            this.RetakeTestID = RetakeTestID;
            this.FullName = FullName;
            this.LDLAppID = LDLAppID;
            this.DrivingClassTypeID = DrivingClassTypeID;   
            this.TestAppointment = TestAppointment;
        }      
        void PrepareFormMode()
        {
            if (TestMode == enMode.AddNew)
            {
                dtpTest.MinDate = dtpTest.Value = DateTime.Now;
            }
            else 
            {
                dtpTest.MinDate = dtpTest.Value = (TestAppointment.AppointmentDate > DateTime.Now)? TestAppointment.AppointmentDate : DateTime.Now;
            }

            switch (TestType) 
            {
                case enTestType.enWritten:
                    lblTitle.Text = "Schedule Written Test";
                    PbTestImg.Image = Image.FromFile(@"C:\Projects\Drives and Vehicles License Project\Local Driving License Photoes\Written.png");
                    break;

                case enTestType.enStreet:
                    lblTitle.Text = "Schedule Street Test";
                    PbTestImg.Image = Image.FromFile(@"C:\Projects\Drives and Vehicles License Project\Local Driving License Photoes\Street Test.png");
                    break;
            }
        }        
        void ShowData () 
        {
            TestFees = ClsTestType.GetTestFees((byte)TestType);
            lblDLAppID.Text = LDLAppID.ToString(); 
            lblDrivingClass.Text = LicenseClass.LicenseName(DrivingClassTypeID).ToString();
            lblName.Text = FullName;
            lblTrails.Text = ClsTest.Trails(LDLAppID,TestType).ToString();
            lblTestFees.Text = TestFees.ToString();

            if (panel2.Enabled) 
            {
                int RetakeTestFees = ClsApplicationType.GetFees((byte)enApplicationType.RetakeTest);
                lblRTestFees.Text = RetakeTestFees.ToString();
                lblTotalFees.Text = Convert.ToString(TestFees + RetakeTestFees);
            }
        }
        private void FrmScheduleTest_Load(object sender, EventArgs e)
        {
            panel2.Enabled = RetakeTestID != -1; 
            PrepareFormMode();
            ShowData();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TestMode == enMode.AddNew)
            {
                TestAppointment = new ClsTestAppointment(LDLAppID, TestType, TestFees, ClsGlobal.CurrentUser.UserID, dtpTest.Value);
                if (TestAppointment.Save())
                {
                    MessageBox.Show($"Appointment Saved Successfully With ID {TestAppointment.TestAppointmentID}!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AppointmentBack?.Invoke(ref TestAppointment);
                    TestMode = enMode.Update;
                    if (RetakeTestID != -1) 
                        lblTestID.Text = RetakeTestID.ToString();
                }
            }
            else
            {
                if (TestAppointment.AppointmentDate == dtpTest.Value)
                    return;

                TestAppointment.AppointmentDate = dtpTest.Value;
                if (TestAppointment.Save())
                {
                    MessageBox.Show($"Appointment Updated Successfully With New Date {TestAppointment.AppointmentDate.Day} / {TestAppointment.AppointmentDate.Month} / {TestAppointment.AppointmentDate.Year}!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
