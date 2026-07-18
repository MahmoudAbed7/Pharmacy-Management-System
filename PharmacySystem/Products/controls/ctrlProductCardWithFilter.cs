using PharmacyBusinessLayer;
using PharmacySystem.People.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Products.controls
{
    public partial class ctrlProductCardWithFilter : UserControl
    {
        public delegate void ProductInfoEventHandler(int ProductID);
        public event ProductInfoEventHandler OnProductSelected;

        private bool _ShowAppProduct = true;
        public bool ShowAppProduct
        {
            get { return _ShowAppProduct; }
            set
            {
                _ShowAppProduct = value;
                btnAddNewProduct.Enabled = _ShowAppProduct;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private bool _FilterValue = true;
        public bool FilterValue
        {
            get { return _FilterValue; }
            set
            {
                _FilterValue = value;
                txtFilterValue.Enabled = _FilterEnabled;
            }
        }

        private int _ProductID;
        public int ProductID { get { return ctrlProductCard1.ProductID; } }
        public clsProduct SelectedProduct { get { return ctrlProductCard1.ProductInfo; } }

        private void _FindNow() 
        {
            switch(cbFilterBy.Text)
            {
                case "Product ID":
                    ctrlProductCard1.LoadProductInfo(int.Parse(txtFilterValue.Text));
                    break;
                case "Product Name":
                    ctrlProductCard1.LoadProductInfo(txtFilterValue.Text);
                    break;
            }
            if (OnProductSelected != null && FilterEnabled) OnProductSelected.Invoke(ctrlProductCard1.ProductID);
        }

        public void LoadProductInfo(int ProductID) 
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = ProductID.ToString();
            _FindNow();
        }
        public ctrlProductCardWithFilter()
        {
            InitializeComponent();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fields are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FindNow();
        }

        private void btnAddNewProduct_Click(object sender, EventArgs e)
        {
            frmAddUpdateProducts frm = new frmAddUpdateProducts();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        public void DataBackEvent(int ProductID) 
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = ProductID.ToString();
            ctrlProductCard1.LoadProductInfo(ProductID);
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (txtFilterValue.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }

            if (e.KeyChar == 13) btnFind.PerformClick();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void ctrlProductCardWithFilter_Load(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
            cbFilterBy.Text = "Product ID";
        }

    }
}
