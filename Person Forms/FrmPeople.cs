using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DLVDBusinessLayer; 

namespace Drives_and_Vehicles_License
{
    public delegate void RefreshData();

    public partial class FrmPeople : Form
    {
        public DataTable PeopleTable;
        public DataView PeopleView;

        FrmAddPerson AddUpdatePerson;
        FrmShowPersonInfo frmShowPersonInfo;
        public FrmPeople()
        {
            InitializeComponent();
        }
   
        void ReloadPeopleData() 
        {
            PeopleTable = ClsPerson.GetAllPeople();

            PeopleView = new DataView(PeopleTable);
            PeopleView.AllowDelete = true;
            PeopleView.AllowEdit = true;
            PeopleView.AllowNew = true;

            GV_People.DataSource = PeopleView;
        }
        private void FrmPeople_Load(object sender, EventArgs e)
        {
            ReloadPeopleData();
            Cb_Filter.SelectedIndex = 0;
        }
        private void Cb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txSearch.Visible = Cb_Filter.SelectedIndex > 0;
        }
        private void txSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //PersonID or PhoneNum
            if (Cb_Filter.SelectedIndex == 1 || Cb_Filter.SelectedIndex == 7)
                e.Handled = char.IsLetter(e.KeyChar);
        }
        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearch.Text))
            {
                PeopleView.RowFilter = "";
                GV_People.DataSource = PeopleView;
                return;
            }
            if (Cb_Filter.SelectedIndex != 1 )
            {
                PeopleView.RowFilter = $"{Cb_Filter.SelectedItem} like '{txSearch.Text}%'";
            }
            else
            {
                PeopleView.RowFilter = $"{Cb_Filter.SelectedItem} = {Convert.ToInt32(txSearch.Text)}";
            }

            GV_People.DataSource = PeopleView;
        }
        void AddPersonToView(ref ClsPerson Person)
        {
            DataRow NewPersonData = PeopleView.Table.NewRow();

            NewPersonData[0] = Person.PersonID;
            NewPersonData[1] = Person.NationalNumber;
            NewPersonData[2] = Person.FirstName;
            NewPersonData[3] = Person.SecondName;
            NewPersonData[4] = Person.ThirdName;
            NewPersonData[5] = Person.LastName;
            NewPersonData[6] = Person.Gender.ToString();
            NewPersonData[7] = Person.DateOfBirth.ToString();
            NewPersonData[9] = Person.CountryName;
            NewPersonData[8] = Person.Phone.ToString();
            NewPersonData[10] = Person.Email;

            PeopleView.Table.Rows.Add(NewPersonData);
        }
        void RefreshPersonData(ref DataRowView PersonData,ref ClsPerson Person)
        {
            PersonData["FirstName"] = Person.FirstName;
            PersonData["SecondName"] = Person.SecondName;
            PersonData["ThirdName"] = Person.ThirdName;
            PersonData["LastName"] = Person.LastName;
            PersonData["Phone"] = Person.Phone;
            PersonData["Email"] = Person.Email;

            PersonData.EndEdit();
        }
        void AddNewPerson () 
        {
            using (AddUpdatePerson = new FrmAddPerson())
            {
                AddUpdatePerson.PersonAdded += AddPersonToView;
                AddUpdatePerson.ShowDialog();
            }
        }
        void UpdatePerson (ref ClsPerson Person) 
        {
            if (GV_People.SelectedRows.Count > 0) 
            {
                DataRowView PersonData =  PeopleView[GV_People.SelectedRows[0].Index];
                using (AddUpdatePerson = new FrmAddPerson(ref Person)) 
                {
                    AddUpdatePerson.ShowDialog();

                    if (AddUpdatePerson.IsUpdated) 
                    {
                        RefreshPersonData(ref PersonData,ref Person);
                    }
                }
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddNewPerson();
        }
        private void editInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NationalNum = GV_People.CurrentRow.Cells[1].Value?.ToString();
            ClsPerson Person = ClsPerson.Find(NationalNum);
            UpdatePerson(ref Person);   
        }
        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPerson();
        }
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GV_People.SelectedRows.Count > 0) 
            {
                int Index = GV_People.SelectedRows[0].Index;
                string nationalNum = PeopleView[Index]["NationalNo"].ToString();
                PeopleView[Index].Delete();
                ClsPerson.Delete(nationalNum);
                PeopleView.Table.AcceptChanges();
            }       
        }
        private void callPhoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet", "", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
        }
        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented Yet", "", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
        }
        private void viewDetialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GV_People.SelectedRows.Count > 0) 
            {
                int Index = GV_People.SelectedRows[0].Index;
                string nationalNum = PeopleView[Index]["NationalNo"].ToString();
                ClsPerson Person = ClsPerson.Find(nationalNum);
                DataRowView PersonData = PeopleView[GV_People.SelectedRows[0].Index];

                using (frmShowPersonInfo = new FrmShowPersonInfo(ref Person)) 
                {
                    frmShowPersonInfo.ShowDialog();
                    RefreshPersonData(ref PersonData, ref Person);
                }
            }
        }
        private void GV_People_DoubleClick(object sender, EventArgs e)
        {
             viewDetialsToolStripMenuItem.PerformClick();
        }

    }
}
