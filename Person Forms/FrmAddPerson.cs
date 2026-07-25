using DLVDBusinessLayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Drives_and_Vehicles_License
{
    public enum enMode {AddNew,Update};
    public partial class FrmAddPerson : Form
    {
        enMode FrmMode;
        ClsPerson Person;
        public bool IsUpdated = false;

        public delegate void SendPersonBack(ref ClsPerson Person);
        public event SendPersonBack PersonAdded;


        static List<string> Countries = Countries = ClsPerson.GetAllCountries();
        List<string> NationalNumbers;

       
        void FillControlsWithData() 
        {
            txFirstName.Text = Person.FirstName; 
            txSecondName.Text = Person.SecondName;
            txThirdName.Text = Person.ThirdName;
            txLastName.Text = Person.LastName;  
            txAddress.Text = Person.Address;
            txEmail.Text = Person.Email;
            txPhone.Text = Person.Phone;
            dtpDateOfBirth.Value = Person.DateOfBirth;  

            rdMale.Checked = Person.Gender == 'M';

            if (!rdMale.Checked)
                rdFemale.Checked = true;

            if (!string.IsNullOrEmpty(Person.ImgPath)) 
            ImgPerson.Image = Image.FromFile (Person.ImgPath);

            CbCountries.SelectedIndex = Person.NationalCountryID - 1;
        }
        void InitializeFormMode () 
        {
            if (FrmMode == enMode.Update)
            {
                this.Text = lblTitle.Text = "Update Person";
                lblPersonID.Text = Person.PersonID.ToString();
                lnklblSetImage.Text = "Change Image";
                rdFemale.Enabled = rdMale.Enabled = CbCountries.Enabled = txbNationalNum.Enabled = dtpDateOfBirth.Enabled = false;
                FillControlsWithData();
            }
            else 
            {
                CbCountries.SelectedIndex = 50;
                NationalNumbers = ClsPerson.GetAllNationalNumbers();    
                this.Text = lblTitle.Text = "Add New Person";
                lnklblSetImage.Text = "Set Image";
                ImgPerson.Image = Image.FromFile(@"C:\Projects\DLVD\DLVDPresention\Person Forms\Person Photoes\Man.png");
            }
        }
        public FrmAddPerson(ref ClsPerson Person)
        {
            InitializeComponent();

            this.FrmMode = enMode.Update;
            this.Person = Person;
        }
        public FrmAddPerson()
        {
            InitializeComponent();
            this.FrmMode = enMode.AddNew;
        } 
        private void FrmAddPerson_Load(object sender, EventArgs e)
        {
            CbCountries.DataSource = Countries;
            InitializeFormMode();
        }
        private void NamesValidations(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox tx = sender as TextBox;

            if (string.IsNullOrEmpty(tx.Text) || int.TryParse(tx.Text, out int res))
            {
                e.Cancel = true;
                tx.Focus();
                errorProvider1.SetError(tx, "Enter Valid Name");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(tx, "");
            }
        }
        private void ValidateNationalNum(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (NationalNumbers.Contains(txbNationalNum.Text)|| string.IsNullOrEmpty(txbNationalNum.Text))
            {
                e.Cancel = true;
                txbNationalNum.Focus();
                errorProvider1.SetError(txbNationalNum, "National Number Already Found !");                
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txbNationalNum, "");
            }
        }
        private void ValidateEmail(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txEmail.Text) || !txEmail.Text.Contains("@gmail.com"))
            {
                e.Cancel = true;
                txEmail.Focus();
                errorProvider1.SetError(txEmail, "Enter Valid Email");
            }
            else 
            {
                e.Cancel= false;
                errorProvider1.SetError(txEmail, "");
            }

        }   
        bool AllFieldsAreFull() 
        {
            foreach (Control c in panel1.Controls) 
            {
                if (c is TextBox tx ) 
                {
                    if (string.IsNullOrEmpty(tx.Text)) 
                    {
                        return false;
                    }
                }
            }
            return true; 
        }
        void ClearForm () 
        {
            lblPersonID.Text = "#%%#";
            foreach (Control c in panel1.Controls)
            {
                if (c is TextBox tx)
                {
                    c.Text = "";
                }
            }

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (FrmMode == enMode.AddNew)
            {
                if (!AllFieldsAreFull()) 
                {
                    MessageBox.Show("Not All Fields Are Full !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Person Saved Successfully","Add Person",MessageBoxButtons.OK, MessageBoxIcon.Information);
                char Gender = rdMale.Checked ? 'M' : 'F';
                Person = new ClsPerson(txbNationalNum.Text, txFirstName.Text, txSecondName.Text, txThirdName.Text, txLastName.Text, dtpDateOfBirth.Value, Gender, txAddress.Text
                    , txPhone.Text, txEmail.Text, ImgPerson.ImageLocation, CbCountries.SelectedIndex + 1);
                //Add National Num to List
                NationalNumbers.Add(Person.NationalNumber);
                ClearForm();
            }
            else
            {
                MessageBox.Show("Person Updated Successfully", "Update Person", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Person.FirstName = txFirstName.Text;
                Person.SecondName = txSecondName.Text;
                Person.ThirdName = txThirdName.Text;
                Person.LastName = txLastName.Text;
                Person.Email = txEmail.Text;
                Person.Phone = txPhone.Text;
                Person.Address = txAddress.Text;  
                if (ImgPerson.ImageLocation != null)
                Person.ImgPath = ImgPerson.ImageLocation;
                IsUpdated = true;
            }
            Person.Save();


            if (FrmMode == enMode.AddNew)
            {
                lblPersonID.Text = Person.PersonID.ToString();
                MessageBox.Show($"Person With [{lblPersonID.Text}] Added Successfully !","",MessageBoxButtons.OK, MessageBoxIcon.Information); 
                PersonAdded?.Invoke(ref Person);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lnklblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "Choose Photo";
            openFileDialog1.DefaultExt = ".jpg";
            openFileDialog1.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";
            openFileDialog1.FilterIndex = 1;

            lnklblSetImage.LinkVisited = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImgPerson.ImageLocation = openFileDialog1.FileName;
                lnklblRemove.Visible = true;    
            }
        }
        private void lnklblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want To Remove This Image ? ", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) 
            {
                lnklblRemove.LinkVisited = true;
                ImgPerson.ImageLocation = null;
            }
        }
        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
            if (ImgPerson.ImageLocation == null) 
            {
                if (rdMale.Checked)
                    ImgPerson.Image = Image.FromFile(@"C:\Projects\DLVD\DLVDPresention\Person Forms\Person Photoes\Man.png");
                else
                    ImgPerson.Image = Image.FromFile(@"C:\Projects\DLVD\DLVDPresention\Person Forms\Person Photoes\Woman.png");
            }
        }
    }
}
