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
    public partial class CtrlLicenseSelector : UserControl
    {
        public ClsLocalLicense LocalLicense;

        public event Action OnLicenseFound;
        public virtual void LicenseFound()
        {
            Action handler = OnLicenseFound;
            if (handler != null)
                handler();
        }
        public CtrlLicenseSelector()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = e.KeyChar == (char)Keys.Enter||char.IsLetter(e.KeyChar);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txSearch.Text.Length > 0) 
            {
                LocalLicense = ClsLocalLicense.Find(Convert.ToInt32(txSearch.Text));
                if (LocalLicense != null)
                {
                    ctrlLicenseinfo1.ShowLicenseInfo(LocalLicense);
                    LicenseFound();
                    btnSearch.Enabled = false; 
                    panel1.Enabled = false;
                }
                else 
                {
                    MessageBox.Show($"There Is No Local License With ID [ {txSearch.Text} ]","Done",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
        }

    }
}
