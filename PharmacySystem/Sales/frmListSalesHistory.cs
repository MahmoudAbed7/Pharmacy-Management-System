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

namespace PharmacySystem.Sales
{
    public partial class frmListSalesHistory : Form
    {
        private DataTable _dtSalesHistory;
        public frmListSalesHistory()
        {
            InitializeComponent();
        }

        private void frmListSalesHistory_Load(object sender, EventArgs e)
        {
            _dtSalesHistory = clsSale.GetAllSales();
            dgvSales.DataSource = _dtSalesHistory;
            lblRecordsCount.Text = dgvSales.Rows.Count.ToString();
            cbFilterBy.SelectedIndex = 0;

            if (dgvSales.Rows.Count != 0)
            {
                dgvSales.Columns[0].HeaderText = "Sale ID";
                dgvSales.Columns[0].Width = 40;

                dgvSales.Columns[1].HeaderText = "Cart ID";
                dgvSales.Columns[1].Width = 40;

                dgvSales.Columns[2].HeaderText = "Sale Date";
                dgvSales.Columns[2].Width = 100;

                dgvSales.Columns[3].HeaderText = "Product Name";
                dgvSales.Columns[3].Width = 200;

                dgvSales.Columns[4].HeaderText = "Qty";
                dgvSales.Columns[4].Width = 60;

                dgvSales.Columns[5].HeaderText = "Unit Price";
                dgvSales.Columns[5].Width = 120;

                dgvSales.Columns[6].HeaderText = "Line Total";
                dgvSales.Columns[6].Width = 120;

                dgvSales.Columns[7].HeaderText = "Invoice Total";
                dgvSales.Columns[7].Width = 120;
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
            string filterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Sale ID":
                    filterColumn = "SaleID";
                    break;
                case "Cart ID":
                    filterColumn = "CartID";
                    break;
                case "Product Name":
                    filterColumn = "ProductName";
                    break;
                default:
                    filterColumn = "None";
                    break;
            }

            if (cbFilterBy.Text == "None" || txtFilterValue.Text == string.Empty)
            {
                _dtSalesHistory.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvSales.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Sale ID" || cbFilterBy.Text == "Cart ID")
            {
                _dtSalesHistory.DefaultView.RowFilter = $"[{filterColumn}] = {txtFilterValue.Text.Trim()}";
            }
            else
            {
                _dtSalesHistory.DefaultView.RowFilter = $"[{filterColumn}] Like '{txtFilterValue.Text.Trim()}%' ";
            }
            lblRecordsCount.Text = dgvSales.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Sale ID" || cbFilterBy.Text == "Cart ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
