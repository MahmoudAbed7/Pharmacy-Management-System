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

namespace PharmacySystem.People.controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
        public event EventHandler<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID) 
        {
            OnPersonSelected?.Invoke(this, PersonID);
        }

        private bool _ShowAppPerson = true;
        public bool ShowAppPerson 
        {
            get { return _ShowAppPerson; }
            set 
            {
                _ShowAppPerson = value;
                btnAddNewPerson.Enabled = _ShowAppPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled 
        {
            get { return _FilterEnabled; }
            set 
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private bool _FilterValue = true;
        public bool FilterValue
        {
            get { return _FilterValue; }
            set
            {
                _FilterValue = value;
                txtFilterValue.Enabled = _FilterEnabled;
            }
        }


        private int _PersonID;
        public int PersonID { get { return ctrlPersonCard1.PersonID; } }
        public clsPerson SelectedPerson { get {  return ctrlPersonCard1.Person; } }
        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void _FindNow() 
        {
            ctrlPersonCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
            if (OnPersonSelected != null && FilterEnabled) PersonSelected(ctrlPersonCard1.PersonID);
        }
        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = PersonID.ToString();
            _FindNow();
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren()) 
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fields are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FindNow();
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson frm = new frmAddUpdatePerson();
            frm.Databack += DataEventBack;
            frm.ShowDialog();
        }

        private void DataEventBack(object sender, int PersonID) 
        {
            cbFilterBy.Text = "Person ID";
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadPersonInfo(PersonID);
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (txtFilterValue.Text == string.Empty)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == 13) btnFind.PerformClick();
        }
        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            txtFilterValue.Focus();
            cbFilterBy.Text = "Person ID";
        }

    }
}
