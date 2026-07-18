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

namespace PharmacySystem.Users
{
    public partial class frmListUsers : Form
    {
        DataTable _dtUsers = new DataTable();
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtUsers;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();

            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 100;

                dgvUsers.Columns[1].HeaderText = "User Name";
                dgvUsers.Columns[1].Width = 100;

                dgvUsers.Columns[2].HeaderText = "Person ID";
                dgvUsers.Columns[2].Width = 100;

                dgvUsers.Columns[3].HeaderText = "Joining Application ID";
                dgvUsers.Columns[3].Width = 100;

                dgvUsers.Columns[4].HeaderText = "Full Name";
                dgvUsers.Columns[4].Width = 200;

                dgvUsers.Columns[5].HeaderText = "Is Active";
                dgvUsers.Columns[5].Width = 50;

            }
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "Is Active")
            {
                txtFilterValue.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterValue.Visible = cbFilterBy.Text != "None";
                cbIsActive.Visible = false;
                if (txtFilterValue.Visible)
                {
                    txtFilterValue.Focus();
                    txtFilterValue.Text = "";
                }
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text) 
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "User Name":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "Joining Application ID":
                    FilterColumn = "ApplicationID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || cbFilterBy.Text == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
                return;
            }


            if (cbFilterBy.Text != "Full Name" && cbFilterBy.Text != "User Name")
                //in this case we deal with numbers not string.
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue) 
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }

            if(cbIsActive.Text == "All")
                _dtUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblRecordsCount.Text = _dtUsers.Rows.Count.ToString();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers frm = new frmAddUpdateUsers();
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmUserInfo frm = new frmUserInfo(UserID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers frm = new frmAddUpdateUsers();
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmAddUpdateUsers frm = new frmAddUpdateUsers(UserID);
            frm.ShowDialog();

            frmListUsers_Load(null, null);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            if (clsUser.DeleteUser(UserID))
            {
                MessageBox.Show("User has been deleted successfully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListUsers_Load(null, null);
            }

            else
                MessageBox.Show("User is not delted due to data connected to it.", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text == "User ID" || cbFilterBy.Text == "Person ID") 
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void ChangePasswordtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)dgvUsers.CurrentRow.Cells[0].Value;
            frmChangePassword frm = new frmChangePassword(UserID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListUsers_Activated(object sender, EventArgs e)
        {
            btnAddUser.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateUsers);
            toolStripMenuItem1.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateUsers);
            editToolStripMenuItem.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateUsers);
            deleteToolStripMenuItem.Enabled = clsGlobal.AccessPermission(clsUser.enPermission.AddUpdateUsers);
        }
    }
}
