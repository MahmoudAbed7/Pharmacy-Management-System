using PharmacyBusinessLayer;
using PharmacySystem.People.controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacySystem.Cart.Control
{
    public partial class ctrlItemInfo : UserControl
    {
        private int _ItemID = -1;
        public int ItemID { get { return _ItemID; } }

        private clsCartItms _Item;
        public clsCartItms ItemInfo {  get { return _Item; } }

        private void _ResetInfo() 
        {
            ctrlProductCard1.resetProductInfo();
            lblCartID.Text = "??";
            lblItemID.Text = "??";
            lblQuantity.Text = "??";
        }

        public void LoadItemInfo(int ItemID)
        {
            _ItemID = ItemID;
            _Item = clsCartItms.FindByID(ItemID);

            if (_Item == null)
            {
                _ResetInfo();
                MessageBox.Show("No Item with id = " + ItemID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillItemInfo();
        }


        private void _FillItemInfo()
        {
            ctrlProductCard1.LoadProductInfo(_Item.ProductID);
            lblItemID.Text = _Item.ItemID.ToString();
            lblCartID.Text = _Item.CartID.ToString();
            lblQuantity.Text = _Item.Quantity.ToString();
        }

        public ctrlItemInfo()
        {
            InitializeComponent();
        }

    }
}
