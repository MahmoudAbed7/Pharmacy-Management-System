using PharmacyBusinessLayer;
using PharmacySystem.People.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Customers.Controls
{
    public partial class ctrlCustomersWithFilter : UserControl
    {
        public event EventHandler<int> OnCustomerSelected;
        protected virtual void CustomerSelected(int CustomerID) 
        {
            OnCustomerSelected?.Invoke(this, CustomerID);
        }

        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get { return _ShowAddPerson; }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Enabled = _ShowAddPerson;
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

        private int _CustomerID = -1;
        public int CustomerID { get {  return ctrlCustomerCard1.CustomerID; } } 
        public clsCustomers SelectedCustomerInfo { get { return ctrlCustomerCard1.Customer; } }

        private void _FindNow() 
        {
            if (cbFilterBy.Text == "Customer ID")
                ctrlCustomerCard1.LoadCustomerData(int.Parse(txtFilterValue.Text));
            else
                ctrlCustomerCard1.LoadCustomerDataByPersonID(int.Parse(txtFilterValue.Text));

            if (OnCustomerSelected != null && FilterEnabled) CustomerSelected(ctrlCustomerCard1.CustomerID);
        }

        public void LoadCustomerInfo(int CustomerID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = CustomerID.ToString();
            _FindNow();
        }

        public void LoadCustomerInfoByPersonID(int PersonID)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            _FindNow();
        }
        public ctrlCustomersWithFilter()
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
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == 13) btnFind.PerformClick();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void ctrlCustomersWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.Text = "Customer ID";
            txtFilterValue.Focus();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.Databack += DataEventBack;
            frm.ShowDialog();
        }

        private void DataEventBack(object sender, int PersonID) 
        {
            cbFilterBy.Text = "Person ID";
            txtFilterValue.Text = PersonID.ToString();
            ctrlCustomerCard1.LoadCustomerDataByPersonID(PersonID);
        }
    }
}
