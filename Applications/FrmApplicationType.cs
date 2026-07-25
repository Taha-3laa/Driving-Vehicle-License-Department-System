using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using DLVDBusinessLayer;
using System.Windows.Forms;

namespace Drives_and_Vehicles_License
{
    public partial class FrmApplicationType : Form
    {
        DataTable ApplicationTypes;
        FrmUpdateApplicationInfo UpdateShowApplication;
        void LoadTableDataAndView () 
        {
            ApplicationTypes = ClsApplicationType.AllApplicationTypes();
        }
        public FrmApplicationType()
        {
            InitializeComponent();
            LoadTableDataAndView();
        }
        private void FrmUpdateApplicationInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (UpdateShowApplication._IsUpdated) 
            {
                LoadTableDataAndView();
                GV_Applications.DataSource = ApplicationTypes;
            }
        }
        private void GV_Applications_DoubleClick(object sender, EventArgs e)
        {
            byte ApplicationID = Convert.ToByte(GV_Applications.CurrentRow.Cells[0].Value);
            UpdateShowApplication = new FrmUpdateApplicationInfo(ApplicationID);
            UpdateShowApplication.FormClosed += FrmUpdateApplicationInfo_FormClosed;
            UpdateShowApplication.ShowDialog();
        }
        private void FrmApplicationType_Load(object sender, EventArgs e)
        {
            GV_Applications.DataSource = ApplicationTypes; 
            lblRecords.Text = ApplicationTypes.Rows.Count.ToString();
        }
    }
}
