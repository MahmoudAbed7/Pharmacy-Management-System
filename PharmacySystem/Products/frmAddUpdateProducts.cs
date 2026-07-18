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

namespace PharmacySystem.Products
{
    public partial class frmAddUpdateProducts : Form
    {
        public delegate void EventHandler(int ProductID);
        public event EventHandler DataBack;
        private enum enMode { AddNew =  0, Update = 1 }
        private enMode _Mode = enMode.AddNew;
        private int _ProductID = -1;
        private clsProduct _Product;

        private void _FillCategoriesMenu() 
        {
            DataTable dt = clsCategory.GetAllCategories();
            foreach (DataRow dr in dt.Rows) 
            {
                cbCategories.Items.Add(dr["CategoryName"]);
            }
        }

        private void _ResetDefaultProductsInfo() 
        {
            _FillCategoriesMenu();
            if(_Mode == enMode.AddNew) 
            {
                lblTitle.Text = "Add New Product";
                this.Text = "Add New Product";
                _Product = new clsProduct();
            }
            else 
            {
                lblTitle.Text = "Update New Product";
                this.Text = "Update New Product";
            }
            txtProductName.Text = "";
            txtDescription.Text = "";
            txtCount.Text = "0";
            txtPrice.Text = "0";
            cbCategories.SelectedIndex = 0;
            dtpProductionDate.Value = DateTime.Now;
            dtpExpiryDate.Value = DateTime.Now;
           llRemoveImage.Visible = pbProductImage.ImageLocation != null;
            pbProductImage.Image = Resources.beauty_and_cosmetics;
        }

        private bool _handleProductImage() 
        {
            if(_Product.ImageUrl != pbProductImage.ImageLocation) 
            {
                try
                {
                    File.Delete(_Product.ImageUrl);
                }
                catch (Exception ex) { }
            }
            else 
            {
                if (pbProductImage.ImageLocation != "") 
                {
                    string imageSource = pbProductImage.ImageLocation;
                    if(clsUtil.CopyImageToProjectFolder(ref imageSource)) 
                    {
                        pbProductImage.ImageLocation = imageSource;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }return true;
        }
        private void _LoadProductData()
        {
            _Product = clsProduct.FindByID(_ProductID);
            if (_Product != null)
            {
                lblProductID.Text = _Product.ProductID.ToString();
                txtProductName.Text = _Product.Name;
                txtDescription.Text = _Product.Description;
                txtCount.Text = _Product.Quantity.ToString();
                txtPrice.Text = _Product.Price.ToString();
                dtpProductionDate.Value = _Product.ProducingDate;
                dtpExpiryDate.Value = _Product.ExpiryDate;
                cbCategories.Text = clsCategory.Find(_Product.CategoryID).CategoryName;
                if(_Product.ImageUrl != null) pbProductImage.ImageLocation = _Product.ImageUrl;
                llRemoveImage.Visible = _Product.ImageUrl != "";
            }
            else 
            {
                MessageBox.Show("No product with ID = " + _ProductID, "Product Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
        }
        public frmAddUpdateProducts()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            
        }

        public frmAddUpdateProducts(int ProductID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _ProductID = ProductID;
        }

        private void frmAddUpdateProducts_Load(object sender, EventArgs e)
        {
            _ResetDefaultProductsInfo();
            if (_Mode == enMode.Update) _LoadProductData();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files| *.JPEG; *.png; *.jpg; *.gif; *.bmp";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;
            if(openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                string fileName = openFileDialog1.FileName;
                pbProductImage.ImageLocation = fileName;
                llRemoveImage.Visible = false;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            llRemoveImage.Visible = true;
            pbProductImage.ImageLocation = null;
        }

        private void txtProductName_Validating(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;
            if(temp.Text == string.Empty) 
            {
                e.Cancel = true;
                errorProvider1.SetError(temp, "This field is required");
            }
            else 
            {
                e.Cancel = false;
                errorProvider1.SetError(temp, null);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren()) 
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_handleProductImage()) return;

            _Product.Name = txtProductName.Text;
            _Product.Price = int.Parse(txtPrice.Text);
            _Product.Description = txtDescription.Text;
            _Product.Quantity += int.Parse(txtCount.Text);
            _Product.CategoryID = clsCategory.Find(cbCategories.Text).CategoryID;
            _Product.ProducingDate = dtpProductionDate.Value;
            _Product.ExpiryDate = dtpExpiryDate.Value;
            _Product.LastRefillTime = DateTime.Now;
            _Product.LastRefillTime = DateTime.Now;

            if (pbProductImage.ImageLocation != null)
                _Product.ImageUrl = pbProductImage.ImageLocation;
            else
                _Product.ImageUrl = "";

            _Product.AddedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Product.Save()) 
            {
                lblProductID.Text = _Product.ProductID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Product";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DataBack?.Invoke(_Product.ProductID);

            }
            else
            {
                MessageBox.Show("An error occurred during Saving data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
