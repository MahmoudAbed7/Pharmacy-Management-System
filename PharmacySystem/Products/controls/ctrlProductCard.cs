using PharmacyBusinessLayer;
using PharmacySystem.GlobalClasses;
using PharmacySystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Products.controls
{
    public partial class ctrlProductCard : UserControl
    {
        private int _ProductID;
        public int ProductID { get { return _ProductID; } }

        private clsProduct _Product;
        public clsProduct ProductInfo { get { return _Product; } }

        public void resetProductInfo()
        {
            _ProductID = -1;
            lblProductID.Text = "[????]";
            lblFullName.Text = "[????]";
            lblCategory.Text = "[????]";
            lblCount.Text = "[????]";
            lblPrice.Text = "[????]";
            lblAddedByUser.Text = "[????]";
            lblProductionDate.Text = "[????]";
            lblExpirationDate.Text = "[????]";
            lblLastOperDate.Text = "[????]";
            lblRefillingDate.Text = "[????]";
            pbProductImage.Image = Resources.beauty_and_cosmetics;
        }

        private void _LoadProductImage() 
        {
            string ImageUrl = _Product.ImageUrl;
            if (ImageUrl != "") 
            {
                if (File.Exists(ImageUrl)) 
                {
                    pbProductImage.ImageLocation = ImageUrl;
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + ImageUrl
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _FillProductInfo() 
        {
            llEditProductInfo.Enabled = true;
            _ProductID = _Product.ProductID;
            lblProductID.Text = _Product.ProductID.ToString();
            lblFullName.Text = _Product.Name;
            lblPrice.Text = _Product.Price.ToString() + "$";
            lblCount.Text = _Product.Quantity.ToString();
            lblCategory.Text = clsCategory.Find(_Product.CategoryID).CategoryName;
            lblAddedByUser.Text = clsUser.FindByID(_Product.AddedByUserID).UserName;
            lblProductionDate.Text = _Product.ProducingDate.ToShortDateString();
            lblExpirationDate.Text = _Product.ExpiryDate.ToShortDateString();
            lblRefillingDate.Text = _Product.LastRefillTime.ToShortDateString();
            lblLastOperDate.Text = _Product.LastOperationDate.ToShortDateString();
            _LoadProductImage();
        }

        public void LoadProductInfo(int ProductID)
        {
            _Product = clsProduct.FindByID(ProductID);
            if (_Product == null)
            {
                resetProductInfo();
                MessageBox.Show("No product with ID = " + ProductID
                    , "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _FillProductInfo();
        }

        public void LoadProductInfo(string ProductName)
        {
            _Product = clsProduct.FindByName(ProductName);
            if (_Product == null)
            {
                resetProductInfo();
                MessageBox.Show("No product with name = " + ProductName
                    , "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _FillProductInfo();
        }
        public ctrlProductCard()
        {
            InitializeComponent();
        }

        private void llEditProductInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdateProducts frm = new frmAddUpdateProducts(_ProductID);
            frm.ShowDialog();
        }

        private void ctrlProductCard_Load(object sender, EventArgs e)
        {
            if (clsGlobal.CurrentUser == null || clsGlobal.CurrentUser.Permissions == (int)clsUser.enPermission.ListProducts)
            {
                llEditProductInfo.Visible = false;
            }
        }
    }
}
