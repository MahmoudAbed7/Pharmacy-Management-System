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

namespace PharmacySystem.Category
{
    public partial class frmListCategories : Form
    {
        static DataTable _dt = clsCategory.GetAllCategories();
        //DataTable _dt = _dtAll.DefaultView.ToTable(false, "CategoryID"
        //        , "CategoryName", "Description", "AddedByUserID");
        public frmListCategories()
        {
            InitializeComponent();
        }

        private void frmListCategories_Load(object sender, EventArgs e)
        {
            cbFilterBy.Text = "None";
            _dt = clsCategory.GetAllCategories();
            dgvCategory.DataSource = _dt;
            lblRecordsCount.Text = dgvCategory.Rows.Count.ToString();

            if (dgvCategory.Rows.Count > 0) 
            {
                dgvCategory.Columns[0].HeaderText = "Category ID";
                dgvCategory.Columns[0].Width = 100;

                dgvCategory.Columns[1].HeaderText = "Category Name";
                dgvCategory.Columns[1].Width = 150;

                dgvCategory.Columns[2].HeaderText = "Description";
                dgvCategory.Columns[2].Width = 300;

                dgvCategory.Columns[3].HeaderText = "Last Updated Date";
                dgvCategory.Columns[3].Width = 150;

                dgvCategory.Columns[4].HeaderText = "Added By User ID";
                dgvCategory.Columns[4].Width = 150;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = cbFilterBy.Text != "None";
            if (cbFilterBy.Text != "None") 
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
                case "Category ID":
                    FilterColumn = "CategoryID";
                    break;
                case "Category Name":
                    FilterColumn = "CategoryName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text == "" || cbFilterBy.Text == "None") 
            {
                _dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvCategory.Rows.Count.ToString();
                return;
            }

            if (cbFilterBy.Text == "Category ID")
                _dt.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
                _dt.DefaultView.RowFilter = $"[{FilterColumn}] Like '{txtFilterValue.Text.Trim()}%' ";
            lblRecordsCount.Text = dgvCategory.Rows.Count.ToString();

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdateCategory frm = new frmAddUpdateCategory();
            frm.ShowDialog();

            frmListCategories_Load(null, null);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNewCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateCategory frm = new frmAddUpdateCategory();
            frm.ShowDialog();

            frmListCategories_Load(null, null);
        }

        private void updateCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int CategoryID = (int)dgvCategory.CurrentRow.Cells[0].Value;
            frmAddUpdateCategory frm = new frmAddUpdateCategory(CategoryID);
            frm.ShowDialog();

            frmListCategories_Load(null, null);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "Category ID") 
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
