using PharmacyBusinessLayer;
using PharmacySystem.Cart;
using PharmacySystem.Customers;
using PharmacySystem.GlobalClasses;
using PharmacySystem.Login;
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

namespace PharmacySystem.Products
{
    public partial class frmListAllProducts : Form
    {
        
        private static DataTable _dtAllProducts = clsProduct.GetAllProducts();
        public DataTable _dtProducts = _dtAllProducts.DefaultView.ToTable(false, "ProductID", "Name"
            , "CategoryName", "ProducingDate", "ExpiryDate", "Quantity", "Price");

        private clsCustomers _Customer;
        public frmListAllProducts()
        {
            InitializeComponent();
            if (clsGlobal.CurrentUser != null) btnCart.Enabled = true;
        }

        private void frmListAllProducts_Load(object sender, EventArgs e)
        {
            _dtProducts = clsProduct.GetAllProducts();
            dgvProducts.DataSource = _dtProducts;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvProducts.Rows.Count.ToString();
            if (dgvProducts.Rows.Count != 0) 
            {
                dgvProducts.Columns[0].HeaderText = "Product ID";
                dgvProducts.Columns[0].Width = 40;

                dgvProducts.Columns[1].HeaderText = "Name";
                dgvProducts.Columns[1].Width = 150;

                dgvProducts.Columns[2].HeaderText = "Category";
                dgvProducts.Columns[2].Width = 150;

                dgvProducts.Columns[3].HeaderText = "Production Date";
                dgvProducts.Columns[3].Width = 150;

                dgvProducts.Columns[4].HeaderText = "Expiry Date";
                dgvProducts.Columns[4].Width = 150;

                dgvProducts.Columns[5].HeaderText = "Quntity";
                dgvProducts.Columns[5].Width = 40;

                dgvProducts.Columns[6].HeaderText = "Price";
                dgvProducts.Columns[6].Width = 40;

                dgvProducts.Columns[7].HeaderText = "Refill Date";
                dgvProducts.Columns[7].Width = 150;

                dgvProducts.Columns[8].HeaderText = "Last Oper Date";
                dgvProducts.Columns[8].Width = 150;

                dgvProducts.Columns[9].HeaderText = "Added By User ID";
                dgvProducts.Columns[9].Width = 150;
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateProducts frm = new frmAddUpdateProducts();
            frm.ShowDialog();
            frmListAllProducts_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ProductID = (int)dgvProducts.CurrentRow.Cells[0].Value;
            frmShowProductInfo frm = new frmShowProductInfo(ProductID);
            frm.ShowDialog();
            frmListAllProducts_Load(null, null);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateProducts frm = new frmAddUpdateProducts();
            frm.ShowDialog();
            frmListAllProducts_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ProductID = (int)dgvProducts.CurrentRow.Cells[0].Value;
            frmAddUpdateProducts frm = new frmAddUpdateProducts(ProductID);
            frm.ShowDialog();
            frmListAllProducts_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";
            switch (cbFilterBy.Text) 
            {
                case "Product ID":
                    filterColumn = "ProductID";
                    break;
                case "Name":
                    filterColumn = "Name";
                    break;
                case "Category":
                    filterColumn = "CategoryName";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (cbFilterBy.Text == "None" || txtFilterValue.Text == string.Empty)
            {
                _dtProducts.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvProducts.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Product ID")
            {
                _dtProducts.DefaultView.RowFilter = $"[{filterColumn}] = {txtFilterValue.Text.Trim()}";
            }
            else
            {
                _dtProducts.DefaultView.RowFilter = $"[{filterColumn}] Like '{txtFilterValue.Text.Trim()}%' ";
            }
            lblRecordsCount.Text = dgvProducts.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtFilterValue.Text == "Product ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Product [" + dgvProducts.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsProduct.DeleteProduct((int)dgvProducts.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Product Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListAllProducts_Load(null, null);
                }

                else
                    MessageBox.Show("Product was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmListAllProducts_Activated(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser != null) 
            {
                btnLogin.Visible = false; 
                llSignUp.Visible = false;


                btnAddProduct.Visible = clsGlobal.CurrentUser.Permissions != (int)clsUser.enPermission.ListProducts;
                addBalanceToolStripMenuItem.Visible = clsGlobal.CurrentUser.Permissions == (int)clsUser.enPermission.ListProducts;
                showDetailsToolStripMenuItem.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.ListProducts);
                toolStripMenuItem1.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateProducts);
                editToolStripMenuItem.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateProducts);
                deleteToolStripMenuItem.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateProducts);
                refillStockToolStripMenuItem.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateProducts);

            }
            
            else 
            {
                btnAddProduct.Visible = false;
                msProducts.Visible = false;
                showDetailsToolStripMenuItem.Enabled = true;
                toolStripMenuItem1.Enabled = false;
                editToolStripMenuItem.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                refillStockToolStripMenuItem.Enabled = false;
            }


        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }

        private void llSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
        }

        private void profileToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _Customer = clsCustomers.FindCustomerByPersonID(clsGlobal.CurrentUser.PersonID);
            if (_Customer != null)
            {
               frmShowCustomerInfo frm = new frmShowCustomerInfo(_Customer.CustomerID);
                frm.ShowDialog();
            }
            else 
            {
                frmUserInfo frm = new frmUserInfo(clsGlobal.CurrentUser.UserID);
                frm.ShowDialog();
            }
        }

        private void logoutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            clsGlobal.CurrentUser = null;
            frmLogin frm = new frmLogin();
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void addBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = clsGlobal.CurrentUser.PersonID;
            clsCustomers Customer = clsCustomers.FindCustomerByPersonID(PersonID);
            frmAddBalance frm = new frmAddBalance(Customer.CustomerID);
            frm.ShowDialog();
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            int PersonID = clsGlobal.CurrentUser.PersonID;
            clsCustomers Customer= clsCustomers.FindCustomerByPersonID(PersonID);
            if (Customer == null)
            {
                MessageBox.Show("This user is not registered as a custmomer", "No Customer found"
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            clsCarts Cart = clsCarts.FindCartByCustomerID(Customer.CustomerID);
            frmListCartItem frm = new frmListCartItem(Cart.CartID);
            frm.ShowDialog();
            frmListAllProducts_Load(null, null);
        }

        private void refillStockToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int ProductID = (int)dgvProducts.CurrentRow.Cells[0].Value;
            frmAddUpdateProducts frm = new frmAddUpdateProducts(ProductID);
            frm.ShowDialog();
            frmListAllProducts_Load(null, null);
        }
    }
}
