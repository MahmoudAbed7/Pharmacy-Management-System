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
    public partial class frmListAllCustomers : Form
    {
        private static DataTable _dtAllCustomers = clsCustomers.GetAllCustomers();
        public DataTable _dtCustomers = _dtAllCustomers.DefaultView.ToTable(false, "CustomerID", "PersonID",
             "FullName", "Gender", "Phone", "Email");
        public frmListAllCustomers()
        {
            InitializeComponent();
        }

        private void frmListAllCustomers_Load(object sender, EventArgs e)
        {
            _dtCustomers = clsCustomers.GetAllCustomers();
            dgvCustomers.DataSource = _dtCustomers;
            lblRecordsCount.Text = dgvCustomers.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvCustomers.Rows.Count > 0)
            {
                dgvCustomers.Columns[0].HeaderText = "Customer ID";
                dgvCustomers.Columns[0].Width = 110;

                dgvCustomers.Columns[1].HeaderText = "Person ID";
                dgvCustomers.Columns[1].Width = 110;

                dgvCustomers.Columns[2].HeaderText = "Full Name";
                dgvCustomers.Columns[2].Width = 220;

                dgvCustomers.Columns[3].HeaderText = "Gender";
                dgvCustomers.Columns[3].Width = 120;
                
                dgvCustomers.Columns[4].HeaderText = "Phone";
                dgvCustomers.Columns[4].Width = 120;
                
                dgvCustomers.Columns[5].HeaderText = "Email";
                dgvCustomers.Columns[5].Width = 170;
            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text) 
            {
                case "Customer ID":
                    FilterColumn = "CustomerID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Gender":
                    FilterColumn = "Gender";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if(cbFilterBy.Text == "None" || txtFilterValue.Text == "") 
            {
                _dtCustomers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvCustomers.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Customer ID")
            {
                _dtCustomers.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            }
            else
            {
                _dtCustomers.DefaultView.RowFilter = $"[{FilterColumn}] Like '{txtFilterValue.Text.Trim()}%' ";
            }

            lblRecordsCount.Text = dgvCustomers.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            frmListAllCustomers_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CustomerID = (int)dgvCustomers.CurrentRow.Cells[0].Value;
            frmShowCustomerInfo frm = new frmShowCustomerInfo(CustomerID);
            frm.ShowDialog();
            frmListAllCustomers_Load(null, null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Customer ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
            frmListAllCustomers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvCustomers.CurrentRow.Cells[1].Value;
            frmAddUpdatePerson frm = new frmAddUpdatePerson(PersonID);
            frm.ShowDialog();
            frmListAllCustomers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete Customer [" + dgvCustomers.CurrentRow.Cells[0].Value + "]", "Confirm Delete", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)

            {
                int PersonID = (int)dgvCustomers.CurrentRow.Cells[1].Value;
                clsUser User = clsUser.FindByPersonID(PersonID);
                User.IsActive = false;
                if (User.Save())
                {
                    MessageBox.Show("Customer Deleted Successfully.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListAllCustomers_Load(null, null);
                }

                else
                    MessageBox.Show("Customer was not deleted because it has data linked to it.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void addBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CustomerID = (int)dgvCustomers.CurrentRow.Cells[0].Value;
            frmAddBalance frm = new frmAddBalance(CustomerID);
            frm.ShowDialog();
            frmListAllCustomers_Load(null, null);
        }
    }
}
