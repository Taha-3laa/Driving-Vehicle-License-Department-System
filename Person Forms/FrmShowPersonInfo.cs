using DLVDBusinessLayer;
using System.Data;
using System;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmShowPersonInfo : Form
    {
        DataRowView PersonData;
        ClsPerson Person;
        public FrmShowPersonInfo(ref DataRowView PersonData,ref ClsPerson Person)
        {
            InitializeComponent();
            this.Person = Person;
            this.PersonData = PersonData;
        }           
        public FrmShowPersonInfo(ref ClsPerson Person)
        {
            InitializeComponent();
            this.Person = Person;   
        }        
        
        public FrmShowPersonInfo()
        {
            InitializeComponent();
        }
        private void FrmShowPersonInfo_Load(object sender, EventArgs e)
        {
            ctrlPersonCardInfo1.ViewPerson(ref Person);
        }
    }
}
