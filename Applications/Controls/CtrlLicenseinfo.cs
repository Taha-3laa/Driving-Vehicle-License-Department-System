using System;
using DLVDBusinessLayer;
using System.Windows.Forms;
using System.Drawing;
using Drives_and_Vehicles_License.Properties;

namespace Drives_and_Vehicles_License
{
    public partial class CtrlLicenseinfo : UserControl
    {
        ClsLocalLicense License;
        public ClsDriver Driver;
        public CtrlLicenseinfo()
        {
            InitializeComponent();
        }
        void ShowData () 
        {
            lblLicenseID.Text = License.LicenseID.ToString();
            lblClass.Text = LicenseClass.LicenseName(License.LicenseClass);
            lblissueDate.Text = License.IssueDate.ToString("dd / MM / yyyy");
            lblExpirationDate.Text = License.ExpirationDate.ToString("dd / MM / yyyy");
            lblIssureReason.Text = License.ToString();
            lblNotes.Text = string.IsNullOrEmpty(License.Notes)? "No Notes" : License.Notes;
            lblDriverID.Text = License.DriverID.ToString();
            lblName.Text = Driver.FullName;
            lblGender.Text = Driver.Gender == 'M' ? "Male" : "Female";
            lblIsDetained.Text = License.IsDetained() ? "Yes" : "No";
            lblIsActive.Text = License.IsActive ? "Yes" : "No";
            lblNationalNum.Text = Driver.NationalNumber;
            lblDateOfBirth.Text = Driver.DateOfBirth.ToString("dd / MM / yyyy");
            if (!string.IsNullOrEmpty(Driver.ImgPath)) 
                PbPersonImg.Image = Image.FromFile(Driver.ImgPath);            
            else
                PbPersonImg = null;
        }

        public void ShowLicenseInfo (ClsLocalLicense license) 
        {
            this.License = license;
            Driver = ClsDriver.Find(DriverID: License.DriverID);
            ShowData();
        }

    }
}
