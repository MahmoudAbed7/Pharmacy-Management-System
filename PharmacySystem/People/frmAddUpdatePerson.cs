using PharmacyBusinessLayer;
using PharmacySystem.GlobalClasses;
using PharmacySystem.Login;
using PharmacySystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem
{
    public partial class frmAddUpdatePerson : Form
    {
        public Action<object, int> Databack
            = (sender, PersonID) => _Person.PersonID = PersonID;
        enum enMode { AddNew, Update};
        enMode _Mode = enMode.AddNew;
        public static clsPerson _Person;
        private int _PersonID;
        private clsUserJoiningApplication _Application;

        private void _FillCountriesComboBox()
        {
            DataTable dt = clsCountry.GetAllCountries();
            foreach (DataRow row in dt.Rows) 
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _ResetDefaultValues() 
        {
            _FillCountriesComboBox();
            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                this.Text = "Add New Person";
                _Person = new clsPerson();
                _Application = new clsUserJoiningApplication();
            }
            else 
            {
                lblTitle.Text = "Update Person";
                this.Text = "Update Person";
            }

            llRemoveImage.Visible = pbPersonImage.ImageLocation != null;
            cbCountry.SelectedItem = clsCountry.Find("Ireland").CountryName;
            if (rbMale.Checked) pbPersonImage.Image = Resources.Male_512;
            else pbPersonImage.Image = Resources.Female_512;
                rbMale.Checked = true;
        }

        private void _LoadData()
        {
            _Person = clsPerson.Find(_PersonID);
            _Application = clsUserJoiningApplication.FindByPersonID(_Person.PersonID);
            if (_Person == null && _Application == null) 
            {
                MessageBox.Show("No Person with ID = " + _PersonID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gender == 0) rbMale.Checked = true;
            else rbFemale.Checked = true;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            cbCountry.Text = clsCountry.Find(_Person.NationailtyCountryID).CountryName;
            txtAddress.Text = _Person.Address;
            if (_Person.ImageUrl != "") pbPersonImage.Load(_Person.ImageUrl);
            llRemoveImage.Visible = (_Person.ImageUrl != "");

        }

        private bool _HandlePersonImage() 
        {
            if(_Person.ImageUrl != pbPersonImage.ImageLocation) 
            {
                try
                {
                    File.Delete(_Person.ImageUrl);
                }
                catch (Exception ex) { }
            }
            else 
            {
                
                if(pbPersonImage.ImageLocation != "") 
                {
                    string ImageSource = pbPersonImage.ImageLocation;
                    if (clsUtil.CopyImageToProjectFolder(ref ImageSource)) 
                    {
                        pbPersonImage.ImageLocation = ImageSource;
                        return true;
                    }
                    else 
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }return true;
        }

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
            
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();
            if (_Mode == enMode.Update) _LoadData();
            
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files| *.JPEG; *.png; *.jpg; *.gif; *.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) 
            {
                string FileName = openFileDialog1.FileName;
                pbPersonImage.Load(FileName);
                llRemoveImage.Visible = true;
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;
            if (rbMale.Checked) pbPersonImage.Image = Resources.Male_512;
            else pbPersonImage.Image = Resources.Female_512;
            llRemoveImage.Visible = false;

        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null) pbPersonImage.Image = Resources.Male_512;
        }

        private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null) pbPersonImage.Image = Resources.Female_512;
        }

       

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                return;
            }
            else
            {
                if (!clsValidation.EmailValidate(txtEmail.Text)) 
                {
                    errorProvider1.SetError(txtEmail, "Invalid email format");
                    e.Cancel = true;
                }
                else 
                {
                    errorProvider1.SetError(txtEmail, null);
                    e.Cancel = false;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            if (clsGlobal.CurrentUser == null) 
            {
                frmLogin frm = new frmLogin();
                frm.ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren()) 
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandlePersonImage()) return;
            int NationalityCountryID = clsCountry.Find(cbCountry.Text).CountryID;

            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            _Person.ThirdName = txtThirdName.Text.Trim();
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Phone = txtPhone.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (rbMale.Checked)
                _Person.Gender = 0;
            else
                _Person.Gender = 1;

            _Person.NationailtyCountryID = NationalityCountryID;

            if (pbPersonImage.ImageLocation != null)
                _Person.ImageUrl = pbPersonImage.ImageLocation;
            else
                _Person.ImageUrl = "";


            if (_Person.Save()) 
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                _Mode = enMode.Update;
                lblTitle.Text = "Update Person";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //DataBackEventHandler(this, _Person.PersonID);
                Databack?.Invoke(this, _Person.PersonID);
            }
            else 
            {
                MessageBox.Show("An error occurred during Saving data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_Application.Status == (int)clsUserJoiningApplication.enStatus.New)
            {
                _Application.PersonID = _Person.PersonID;
                _Application.Status = (int)clsUserJoiningApplication.enStatus.New;
                _Application.ApplicationDate = DateTime.Now;
                if (_Application.Save())
                {
                    MessageBox.Show("Your Application under checking for specifing the proper permissions"
                        , "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Mode = enMode.Update;
                }
                else
                {
                    MessageBox.Show("An error occurred during Saving data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            TextBox temp = (TextBox)sender;

            if (temp.Text.Trim() == string.Empty) 
            {
                errorProvider1.SetError(temp, "This field is required");
                e.Cancel = true;
            }
            else 
            {
                errorProvider1.SetError(temp, null);
                e.Cancel = false;
            }
        }
    }
}
