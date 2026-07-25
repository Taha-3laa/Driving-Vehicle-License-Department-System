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
    public partial class FrmShowPersonLicenseHistory : Form
    {
        ClsPerson Person;
        string NationalNum;
        public FrmShowPersonLicenseHistory(string nationalNum)
        {
            InitializeComponent();
            NationalNum = nationalNum;
        }
        
        void LoadLicenses () 
        {
            GV_LocalLicenses.DataSource = ClsLocalLicense.ListAllLocalLicenses(Person.PersonID).DefaultView;
            Gv_InternationalLicenses.DataSource = ClsInternationalLicense.ListAllInternationalLicensesForPerson(Person.PersonID).DefaultView;
        }

        private void FrmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            Person = ClsPerson.Find(NationalNum:NationalNum);
            ctrlPersonCardInfo1.ViewPerson(ref Person);
            LoadLicenses();
        }

        private void GV_LocalLicenses_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblLRecords.Text = GV_LocalLicenses.Rows.Count.ToString();
        }

        private void Gv_InternationalLicenses_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lbliRecords.Text = Gv_InternationalLicenses.Rows.Count.ToString();
        }
    }
}
