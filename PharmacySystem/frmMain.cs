using PharmacyBusinessLayer;
using PharmacySystem.Category;
using PharmacySystem.Customers;
using PharmacySystem.GlobalClasses;
using PharmacySystem.Login;
using PharmacySystem.People;
using PharmacySystem.Products;
using PharmacySystem.Sales;
using PharmacySystem.UserJoiningApplication;
using PharmacySystem.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem
{
    public partial class frmMain : Form
    {
        public frmMain(frmLogin frm)
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmListPeople frm = new frmListPeople();
            frm.ShowDialog();
        }

        private void btnListUsers_Click(object sender, EventArgs e)
        {
            frmListUsers frm = new frmListUsers(); 
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lblCurrentUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void btnListCategories_Click(object sender, EventArgs e)
        {
            frmListCategories frm = new frmListCategories();
            frm.ShowDialog();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            frmAddUpdateProducts frm = new frmAddUpdateProducts();
            frm.ShowDialog();
        }

        private void btnListProducts_Click(object sender, EventArgs e)
        {
            frmListAllProducts frm = new frmListAllProducts();
            frm.ShowDialog();
        }

        private void profileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            clsGlobal.CurrentUser = null;
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }

        private void btnJoiningApplication_Click(object sender, EventArgs e)
        {
            frmJoiningApplicationList frm = new frmJoiningApplicationList();
            frm.ShowDialog();
        }

        private void btnListCustomers_Click(object sender, EventArgs e)
        {
            frmListAllCustomers frm = new frmListAllCustomers();    
            frm.ShowDialog();
        }

        private void btnRefillStock_Click(object sender, EventArgs e)
        {
            frmRefillStock frm = new frmRefillStock(); 
            frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmListSalesHistory frm = new frmListSalesHistory();
            frm.ShowDialog();
        }
    }
}
