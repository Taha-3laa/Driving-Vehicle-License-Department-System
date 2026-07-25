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
    public partial class FrmUpdateTestInfo : Form
    {
        ClsTestType Test;
        public bool IsUpdated { get; set; }
        public FrmUpdateTestInfo(byte TestID)
        {
            InitializeComponent();
            Test = ClsTestType.Find(TestID);    
        }

        void ShowTestInfo () 
        {
            lblTestID.Text = Test.TestTypeID.ToString();
            lblTitle.Text = Test.TestName;
            lblDescription.Text = Test.TestTypeID != 3 ? Test.TestDescription : GetDescription();
            lblFees.Text = Test.TestTypeFees.ToString();
        }

        private void FrmUpdateTestInfo_Load(object sender, EventArgs e)
        {
            ShowTestInfo();
        }

        string GetDescription () 
        {
            string Description = "";
            string[] Words = Test.TestDescription.Split(' ');
            int Counter = 0;
            foreach (string W in Words) 
            {
                if (++Counter % 10 == 0 && W!=",") 
                    Description += '\n';

                if (W!=",")
                Description+= W + " ";    

            }

            return Description ;
        }


        private void lnklblEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lblTitle.Visible = lblDescription.Visible = lblFees.Visible = false;
            txDescription.Visible = txFees.Visible = txTitle.Visible = true;

            btnSave.Visible = btnClose.Visible = true; 

            txTitle.Text = Test.TestName;
            txDescription.Text = Test.TestDescription; 
            txFees.Text = Test.TestTypeFees.ToString();

            this.Text = "Update Test Info";
        }

        private void ValidateInput(object sender, KeyPressEventArgs e)
        {
            TextBox tx = sender as TextBox;

            if (tx.TabIndex < 3)
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

            if (MessageBox.Show("Are You Sure You Want Update This Test", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                IsUpdated = true;
                Test.TestName = txTitle.Text;
                Test.TestDescription = txDescription.Text;
                Test.TestTypeFees = Convert.ToInt32(txFees.Text);

                if (Test.Update())
                {
                    MessageBox.Show("Test Info Saved Successfully!");
                }
            }
             
                
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
