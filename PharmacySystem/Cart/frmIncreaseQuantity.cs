using PharmacyBusinessLayer;
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
    public partial class frmIncreaseQuantity : Form
    {
        private int _ItemID;
        private clsProduct _Product;
        public frmIncreaseQuantity(int ItemID)
        {
            _ItemID = ItemID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void txtQuantity_Validating(object sender, CancelEventArgs e)
        {
            if (txtQuantity.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtQuantity, "this field is required");
            }
            else 
            {
                e.Cancel = false;
                errorProvider1.SetError(txtQuantity, null);
            }

            _Product = clsProduct.FindByID(ctrlItemInfo1.ItemInfo.ProductID);
            if (_Product.Quantity < int.Parse(txtQuantity.Text)) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtQuantity, "insufficent stock");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtQuantity, null);
            }
        }

        private void btnAddBalance_Click(object sender, EventArgs e)
        {
            ctrlItemInfo1.ItemInfo.Quantity = int.Parse(txtQuantity.Text);
            if (ctrlItemInfo1.ItemInfo.Save())
            {
                MessageBox.Show("Quantity of this item has changed", "Change Quantity"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);   
            }
        }

        private void frmIncreaseQuantity_Load(object sender, EventArgs e)
        {
            ctrlItemInfo1.LoadItemInfo(_ItemID);
            txtQuantity.Text = ctrlItemInfo1.ItemInfo.Quantity.ToString();
        }
    }
}
