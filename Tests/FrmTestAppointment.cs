using DLVDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Globalization;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmTestAppointment : Form
    {    
        int LocalDrivingLiceAppID;
        public bool IsPassed;
        int Index;
        enTestType TestType;       

        ClsTestAppointment TestAppointment;
        DataView TestAppointments;
        FrmScheduleTest ScheduleTest;
        FrmTakeTest TakeTest;
        public FrmTestAppointment(int localDrivingLiceAppID, enTestType TestType)
        {
            InitializeComponent();
            this.LocalDrivingLiceAppID = localDrivingLiceAppID;
            this.TestType = TestType;
        }
        void EnableFormMode ()
        {
            switch (TestType) 
            {
                case enTestType.enWritten:
                    this.Text = this.lbltitle.Text = "Written Test";
                    pbTestImg.Image = Image.FromFile(@"C:\Projects\Drives and Vehicles License Project\Local Driving License Photoes\Written.png");
                    break; 
                case enTestType.enStreet:
                    this.Text = this.lbltitle.Text = "Street Test";
                    pbTestImg.Image = Image.FromFile(@"C:\Projects\Drives and Vehicles License Project\Local Driving License Photoes\Street Test.png");
                    break; 
            }
        }

        void InitializeAppointmentsTable () 
        {
            if (TestAppointments.Table.Columns.Count == 0)
            {
                TestAppointments.Table.Columns.Add("TestAppointmentID", typeof(int));
                TestAppointments.Table.Columns.Add("AppointmentDate", typeof(DateTime));
                TestAppointments.Table.Columns.Add("PaidFees", typeof(int));
                TestAppointments.Table.Columns.Add("IsLocked", typeof(bool));
            }

        }
        void LoadTestAppointments () 
        {
            EnableFormMode();
            TestAppointments = ClsTestAppointment.ListAllAppointments(LocalDrivingLiceAppID,TestType).DefaultView;
            GV_Appointments.DataSource = TestAppointments; 
        }
        private void FrmTestAppointment_Load(object sender, EventArgs e)
        { 
            ctrlApplicationInfo1.ViewApplicationInfo(LocalDrivingLiceAppID);
            LoadTestAppointments();
        }

        void SetAppointment(ref ClsTestAppointment Appointment) =>this.TestAppointment = Appointment;

        void AddAppointment()
        {
            if (TestAppointment == null)
                return;

            InitializeAppointmentsTable();

            TestAppointments.RowFilter = "";

            DataRow NewAppointment = TestAppointments.Table.NewRow();

            NewAppointment[0] = TestAppointment.TestAppointmentID;
            NewAppointment[1] = TestAppointment.AppointmentDate;
            NewAppointment[2] = TestAppointment.PaidFees;
            NewAppointment[3] = TestAppointment.IsLocked;

            TestAppointments.Table.Rows.Add(NewAppointment);

            GV_Appointments.Refresh();
                
        }
        private void AddTest(object sender, EventArgs e)
        {
            TestAppointment = ClsTestAppointment.Find(TestType, LocalDrivingLiceAppID);
            
            //there is valid test appointment not locked 
            if (TestAppointment != null)
            {
                MessageBox.Show($"This Person Has AlReady Test Appointment At {TestAppointment.AppointmentDate.Day} / {TestAppointment.AppointmentDate.Month} / {TestAppointment.AppointmentDate.Year}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
            else 
            {
                if (GV_Appointments.Rows.Count != 0)
                {
                    if (!ClsTest.IsPassed(LocalDrivingLiceAppID, TestType))
                    {
                        int PaidFees = ClsApplicationType.GetFees((byte)enApplicationType.RetakeTest);
                        ClsApplication RetakeApplication = new ClsApplication(enApplicationType.RetakeTest, ctrlApplicationInfo1.Person.PersonID, ClsGlobal.CurrentUser.UserID, DateTime.Now, PaidFees);
                        RetakeApplication.Save();

                        using (ScheduleTest = new FrmScheduleTest(TestType, enMode.AddNew, ctrlApplicationInfo1.Person.FullName,
                            ctrlApplicationInfo1.LocalDrivingLicenseApp.LocalDrivingLicenseAppID, ctrlApplicationInfo1.LocalDrivingLicenseApp.DrivingClass,
                            ref TestAppointment, RetakeApplication.ApplicationID))
                        {
                            ScheduleTest.AppointmentBack += SetAppointment;
                            ScheduleTest.ShowDialog();
                        }
                        AddAppointment();
                    }
                    else 
                    {
                        MessageBox.Show("You Have Already Passed This Test");
                        return;
                    }
                }
                else
                {   
                    // there is not a test at all 
                    using (ScheduleTest = new FrmScheduleTest(TestType, enMode.AddNew, ctrlApplicationInfo1.Person.FullName, ctrlApplicationInfo1.LocalDrivingLicenseApp.LocalDrivingLicenseAppID, ctrlApplicationInfo1.LocalDrivingLicenseApp.DrivingClass, ref TestAppointment))
                    {
                        ScheduleTest.AppointmentBack += SetAppointment;
                        ScheduleTest.ShowDialog();
                    }
                    AddAppointment();
                }
            }
        }
        private void GV_Appointments_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblRecords.Text = GV_Appointments.Rows.Count.ToString();
        }

        void UpdateDate (int Index) 
        {
            DataRowView dt = TestAppointments[Index];
            dt[1] = TestAppointment.AppointmentDate;
            dt.EndEdit();
        }

        private void editAppointmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GV_Appointments.SelectedRows[0].Index >= 0 ) 
            {
                int TestAppointmentID = Convert.ToInt32(TestAppointments[Index][0]);
                bool IsLocked = Convert.ToBoolean(TestAppointments[Index][3]);
               
                if (IsLocked) 
                {
                    MessageBox.Show("You Can Not Edit Date Because Test Appointment is Locked !!", "",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }

                if (TestAppointment == null)
                TestAppointment = ClsTestAppointment.Find(TestAppointmentID);

                using (ScheduleTest = new FrmScheduleTest(TestType, enMode.Update, ctrlApplicationInfo1.Person.FullName, ctrlApplicationInfo1.LocalDrivingLicenseApp.LocalDrivingLicenseAppID, ctrlApplicationInfo1.LocalDrivingLicenseApp.DrivingClass, ref TestAppointment))
                {
                    ScheduleTest.ShowDialog();
                    UpdateDate(Index);
                }
            }
        }

       
        void UpdateLockedStatus (int Index) 
        {
            if (TestAppointment.IsLocked) 
            {
                DataRowView dt = TestAppointments[Index];
                dt[3] = true;
                dt.EndEdit();
            }
        }
        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Index >= 0)
            {
                int TestAppointmentID = Convert.ToInt32(TestAppointments[Index][0]);
                bool IsLocked = Convert.ToBoolean(TestAppointments[Index][3]);

                if (IsLocked)
                {
                    MessageBox.Show("You Can Not Take Test Because Test Appointment is Locked !!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (TestAppointment == null)
                    TestAppointment = ClsTestAppointment.Find(TestAppointmentID);

                using (TakeTest = new FrmTakeTest(TestAppointment, ctrlApplicationInfo1.Person.FullName, ctrlApplicationInfo1.LocalDrivingLicenseApp.LocalDrivingLicenseAppID, ctrlApplicationInfo1.LocalDrivingLicenseApp.DrivingClass))
                {
                    TakeTest.ShowDialog();
                    IsPassed = TakeTest.IsPassed;
                    
                    if (IsPassed)
                    {
                        ++ctrlApplicationInfo1.PassedTests;
                        ctrlApplicationInfo1.lblPassedTests.Text = Convert.ToString(ctrlApplicationInfo1.PassedTests ) + " / 3";
                    }
                    UpdateLockedStatus(Index);
                }
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Index = GV_Appointments.SelectedRows[0].Index;
        }
    }
}
