using DLVDBusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class CtrlPersonSelecter : UserControl
    {
        ClsPerson Person;

        public event Action <ClsPerson> OnPersonFound;
        public virtual void FirePersonFound()
        {
            Action<ClsPerson> Handler = OnPersonFound;
            if (Handler != null)
                Handler(Person);
        }
        public CtrlPersonSelecter()
        {
            InitializeComponent();
        }
        private void txSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Cb_Filter.SelectedIndex == 1)
                e.Handled = char.IsLetter(e.KeyChar);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearch.Text))
                return;

            if (Cb_Filter.SelectedIndex != 1)   
                Person = ClsPerson.Find(NationalNum:txSearch.Text);
            else 
                Person = ClsPerson.Find(PersonID : Convert.ToInt32(txSearch.Text));

            if (Person != null)
            {
                FirePersonFound();
                ctrlPersonCardInfo1.ViewPerson(ref Person);
            }
            else
                MessageBox.Show($"Person With {txSearch.Text} Not Found !");
        }       

        public void ddd () 
        {
            btnSearch.PerformClick();
            
        }
        private void CtrlPersonSelecter_Load(object sender, EventArgs e)
        {
            Cb_Filter.SelectedIndex = 1;
        }
        void SetPerson(ref ClsPerson Person)
        {
            this.Person = Person;
            FirePersonFound();
            ctrlPersonCardInfo1.ViewPerson(ref Person);
            panel1.Enabled = false;
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            FrmAddPerson AddNewPerson = new FrmAddPerson();
            AddNewPerson.PersonAdded += SetPerson;
            AddNewPerson.ShowDialog();

        }
    }
}
