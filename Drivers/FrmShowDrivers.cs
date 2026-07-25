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
    public partial class FrmShowDrivers : Form
    {
        DataView Drivers;
        public FrmShowDrivers()
        {
            InitializeComponent();
        }
        private void FrmShowDrivers_Load(object sender, EventArgs e)
        {
            Drivers = ClsDriver.ListAllDrivers().DefaultView; 
            GV_Drivers.DataSource = Drivers;
        }

        private void Cb_Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txSearch.Visible = Cb_Filter.SelectedIndex > 0;
        }

        private void txSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txSearch.Text))
            {
                Drivers.RowFilter = "";
                GV_Drivers.DataSource = Drivers;
                return;
            }


            if (Cb_Filter.SelectedIndex < 3)
            {
                Drivers.RowFilter = $"{Cb_Filter.SelectedItem} = {Convert.ToInt32(txSearch.Text)}";
            }
            else
                Drivers.RowFilter = $"{Cb_Filter.SelectedItem} like '{txSearch.Text}%'";

            GV_Drivers.DataSource = Drivers;
        }

        private void txSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only Numbers
            if (Cb_Filter.SelectedIndex <3) 
            {
                e.Handled = char.IsLetter(e.KeyChar)|| char.IsSeparator(e.KeyChar);
            }
        }

        private void GV_Drivers_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            lblRecords.Text = GV_Drivers.Rows.Count.ToString();
        }
    }
}
