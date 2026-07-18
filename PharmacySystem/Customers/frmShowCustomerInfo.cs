using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Customers
{
    public partial class frmShowCustomerInfo : Form
    {
        int _CustomerID = -1;
        public frmShowCustomerInfo(int CustomerID)
        {
            _CustomerID = CustomerID;
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowCustomerInfo_Load(object sender, EventArgs e)
        {
            ctrlCustomerCard1.LoadCustomerData(_CustomerID);
        }
    }
}
