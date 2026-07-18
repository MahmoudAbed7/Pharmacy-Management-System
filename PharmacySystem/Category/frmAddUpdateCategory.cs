using PharmacyBusinessLayer;
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

namespace PharmacySystem.Category
{
    public partial class frmAddUpdateCategory : Form
    {
        enum enMode { AddNew, Update}
        enMode _Mode = enMode.AddNew;
        int _CategoryID = -1;
        clsCategory _Category;
        public frmAddUpdateCategory()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateCategory(int CategoryID)
        {
            InitializeComponent();
            _CategoryID = CategoryID;
            _Mode = enMode.Update;
        }

        private void _ResetDefaultValue() 
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Category";
                this.Text = "Add New Category";
                txtCategoryName.Text = "";
                txtDescription.Text = "";
                _Category = new clsCategory();
            }
            else 
            {
                lblTitle.Text = "Update Category";
                this.Text = "Update Category";
            }
        }

        private void _LoadCategoryInfo()
        {
            _Category = clsCategory.Find(_CategoryID);
            if (_Category == null) 
            {
                MessageBox.Show("No Category with ID = " + _CategoryID, "Category Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblCategoryID.Text = _Category.CategoryID.ToString();
            txtCategoryName.Text = _Category.CategoryName;
            txtDescription.Text = _Category.Description;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) 
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _Category.CategoryName = txtCategoryName.Text;
            _Category.Description = txtDescription.Text;
            _Category.LastUpdateDate = DateTime.Now;
            _Category.AddedByUserID = clsGlobal.CurrentUser.UserID;

            if (_Category.Save()) 
            {
                lblCategoryID.Text = _Category.CategoryID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Category";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void frmAddUpdateCategory_Load(object sender, EventArgs e)
        {
            _ResetDefaultValue();
            if (_Mode == enMode.Update) _LoadCategoryInfo();
        }

        private void txtCategoryName_Validating(object sender, CancelEventArgs e)
        {
            if(txtCategoryName.Text == string.Empty) 
            {
                errorProvider1.SetError(txtCategoryName, "This field is required");
                e.Cancel = true;
            }
            else 
            {
                errorProvider1.SetError(txtCategoryName, null);
                e.Cancel = false;
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (txtDescription.Text == string.Empty)
            {
                errorProvider1.SetError(txtDescription, "This field is required");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
                e.Cancel = false;
            }
        }
    }
}
