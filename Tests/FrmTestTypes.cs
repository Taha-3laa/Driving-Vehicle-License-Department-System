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
    public partial class FrmTestTypes : Form
    {
        DataTable TestTypes;
        FrmUpdateTestInfo UpdateShowTestInfo;
        void LoadTestTable () 
        {
            TestTypes = ClsTestType.ListTestTypes();
        }
        public FrmTestTypes()
        {
            InitializeComponent();
            LoadTestTable();
        }
        private void FrmTestTypes_Load(object sender, EventArgs e)
        {
            GV_Tests.DataSource= TestTypes;
            lblRecords.Text = TestTypes.Rows.Count.ToString();
        }


        private void FrmUpdateTestInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UpdateShowTestInfo.IsUpdated)
            {
                LoadTestTable();
                GV_Tests.DataSource = TestTypes;
            }

        }
        private void GV_Tests_DoubleClick(object sender, EventArgs e)
        {
            byte TestID = Convert.ToByte(GV_Tests.CurrentRow.Cells[0].Value);
            UpdateShowTestInfo = new FrmUpdateTestInfo(TestID);
            UpdateShowTestInfo.FormClosed += FrmUpdateTestInfo_FormClosed;
            UpdateShowTestInfo.ShowDialog();
        }
    }
}
