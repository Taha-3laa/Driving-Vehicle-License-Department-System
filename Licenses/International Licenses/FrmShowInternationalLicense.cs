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
    public partial class FrmShowInternationalLicense : Form
    {
        ClsInternationalLicense InternationalLicense;
        public FrmShowInternationalLicense(ClsInternationalLicense InternationalLicense)
        {
            InitializeComponent();
            this.InternationalLicense = InternationalLicense;
        }

        private void FrmShowInternationalLicense_Load(object sender, EventArgs e)
        {
            ctrlInternationalLicense1.ViewLicense(InternationalLicense);
        }
    }
}
