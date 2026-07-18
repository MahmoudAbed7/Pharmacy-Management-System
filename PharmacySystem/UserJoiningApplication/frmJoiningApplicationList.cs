using PharmacyBusinessLayer;
using PharmacySystem.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.UserJoiningApplication
{
    public partial class frmJoiningApplicationList : Form
    {
        DataTable _dtApplications = new DataTable();
        clsUserJoiningApplication _Application;
        public frmJoiningApplicationList()
        {
            InitializeComponent();
        }

        private void frmJoiningApplication_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtApplications = clsUserJoiningApplication.GetAllApplications();
            dgvApplications.DataSource = _dtApplications;
            lblRecordsCount.Text = dgvApplications.Rows.Count.ToString();

            if (dgvApplications.Rows.Count > 0) 
            {
                dgvApplications.Columns[0].HeaderText = "Application ID";
                dgvApplications.Columns[0].Width =  200;

                dgvApplications.Columns[1].HeaderText = "Person ID";
                dgvApplications.Columns[1].Width = 100;

                dgvApplications.Columns[2].HeaderText = "Full Name";
                dgvApplications.Columns[2].Width = 200;

                dgvApplications.Columns[3].HeaderText = "Phone";
                dgvApplications.Columns[3].Width = 100;

                dgvApplications.Columns[4].HeaderText = "Status";
                dgvApplications.Columns[4].Width = 100;

                dgvApplications.Columns[5].HeaderText = "Application Date";
                dgvApplications.Columns[5].Width = 200;
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
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (cbFilterBy.Text == "None" || txtFilterValue.Text == "") 
            {
                _dtApplications.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvApplications.Rows.Count.ToString();
                return;
            }

            if(cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "Application ID") 
              _dtApplications.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            else
              _dtApplications.DefaultView.RowFilter = $"[{FilterColumn}] Like '{txtFilterValue.Text.Trim()}%'";

            lblRecordsCount.Text = dgvApplications.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvApplications.CurrentRow.Cells[1].Value;
            int ApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            frmAddUpdateUsers frm = new frmAddUpdateUsers(ApplicationID, PersonID);
            frm.ShowDialog();

            frmJoiningApplication_Load(null, null);
        }

        private void cmsApplication_Opening(object sender, CancelEventArgs e)
        {
            int ApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            _Application = clsUserJoiningApplication.FindByID(ApplicationID);
            if(_Application.Status == (int)clsUserJoiningApplication.enStatus.New) 
            {
                addUserToolStripMenuItem.Enabled = true;
            }
            else 
            {
                addUserToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
            }
           
        }

        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvApplications.CurrentRow.Cells[0].Value;
            clsUserJoiningApplication.CancelApplication(ApplicationID);
        }
    }
}
