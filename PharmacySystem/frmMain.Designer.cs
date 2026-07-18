namespace PharmacySystem
{
    partial class frmMain
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
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefillStock = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnListCustomers = new System.Windows.Forms.Button();
            this.btnJoiningApplication = new System.Windows.Forms.Button();
            this.btnListProducts = new System.Windows.Forms.Button();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnListCategories = new System.Windows.Forms.Button();
            this.btnListUsers = new System.Windows.Forms.Button();
            this.btnListPeople = new System.Windows.Forms.Button();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.userInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentUser.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCurrentUser.Location = new System.Drawing.Point(90, 89);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(248, 29);
            this.lblCurrentUser.TabIndex = 6;
            this.lblCurrentUser.Text = "Current User Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefillStock);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnListCustomers);
            this.groupBox1.Controls.Add(this.btnJoiningApplication);
            this.groupBox1.Controls.Add(this.btnListProducts);
            this.groupBox1.Controls.Add(this.btnAddProduct);
            this.groupBox1.Controls.Add(this.btnListCategories);
            this.groupBox1.Controls.Add(this.btnListUsers);
            this.groupBox1.Controls.Add(this.btnListPeople);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 153);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1289, 398);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Interfaces";
            // 
            // btnRefillStock
            // 
            this.btnRefillStock.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnRefillStock.BackgroundImage = global::PharmacySystem.Properties.Resources.stock__1_;
            this.btnRefillStock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnRefillStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefillStock.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnRefillStock.Location = new System.Drawing.Point(731, 224);
            this.btnRefillStock.Name = "btnRefillStock";
            this.btnRefillStock.Size = new System.Drawing.Size(229, 148);
            this.btnRefillStock.TabIndex = 8;
            this.btnRefillStock.Text = "Refill Stock";
            this.btnRefillStock.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRefillStock.UseVisualStyleBackColor = false;
            this.btnRefillStock.Click += new System.EventHandler(this.btnRefillStock_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.button1.BackgroundImage = global::PharmacySystem.Properties.Resources.sheet;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Location = new System.Drawing.Point(496, 224);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(229, 148);
            this.button1.TabIndex = 7;
            this.button1.Text = "List Selling history";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnListCustomers
            // 
            this.btnListCustomers.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnListCustomers.BackgroundImage = global::PharmacySystem.Properties.Resources.rating;
            this.btnListCustomers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnListCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListCustomers.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnListCustomers.Location = new System.Drawing.Point(261, 224);
            this.btnListCustomers.Name = "btnListCustomers";
            this.btnListCustomers.Size = new System.Drawing.Size(229, 148);
            this.btnListCustomers.TabIndex = 6;
            this.btnListCustomers.Text = "List Customers";
            this.btnListCustomers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnListCustomers.UseVisualStyleBackColor = false;
            this.btnListCustomers.Click += new System.EventHandler(this.btnListCustomers_Click);
            // 
            // btnJoiningApplication
            // 
            this.btnJoiningApplication.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnJoiningApplication.BackgroundImage = global::PharmacySystem.Properties.Resources.Add_Person_72;
            this.btnJoiningApplication.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnJoiningApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoiningApplication.ForeColor = System.Drawing.SystemColors.Control;
            this.btnJoiningApplication.Location = new System.Drawing.Point(966, 50);
            this.btnJoiningApplication.Name = "btnJoiningApplication";
            this.btnJoiningApplication.Size = new System.Drawing.Size(229, 148);
            this.btnJoiningApplication.TabIndex = 5;
            this.btnJoiningApplication.Text = "Joining Applications";
            this.btnJoiningApplication.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnJoiningApplication.UseVisualStyleBackColor = false;
            this.btnJoiningApplication.Click += new System.EventHandler(this.btnJoiningApplication_Click);
            // 
            // btnListProducts
            // 
            this.btnListProducts.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnListProducts.BackgroundImage = global::PharmacySystem.Properties.Resources.products;
            this.btnListProducts.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnListProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListProducts.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnListProducts.Location = new System.Drawing.Point(26, 224);
            this.btnListProducts.Name = "btnListProducts";
            this.btnListProducts.Size = new System.Drawing.Size(229, 148);
            this.btnListProducts.TabIndex = 4;
            this.btnListProducts.Text = "List Products";
            this.btnListProducts.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnListProducts.UseVisualStyleBackColor = false;
            this.btnListProducts.Click += new System.EventHandler(this.btnListProducts_Click);
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnAddProduct.BackgroundImage = global::PharmacySystem.Properties.Resources.shopping_bag;
            this.btnAddProduct.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddProduct.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAddProduct.Location = new System.Drawing.Point(731, 50);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(229, 148);
            this.btnAddProduct.TabIndex = 3;
            this.btnAddProduct.Text = "Add Product";
            this.btnAddProduct.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAddProduct.UseVisualStyleBackColor = false;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnListCategories
            // 
            this.btnListCategories.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnListCategories.BackgroundImage = global::PharmacySystem.Properties.Resources.dashboard;
            this.btnListCategories.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnListCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListCategories.ForeColor = System.Drawing.SystemColors.Control;
            this.btnListCategories.Location = new System.Drawing.Point(496, 50);
            this.btnListCategories.Name = "btnListCategories";
            this.btnListCategories.Size = new System.Drawing.Size(229, 148);
            this.btnListCategories.TabIndex = 2;
            this.btnListCategories.Text = "List Categories";
            this.btnListCategories.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnListCategories.UseVisualStyleBackColor = false;
            this.btnListCategories.Click += new System.EventHandler(this.btnListCategories_Click);
            // 
            // btnListUsers
            // 
            this.btnListUsers.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnListUsers.BackgroundImage = global::PharmacySystem.Properties.Resources.Users_2_400;
            this.btnListUsers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnListUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListUsers.ForeColor = System.Drawing.SystemColors.Control;
            this.btnListUsers.Location = new System.Drawing.Point(261, 50);
            this.btnListUsers.Name = "btnListUsers";
            this.btnListUsers.Size = new System.Drawing.Size(229, 148);
            this.btnListUsers.TabIndex = 1;
            this.btnListUsers.Text = "List Users";
            this.btnListUsers.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnListUsers.UseVisualStyleBackColor = false;
            this.btnListUsers.Click += new System.EventHandler(this.btnListUsers_Click);
            // 
            // btnListPeople
            // 
            this.btnListPeople.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.btnListPeople.BackgroundImage = global::PharmacySystem.Properties.Resources.People_400;
            this.btnListPeople.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnListPeople.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListPeople.ForeColor = System.Drawing.SystemColors.Control;
            this.btnListPeople.Location = new System.Drawing.Point(26, 50);
            this.btnListPeople.Name = "btnListPeople";
            this.btnListPeople.Size = new System.Drawing.Size(229, 148);
            this.btnListPeople.TabIndex = 0;
            this.btnListPeople.Text = "List People";
            this.btnListPeople.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnListPeople.UseVisualStyleBackColor = false;
            this.btnListPeople.Click += new System.EventHandler(this.button1_Click);
            // 
            // msMain
            // 
            this.msMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userInfoToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(1339, 40);
            this.msMain.TabIndex = 171;
            this.msMain.Text = "menuStrip1";
            // 
            // userInfoToolStripMenuItem
            // 
            this.userInfoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profileToolStripMenuItem1,
            this.changePasswordToolStripMenuItem,
            this.logoutToolStripMenuItem1});
            this.userInfoToolStripMenuItem.Image = global::PharmacySystem.Properties.Resources.Person_32;
            this.userInfoToolStripMenuItem.Name = "userInfoToolStripMenuItem";
            this.userInfoToolStripMenuItem.Size = new System.Drawing.Size(114, 36);
            this.userInfoToolStripMenuItem.Text = "User Info";
            // 
            // profileToolStripMenuItem1
            // 
            this.profileToolStripMenuItem1.Image = global::PharmacySystem.Properties.Resources.PersonDetails_32;
            this.profileToolStripMenuItem1.Name = "profileToolStripMenuItem1";
            this.profileToolStripMenuItem1.Size = new System.Drawing.Size(207, 26);
            this.profileToolStripMenuItem1.Text = "Profile";
            this.profileToolStripMenuItem1.Click += new System.EventHandler(this.profileToolStripMenuItem1_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Image = global::PharmacySystem.Properties.Resources.Password_32;
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(207, 26);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem1
            // 
            this.logoutToolStripMenuItem1.Image = global::PharmacySystem.Properties.Resources.logout;
            this.logoutToolStripMenuItem1.Name = "logoutToolStripMenuItem1";
            this.logoutToolStripMenuItem1.Size = new System.Drawing.Size(207, 26);
            this.logoutToolStripMenuItem1.Text = "Logout";
            this.logoutToolStripMenuItem1.Click += new System.EventHandler(this.logoutToolStripMenuItem1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox2.Image = global::PharmacySystem.Properties.Resources.Person_32;
            this.pictureBox2.Location = new System.Drawing.Point(23, 75);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(58, 54);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(-3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1344, 626);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1339, 621);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnListPeople;
        private System.Windows.Forms.Button btnListUsers;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnListCategories;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnListProducts;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem userInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem1;
        private System.Windows.Forms.Button btnJoiningApplication;
        private System.Windows.Forms.Button btnListCustomers;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRefillStock;
    }
}