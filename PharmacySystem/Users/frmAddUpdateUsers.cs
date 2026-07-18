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
    public partial class frmAddUpdateUsers : Form
    {
        enum enMode { AddNew, Update}
        enMode _Mode = enMode.AddNew;

        int _UserID = -1;
        clsUser _User;
        clsUserJoiningApplication _Application;
        clsCustomers _Customer;
        int _CustomerID = -1;
        int _ApplicationID = -1;
        public frmAddUpdateUsers()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdateUsers(int ApplicationID, int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
            _ApplicationID = ApplicationID;
            ctrlPersonCardWithFilter1.LoadPersonInfo(PersonID);
        }

        public frmAddUpdateUsers(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = enMode.Update;
        }

        private void _ResetDefaultValues() 
        {
            if (_Mode == enMode.AddNew) 
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
                _User = new clsUser();
                _Customer = new clsCustomers();
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.FilterFocus();
            }
            else 
            {
                lblTitle.Text = "Update New User";
                this.Text = "Update New User";

                tpLoginInfo.Enabled = true;
                btnSave.Enabled = true ;
            }
        }

        private void _LoadUserInfo() 
        {
            _User = clsUser.FindByID(_UserID);
            ctrlPersonCardWithFilter1.FilterEnabled = false;
            if (_User == null) 
            {
                MessageBox.Show("No User with ID = " + _User, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            //txtPassword.Text = _User.Password;
            //txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;

            chkAll.Checked = _User.checkPermission((int)clsUser.enPermission.All);
            chkListPeople.Checked = _User.checkPermission((int)clsUser.enPermission.ListPeople);
            chkAddUpdatePeople.Checked = _User.checkPermission((int)clsUser.enPermission.AddUpdatePeople);
            chkListUsers.Checked = _User.checkPermission((int)clsUser.enPermission.ListUsers);
            chkAddUpdateUsers.Checked = _User.checkPermission((int)clsUser.enPermission.AddUpdateUsers);
            chkAddUpdateJoiningApplication.Checked = _User.checkPermission((int)clsUser.enPermission.JoiningApplication);
            chkListProducts.Checked = _User.checkPermission((int)clsUser.enPermission.ListProducts);
            chkAddUpdateProduct.Checked = _User.checkPermission((int)clsUser.enPermission.AddUpdateProducts);


            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);
        }

        private void frmAddUpdateUsers_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update) _LoadUserInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(_Mode == enMode.Update) 
            {
                btnNext.Enabled = true;
                btnSave.Enabled = true;
                tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                return;
            }

            if(ctrlPersonCardWithFilter1.PersonID != -1) 
            {
                if (clsUser.IsUserExistForPersonID(ctrlPersonCardWithFilter1.PersonID)) 
                {
                    MessageBox.Show("Selected Person already has a user, choose another one.", "Select another Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ctrlPersonCardWithFilter1.FilterFocus();
                }
                else 
                {
                    btnSave.Enabled = true;
                    tpLoginInfo.Enabled = true;
                    tcUserInfo.SelectedTab = tcUserInfo.TabPages["tpLoginInfo"];
                    return;
                }
            }
            else
            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ctrlPersonCardWithFilter1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "Username cannot be blank");
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }

            if (_Mode == enMode.AddNew)
            {
                if (clsUser.IsUserExistByUserName(txtUserName.Text))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "username is used by another user");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                }
            }
            else
            {
                if (_User.UserName != txtUserName.Text.Trim())
                {
                    if (clsUser.IsUserExistByUserName(txtUserName.Text))
                    {
                        e.Cancel = true;
                        errorProvider1.SetError(txtUserName, "username is used by another user");
                        return;
                    }
                    else
                    {
                        errorProvider1.SetError(txtUserName, null);
                    }
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password dosen't match");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) 
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro",
                 "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = ctrlPersonCardWithFilter1.PersonID;
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = clsSecurity.ComputeHash(txtPassword.Text);


                _User.Permissions = 0;
            if (chkAll.Checked) _User.Permissions = -1;
            if (chkListPeople.Checked) _User.Permissions += (int)clsUser.enPermission.ListPeople;
            if (chkAddUpdatePeople.Checked) _User.Permissions += (int)clsUser.enPermission.AddUpdatePeople;
            if (chkListUsers.Checked) _User.Permissions += (int)clsUser.enPermission.ListUsers;
            if (chkAddUpdateUsers.Checked) _User.Permissions += (int)clsUser.enPermission.AddUpdateUsers;
            if (chkAddUpdateJoiningApplication.Checked) _User.Permissions += (int)clsUser.enPermission.JoiningApplication;
            if (chkAddUpdateProduct.Checked) _User.Permissions += (int)clsUser.enPermission.AddUpdateProducts;
            if (chkListProducts.Checked) 
            {
                _User.Permissions += (int)clsUser.enPermission.ListProducts;
                if(_Mode == enMode.AddNew) 
                {
                    _Customer.PersonID = ctrlPersonCardWithFilter1.PersonID;
                    _Customer.Balance = 0;

                    if(_Customer.Save())
                    {
                        _Mode = enMode.Update;
                        MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else 
                    {
                        MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            _User.IsActive = chkIsActive.Checked;


            if (_User.Save())
            {
                lblUserID.Text = _User.UserID.ToString();
                lblTitle.Text = "Update User";
                this.Text = "Update User";

                _Mode = enMode.Update;

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            _Application = clsUserJoiningApplication.FindByPersonID(ctrlPersonCardWithFilter1.PersonID);
            if (_Application.Status == (int)clsUserJoiningApplication.enStatus.New) 
            {
                _Application.CompleteApplication();
                _Application.ApplicationDate = DateTime.Now;
            } 
            else
                return;

        }

        private void chkListProducts_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox temp = (CheckBox)sender;
            if (!temp.Checked)
            {
                MessageBox.Show("Define the permission", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
