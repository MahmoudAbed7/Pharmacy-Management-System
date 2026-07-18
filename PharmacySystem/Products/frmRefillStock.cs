using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Products
{
    public partial class frmRefillStock : Form
    {
        public frmRefillStock()
        {
            InitializeComponent();
        }

        private void txtCount_Validating(object sender, CancelEventArgs e)
        {
            if (txtCount.Text == string.Empty) 
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCount, "This field is required");
            }
            else 
            {
                e.Cancel = false;
                errorProvider1.SetError(txtCount, null);
            }

        }

        private void txtCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ctrlProductCardWithFilter1.SelectedProduct == null)
            {
                MessageBox.Show("Choose product to refill its stock", "Choose Product"
                   , MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ctrlProductCardWithFilter1.SelectedProduct.Quantity += int.Parse(txtCount.Text);
            if (ctrlProductCardWithFilter1.SelectedProduct.Save()) 
            {
                MessageBox.Show("Stock refilled Successfully.", "Refilled"
                    , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else 
            {
                MessageBox.Show("An error occurred during Saving data."
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
