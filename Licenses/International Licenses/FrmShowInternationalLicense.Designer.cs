namespace Drives_and_Vehicles_License
{
    partial class FrmShowInternationalLicense
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlInternationalLicense1 = new Drives_and_Vehicles_License.Controls.CtrlInternationalLicense();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bell MT", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(280, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(330, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "International License Info";
            // 
            // ctrlInternationalLicense1
            // 
            this.ctrlInternationalLicense1.Location = new System.Drawing.Point(1, 56);
            this.ctrlInternationalLicense1.Name = "ctrlInternationalLicense1";
            this.ctrlInternationalLicense1.Size = new System.Drawing.Size(898, 401);
            this.ctrlInternationalLicense1.TabIndex = 0;
            // 
            // FrmShowInternationalLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(925, 464);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrlInternationalLicense1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FrmShowInternationalLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "International License Info";
            this.Load += new System.EventHandler(this.FrmShowInternationalLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CtrlInternationalLicense ctrlInternationalLicense1;
        private System.Windows.Forms.Label label1;
    }
}