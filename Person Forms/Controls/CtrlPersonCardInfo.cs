using DLVDBusinessLayer;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class CtrlPersonCardInfo : UserControl
    {
        FrmAddPerson EditPersonInfo;
        ClsPerson Person;
        ClsUser User;
        public CtrlPersonCardInfo()
        {
            InitializeComponent();
        }        
        void ShowPersonInfo ()
        {
            lblID.Text = Person.PersonID.ToString();
            lblName.Text = Person.FullName;
            lblNationalNum.Text = Person.NationalNumber;
            lblDataOfBirth.Text = Person.DateOfBirth.ToString("yyyy/MM/dd");
            lblEmail.Text = Person.Email;
            lblPhone.Text = Person.Phone;
            lblAddress.Text = Person.Address;
            lblCountry.Text = Person.CountryName;

            if (Person.Gender == 'M')
            {
                pbGender.Image = Image.FromFile(@"C:\Projects\DLVD\DLVDPresention\Person Forms\Person Photoes\Male.png");
                lblGender.Text = "Male";
            }
            else
            {
                pbGender.Image = Image.FromFile(@"C:\Projects\DLVD\DLVDPresention\Person Forms\Person Photoes\female.png");
                lblGender.Text = "Female";
            }

            if (!string.IsNullOrEmpty(Person.ImgPath))
                pbPerson.Image = Image.FromFile(Person.ImgPath);
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {   
            EditPersonInfo = new FrmAddPerson(ref Person);
            EditPersonInfo.ShowDialog();

            if (EditPersonInfo.IsUpdated) 
            {
                 ShowPersonInfo();   
                 if (User != null)
                 User.Person = Person;
                // Find Current User 
                if (ClsGlobal.CurrentUser.Person.PersonID == Person.PersonID)
                        ClsGlobal.CurrentUser.Person = Person;
            }
        }
        public void ViewPerson(ref ClsPerson Person) 
        {
            lnkEdit.Visible = true;
            this.Person = Person;
            ShowPersonInfo();
        }
        public void ViewPerson(ref ClsUser User) 
        {
            lnkEdit.Visible = true;
            this.User = User;
            this.Person = User.Person; 
            ShowPersonInfo();
        }
    }
}
