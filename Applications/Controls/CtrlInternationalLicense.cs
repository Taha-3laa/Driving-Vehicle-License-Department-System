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

namespace Drives_and_Vehicles_License.Controls
{
    public partial class CtrlInternationalLicense : UserControl
    {
        ClsDriver Driver;
        public CtrlInternationalLicense()
        {
            InitializeComponent();
        }

        public void ViewLicense(ClsInternationalLicense License) 
        {
            Driver = ClsDriver.Find(DriverID:License.DriverID);
            if (Driver != null) 
            {
                lblInternationalID.Text = License.LicenseID.ToString();
                lblAppID.Text = License.ApplicationID.ToString();
                lblLicenseID.Text = License.IssuedLocalLicenseID.ToString();
                lblissueDate.Text = License.IssueDate.ToString("dd / MM / yyyy");
                lblExpirationDate.Text = License.ExpirationDate.ToString("dd / MM / yyyy");
                lblDriverID.Text = License.DriverID.ToString();
                lblName.Text = Driver.FullName;
                lblGender.Text = Driver.Gender == 'M' ? "Male" : "Female";
                lblIsActive.Text = License.IsActive ? "Yes" : "No";
                lblNationalNum.Text = Driver.NationalNumber;
                lblDateOfBirth.Text = Driver.DateOfBirth.ToString("dd / MM / yyyy");
                if (!string.IsNullOrEmpty(Driver.ImgPath))
                    PbPersonImg.Image = Image.FromFile(Driver.ImgPath);
                else
                    PbPersonImg = null;
            }
        }

    }
}
