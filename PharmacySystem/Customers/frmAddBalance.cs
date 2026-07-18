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

namespace PharmacySystem.Customers
{
    public partial class frmAddBalance : Form
    {
        private int _CustomerID = -1;
        private clsCustomers _Customer;

        public frmAddBalance(int CustomerID)
        {
            _CustomerID = CustomerID;
            InitializeComponent();
        }

        private void txtBalanceValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddBalance_Load(object sender, EventArgs e)
        {
            _Customer = clsCustomers.FindCustomerByID(_CustomerID);
            if (_Customer == null) 
            {
                MessageBox.Show("No Customer with ID = " + _CustomerID, "Customer Dosen't Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblCustomerID.Text = _Customer.CustomerID.ToString();
            lblPersonID.Text = _Customer.PersonID.ToString();
            lblFullName.Text = clsPerson.Find(_Customer.PersonID).FullName;
            txtBalanceValue.Text = "0";
        }

        private void btnAddBalance_Click(object sender, EventArgs e)
        {
            if(int.Parse(txtBalanceValue.Text) < 0) 
            {
                MessageBox.Show($"You can't add negative numbers", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _Customer.Balance += int.Parse(txtBalanceValue.Text);
            if (_Customer.Save()) 
            {
                MessageBox.Show($"Balance added successfully for customer with id: {_CustomerID}" +
                    $", your current balance is {_Customer.Balance}", "Balance Added"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show($"Balance addition failed for customer with id: {_CustomerID}", "Addition Error"
                   , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
