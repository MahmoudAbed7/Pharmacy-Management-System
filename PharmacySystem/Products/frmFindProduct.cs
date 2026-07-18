using PharmacyBusinessLayer;
using PharmacySystem.Customers.Controls;
using PharmacySystem.GlobalClasses;
using PharmacySystem.Products.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PharmacySystem.Products
{
    public partial class frmFindProduct : Form
    {
        private clsCustomers _Customer;
        private clsCarts _Cart;
        private clsCartItms _Item;
        public frmFindProduct()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            btnAddToCart.Enabled = ctrlProductCardWithFilter1.SelectedProduct.Quantity < 1;
            if (ctrlProductCardWithFilter1.SelectedProduct.Quantity < 1)
            {
                MessageBox.Show("insufficent stock", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!_Customer.DoesCustomerHaveCart())
            {
                _Cart = _Customer.CreateCart();
                if (_Cart == null)
                {
                    MessageBox.Show($"Cart activation is failed for customer with id {_Customer.CustomerID}", "Activate Cart"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show($"Cart is activated for customer with id {_Customer.CustomerID}", "Activate Cart"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



            if (clsCartItms.DoesItemFindInTheCart(_Customer.CartID, ctrlProductCardWithFilter1.ProductID))
            {
                _Item = clsCartItms.FindByProductID(ctrlProductCardWithFilter1.ProductID);
                if (_Item != null)
                {
                    _Item.Quantity += 1;
                    if (_Item.Save())
                    {
                        MessageBox.Show("Item added successfully in your cart", "Add Item", MessageBoxButtons.OK
                            , MessageBoxIcon.Information);
                        _Item.Mode = clsCartItms.enMode.Update;
                    }
                    return;
                }
            }

            _Item.CartID = _Customer.CartID;
            _Item.ProductID = ctrlProductCardWithFilter1.ProductID;
            _Item.Quantity = 1;

            if (_Item.Save())
            {
                MessageBox.Show("Item added successfully in your cart", "Add Item", MessageBoxButtons.OK
                    , MessageBoxIcon.Information);
                _Item.Mode = clsCartItms.enMode.Update;
            }
            else
            {
                MessageBox.Show("Item added failed in your cart", "Add Item", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }

        }

        private void frmFindProduct_Load(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null)
            {

                btnAddToCart.Enabled = false;
                MessageBox.Show("You should have an account to purchase products", "Information"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _Customer = clsCustomers.FindCustomerByPersonID(clsGlobal.CurrentUser.PersonID);
            if (_Customer == null)
            {
                MessageBox.Show($"Customer with person id {clsGlobal.CurrentUser.PersonID} not found"
                    , "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnAddToCart.Enabled = _Customer != null;
                return;
            }

            if (_Customer.DoesCustomerHaveCart())
            {

                _Cart = clsCarts.FindCartByCustomerID(_Customer.CustomerID);
                if (_Cart == null)
                {
                    return;
                }
                _Customer.CartID = _Cart.CartID;

            }

            _Item = new clsCartItms();
        }
    }
}
