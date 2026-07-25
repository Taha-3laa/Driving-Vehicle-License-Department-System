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
    public partial class FrmManageInternationalLicenses : Form
    {
        DataView ApplicationsView; 
        public FrmManageInternationalLicenses()
        {
            InitializeComponent();
        }

        void ReloadLocalApplications()
        {
            ApplicationsView = ClsInternationalLicense.ListAllInternationalLicenses().DefaultView;
            GV_Applications.DataSource = ApplicationsView;
        }
        private void Cb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cb_Filter.SelectedIndex == 0)
            {
                ApplicationsView.RowFilter = "";
                GV_Applications.DataSource = ApplicationsView;
                txSearch.Visible = CB_IsActive.Visible = false;
            }

            else if (Cb_Filter.SelectedIndex > 0 && Cb_Filter.SelectedIndex < 5)
            {
                txSearch.Visible = true;
                CB_IsActive.Visible = false;
            }
            else
            {
                CB_IsActive.Visible = true;
                txSearch.Visible = false;
            }

        }

        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearch.Text))
            {
                ApplicationsView.RowFilter = "";
                GV_Applications.DataSource = ApplicationsView;
                return;
            }

            if (Cb_Filter.SelectedIndex > 0 && Cb_Filter.SelectedIndex < 5)
                ApplicationsView.RowFilter = $"{Cb_Filter.SelectedItem} = {Convert.ToInt32(txSearch.Text)}";

            GV_Applications.DataSource = ApplicationsView;
        }
        private void txSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Cb_Filter.SelectedIndex > 0 && Cb_Filter.SelectedIndex < 5)
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void GV_Applications_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblRecords.Text = ApplicationsView.Count.ToString();
        }

        private void CB_IsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_IsActive.SelectedIndex == 0)
            {
                ApplicationsView.RowFilter = $"{Cb_Filter.SelectedItem} = true";
            }
            else
            {
                ApplicationsView.RowFilter = $"{Cb_Filter.SelectedItem} = false";
            }
            GV_Applications.DataSource = ApplicationsView;
        }

        private void FrmManageInternationalLicenses_Load(object sender, EventArgs e)
        {
            ReloadLocalApplications();
            Cb_Filter.SelectedIndex = 0;
        }

        private void GV_Applications_DataBindingComplete_1(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblRecords.Text = GV_Applications.Rows.Count.ToString();
        }
    }
}
