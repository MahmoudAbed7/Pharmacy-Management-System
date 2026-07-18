namespace PharmacySystem.Cart.Control
{
    partial class ctrlItemInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlProductCard1 = new PharmacySystem.Products.controls.ctrlProductCard();
            this.gbItemInfo = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblItemID = new System.Windows.Forms.Label();
            this.lblCartID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gbItemInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlProductCard1
            // 
            this.ctrlProductCard1.Location = new System.Drawing.Point(12, 115);
            this.ctrlProductCard1.Name = "ctrlProductCard1";
            this.ctrlProductCard1.Size = new System.Drawing.Size(1106, 370);
            this.ctrlProductCard1.TabIndex = 0;
            // 
            // gbItemInfo
            // 
            this.gbItemInfo.Controls.Add(this.lblQuantity);
            this.gbItemInfo.Controls.Add(this.label4);
            this.gbItemInfo.Controls.Add(this.lblCartID);
            this.gbItemInfo.Controls.Add(this.label3);
            this.gbItemInfo.Controls.Add(this.lblItemID);
            this.gbItemInfo.Controls.Add(this.label1);
            this.gbItemInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbItemInfo.Location = new System.Drawing.Point(12, 12);
            this.gbItemInfo.Name = "gbItemInfo";
            this.gbItemInfo.Size = new System.Drawing.Size(1089, 100);
            this.gbItemInfo.TabIndex = 1;
            this.gbItemInfo.TabStop = false;
            this.gbItemInfo.Text = "Item Info";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "ItemID:";
            // 
            // lblItemID
            // 
            this.lblItemID.AutoSize = true;
            this.lblItemID.Location = new System.Drawing.Point(149, 42);
            this.lblItemID.Name = "lblItemID";
            this.lblItemID.Size = new System.Drawing.Size(34, 25);
            this.lblItemID.TabIndex = 1;
            this.lblItemID.Text = "??";
            // 
            // lblCartID
            // 
            this.lblCartID.AutoSize = true;
            this.lblCartID.Location = new System.Drawing.Point(511, 42);
            this.lblCartID.Name = "lblCartID";
            this.lblCartID.Size = new System.Drawing.Size(34, 25);
            this.lblCartID.TabIndex = 3;
            this.lblCartID.Text = "??";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(433, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "CartID:";
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(950, 42);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(34, 25);
            this.lblQuantity.TabIndex = 5;
            this.lblQuantity.Text = "??";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(840, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 25);
            this.label4.TabIndex = 4;
            this.label4.Text = "Quantity:";
            // 
            // ctrlItemInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbItemInfo);
            this.Controls.Add(this.ctrlProductCard1);
            this.Name = "ctrlItemInfo";
            this.Size = new System.Drawing.Size(1114, 493);
            this.gbItemInfo.ResumeLayout(false);
            this.gbItemInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Products.controls.ctrlProductCard ctrlProductCard1;
        private System.Windows.Forms.GroupBox gbItemInfo;
        private System.Windows.Forms.Label lblCartID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItemID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label label4;
    }
}
