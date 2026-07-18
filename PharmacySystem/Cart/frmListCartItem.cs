using PharmacyBusinessLayer;
using PharmacySystem.GlobalClasses;
using PharmacySystem.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Cart
{
    public partial class frmListCartItem : Form
    {
        private int _CartID = -1;
        private DataTable _dtItem;
        public frmListCartItem()
        {
            InitializeComponent();
        }

        public frmListCartItem(int CartID)
        {
            InitializeComponent();
            _CartID = CartID;
        }

        private void frmListCartItem_Load(object sender, EventArgs e)
        {
            if(_CartID == -1) 
            {
                MessageBox.Show("This customer dosen't have cart yet", "Cart activation", MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
                return;
            }

            _dtItem = clsCartItms.GetAllItemsForCartID(_CartID);
            dgvItems.DataSource = _dtItem;
            cbFilterBy.SelectedIndex = 0;
            btnCheckout.Enabled = dgvItems.Rows.Count > 0;
            showDetailsToolStripMenuItem.Enabled = dgvItems.Rows.Count > 0;
            editToolStripMenuItem.Enabled = dgvItems.Rows.Count > 0;
            deleteToolStripMenuItem.Enabled = dgvItems.Rows.Count > 0;
            lblRecordsCount.Text = dgvItems.Rows.Count.ToString();

            if(dgvItems.Rows.Count > 0) 
            {
                dgvItems.Columns[0].HeaderText = "Item ID";
                dgvItems.Columns[0].Width = 50;

                dgvItems.Columns[1].HeaderText = "Product Name";
                dgvItems.Columns[1].Width = 150;

                dgvItems.Columns[2].HeaderText = "Price";
                dgvItems.Columns[2].Width = 50;

                dgvItems.Columns[3].HeaderText = "Quantity";
                dgvItems.Columns[3].Width = 100;

                dgvItems.Columns[4].HeaderText = "Total Price";
                dgvItems.Columns[4].Width = 150;

                dgvItems.Columns[5].HeaderText = "Addition Date";
                dgvItems.Columns[5].Width = 200;
            }
            else
            {
                MessageBox.Show("Your cart is empty", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            int AddedByUserID = clsGlobal.CurrentUser.UserID;
            if (!clsSale.CheckBalance(_CartID)) 
            {
                MessageBox.Show("Add more balance", "unsufficent balance"
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(clsSale.checkout(_CartID, AddedByUserID)) 
            {
                MessageBox.Show("Purchase done successfully", "Purchase", MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
                frmListCartItem_Load(null, null);
            }
            else 
            {
                MessageBox.Show("Purchase is failed", "Purchase", MessageBoxButtons.OK
                   , MessageBoxIcon.Error);
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
            if (txtFilterValue.Visible) 
            {
                txtFilterValue.Focus();
                txtFilterValue.Text = "";
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text) 
            {
                case "Item ID":
                    FilterColumn = "ItemID";
                    break;
                case "Product Name":
                    FilterColumn = "ProductName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (cbFilterBy.Text == "None" || txtFilterValue.Text == "") 
            {
                _dtItem.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvItems.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Item ID") 
                _dtItem.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _dtItem.DefaultView.RowFilter = $"[{FilterColumn}] like '{txtFilterValue.Text.Trim()}%'";
            lblRecordsCount.Text = dgvItems.Rows.Count.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Item ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ProductID = clsProduct.FindByName((string)dgvItems.CurrentRow.Cells[1].Value).ProductID;
            frmShowProductInfo frm = new frmShowProductInfo(ProductID);
            frm.ShowDialog();
            frmListCartItem_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete [" + dgvItems.CurrentRow.Cells[1].Value + "] from your cart", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {

                //Perform Delele and refresh
                if (clsCartItms.DeleteItem((int)dgvItems.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Product Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListCartItem_Load(null, null);
                }

                else
                    MessageBox.Show("Product was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmFindProduct frm = new frmFindProduct();
            frm.ShowDialog();
            frmListCartItem_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ItemID = (int)dgvItems.CurrentRow.Cells[0].Value;
            frmIncreaseQuantity frm = new frmIncreaseQuantity(ItemID);
            frm.ShowDialog();
            frmListCartItem_Load(null, null);
        }
    }
}
