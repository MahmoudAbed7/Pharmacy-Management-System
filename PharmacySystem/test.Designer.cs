namespace PharmacySystem
{
    partial class test
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
            this.ctrlItemInfo1 = new PharmacySystem.Cart.Control.ctrlItemInfo();
            this.SuspendLayout();
            // 
            // ctrlItemInfo1
            // 
            this.ctrlItemInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlItemInfo1.Name = "ctrlItemInfo1";
            this.ctrlItemInfo1.Size = new System.Drawing.Size(1114, 493);
            this.ctrlItemInfo1.TabIndex = 0;
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 644);
            this.Controls.Add(this.ctrlItemInfo1);
            this.Name = "test";
            this.Text = "test";
            this.Load += new System.EventHandler(this.test_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Cart.Control.ctrlItemInfo ctrlItemInfo1;
    }
}