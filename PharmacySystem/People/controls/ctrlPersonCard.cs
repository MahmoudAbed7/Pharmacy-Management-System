using PharmacyBusinessLayer;
using PharmacySystem.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.People.controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID;
        public int PersonID { get { return _PersonID; } }

        private clsPerson _Person;
        public clsPerson Person {  get { return _Person; } }

        public void resetPersonInfo() 
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Man_32;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;
        }

        private void _LoadPersonImage() 
        {
            if (_Person.Gender == 0) pbPersonImage.Image = Resources.Male_512;
            else pbPersonImage.Image = Resources.Female_512;

            string ImageUrl = _Person.ImageUrl;
            if (ImageUrl != "") 
            {
                if (File.Exists(ImageUrl))
                {
                    pbPersonImage.ImageLocation = ImageUrl;
                }
                else 
                {
                    MessageBox.Show("Could not find this image: = " + ImageUrl
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            
        }

        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblFullName.Text = _Person.FullName;


            if (_Person.Gender == 0)
            {
                lblGendor.Text = "Male";
            }
            else 
            {
                lblGendor.Text = "Female";
            }


            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = _Person.CountryInfo.CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
        }

        public void LoadPersonInfo(int PersonID) 
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                resetPersonInfo();
                MessageBox.Show("No Person with ID = " + PersonID
                    , "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _FillPersonInfo();
        }
        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void llEditPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson(_PersonID);
            frm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }
    }
}
