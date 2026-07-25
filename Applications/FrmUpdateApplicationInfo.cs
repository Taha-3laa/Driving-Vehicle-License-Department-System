using DLVDBusinessLayer;
using Microsoft.Win32;
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
    public partial class FrmUpdateApplicationInfo : Form
    {
        ClsApplicationType ApplicationType;
        byte ApplicationID;
        public bool _IsUpdated { set;get; }
        public FrmUpdateApplicationInfo(byte applicationID)
        {
            InitializeComponent();
            ApplicationID = applicationID;  
        }
        void ShowApplicationInfo ()
        {
            lblAppID.Text = ApplicationType.ApplicationTypeID.ToString();
            lblTitle.Text = ApplicationType.ApplicationName;
            lblFees.Text  = ApplicationType.Fees.ToString();            
        }
        private void FrmUpdateApplicationInfo_Load(object sender, EventArgs e)
        {
            ApplicationType = ClsApplicationType.Find(ApplicationID);           
            ShowApplicationInfo();
        }
        void EnableUpdateMode () 
        {
            this.Text = "Update Application Info"; 

            txTitle.Visible = txFees.Visible = true; 
            btnSave.Visible = btnClose.Visible = true;

            txTitle.Text = ApplicationType.ApplicationName;
            txFees.Text = ApplicationType.Fees.ToString();

            lblTitle.Visible = lblFees.Visible = false;
        }
        private void lnklblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lnklblEdit.LinkVisited = true;
            EnableUpdateMode();
        }
        private void ValidateInput(object sender, KeyPressEventArgs e)
        {
            TextBox tx = sender as TextBox;
            
            if (tx.Tag.ToString() == "1")
            {
                e.Handled = char.IsDigit(e.KeyChar);
            }
            else 
            {
                e.Handled = char.IsLetter(e.KeyChar);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txFees.Text) || string.IsNullOrEmpty(txTitle.Text))
            {
                MessageBox.Show("Invalid Save Operation , Some Fields Are Null !");
                return; 
            }

            if (MessageBox.Show("Are You Sure You Want Update This Application", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                _IsUpdated = true;
                ApplicationType.ApplicationName = txTitle.Text;
                ApplicationType.Fees = Convert.ToInt32 (txFees.Text); 

                if (ApplicationType.Update()) 
                {
                    MessageBox.Show("Application Info Saved Successfully!");
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {          
            this.Close();
        }

  
    }
}
