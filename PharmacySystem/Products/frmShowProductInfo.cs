using PharmacyBusinessLayer;
using PharmacySystem.Cart;
using PharmacySystem.GlobalClasses;
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
    public partial class frmShowProductInfo : Form
    {

        private clsCustomers _Customer;
        private clsCarts _Cart;
        private clsCartItms _Item;
        private clsProduct _Product;
        public frmShowProductInfo(int ProductID)
        {
            InitializeComponent();
            ctrlProductCard1.LoadProductInfo(ProductID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
           
            btnAddToCart.Enabled = ctrlProductCard1.ProductInfo.Quantity < 1;
            if (ctrlProductCard1.ProductInfo.Quantity < 1)
            {
                MessageBox.Show("insufficent stock", "Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!_Customer.DoesCustomerHaveCart())
            {
                _Cart = _Customer.CreateCart();
                if( _Cart == null) 
                {
                    MessageBox.Show($"Cart activation is failed for customer with id {_Customer.CustomerID}", "Activate Cart"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show($"Cart is activated for customer with id {_Customer.CustomerID}", "Activate Cart"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            
         
            if(clsCartItms.DoesItemFindInTheCart(_Customer.CartID, ctrlProductCard1.ProductID)) 
            {
                _Item = clsCartItms.FindByProductID(ctrlProductCard1.ProductID);
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
            _Item.ProductID = ctrlProductCard1.ProductID;
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

        private void frmShowProductInfo_Load(object sender, EventArgs e)
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
