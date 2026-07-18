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

namespace PharmacySystem.Customers.Controls
{
    public partial class ctrlCustomerCard : UserControl
    {
        private int _CustomerID = -1;
        public int CustomerID { get { return _CustomerID; } }

        private clsCustomers _Customer;
        private clsCarts _Carts;
        public clsCustomers Customer {  get { return _Customer; } }
        public clsCarts Cart {  get { return _Carts; } }
        public ctrlCustomerCard()
        {
            InitializeComponent();
        }

        private void _ResetInfo() 
        {
            ctrlPersonCard1.resetPersonInfo();
            lblBalance.Text = "??";
            lblCustomerID.Text = "??";
            lblCartID.Text = "??";
        }

        public void LoadCustomerData(int CustomerID) 
        {
            _CustomerID = CustomerID;
            _Customer = clsCustomers.FindCustomerByID(CustomerID);

            if (_Customer == null) 
            {
                _ResetInfo();
                MessageBox.Show("No Customer with id = " + CustomerID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillCustomerInfo();
        }


        public void LoadCustomerDataByPersonID(int PersonID)
        {
            _Customer = clsCustomers.FindCustomerByPersonID(PersonID);

            if (_Customer == null)
            {
                _ResetInfo();
                MessageBox.Show("No Customer with person id = " + PersonID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillCustomerInfo();
        }

        private void _FillCustomerInfo() 
        {
            ctrlPersonCard1.LoadPersonInfo(_Customer.PersonID);
            lblBalance.Text = _Customer.Balance.ToString();
            lblCustomerID.Text = _Customer.CustomerID.ToString();

            _Carts = clsCarts.FindCartByCustomerID(_Customer.CustomerID);
            if (_Carts != null) lblCartID.Text = _Carts.CartID.ToString();
            else lblCartID.Text = "-1";
        }

        private void llAddBalance_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddBalance frm = new frmAddBalance(Customer.CustomerID);
            frm.ShowDialog();
            LoadCustomerData(Customer.CustomerID);
        }
    }
}
