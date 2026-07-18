using PharmacyBusinessLayer;
using PharmacySystem.GlobalClasses;
using PharmacySystem.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUserNameAndPassword(txtUserName.Text.Trim(), clsSecurity.ComputeHash(txtPassword.Text.Trim()));

            if (User != null)
            {
                if (chkRememberMe.Checked)
                {
                    clsGlobal.RememberUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUserNameAndPassword("", "");
                }

                if (!User.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your accound is not Active, Contact Admin.", "In Active Account", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.CurrentUser = User;
                this.Hide();
                if(clsGlobal.CurrentUser.Permissions == (int)clsUser.enPermission.ListProducts) 
                {
                    frmListAllProducts frm1 = new frmListAllProducts();
                    frm1.ShowDialog();
                    return;
                }
                frmMain frm = new frmMain(this);
                frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid Username/Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = ""; string Password = "";
            if (clsGlobal.GetStoredCredentialFromRegistry(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void llSignUp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.ShowDialog();
        }
    }
}
