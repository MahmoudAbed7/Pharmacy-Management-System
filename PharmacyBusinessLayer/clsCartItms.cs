using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsCartItms
    {
        public enum enMode { AddNew, Update};
        public enMode Mode = enMode.AddNew;

        public int ItemID {  get; set; }
        public int CartID {  get; set; }
        public clsCarts Cart;
        public int ProductID {  get; set; }
        public clsProduct Product;

        public int Quantity {  get; set; }

        public clsCartItms() 
        {
            Mode = enMode.AddNew;
            ItemID = -1; CartID = -1; ProductID = -1; Quantity = -1;
        }

        public clsCartItms(int itemID, int cartID, int productID, int quantity)
        {
            Mode = enMode.Update;
            ItemID = itemID;
            CartID = cartID;
            Cart = clsCarts.FindCartByID(cartID);
            ProductID = productID;
            Product = clsProduct.FindByID(productID);
            Quantity = quantity;
        }

        public static clsCartItms FindByID(int ItemID) 
        {
            int cartID = -1, productID = -1, quantity = 0;

            bool IsFound = clsCartItmsData.GetCartItemByID(ItemID, ref cartID, ref productID, ref quantity);

            if(IsFound) return new clsCartItms(ItemID, cartID, productID, quantity);
            else return null;
        }

        public static clsCartItms FindByCartID(int CartID)
        {
            int itemID = -1, productID = -1, quantity = 0;

            bool IsFound = clsCartItmsData.GetCartItemByCartID(CartID, ref itemID, ref productID, ref quantity);

            if (IsFound) return new clsCartItms(itemID, CartID, productID, quantity);
            else return null;
        }

        public static clsCartItms FindByProductID(int ProductID)
        {
            int itemID = -1, cartID = -1, quantity = 0;

            bool IsFound = clsCartItmsData.GetCartItemByProductID(ProductID, ref itemID, ref cartID, ref quantity);

            if (IsFound) return new clsCartItms(itemID, cartID, ProductID, quantity);
            else return null;
        }

        private bool _AddNewItem()
        {
            this.ItemID = clsCartItmsData.AddNewCartItem(CartID, ProductID, Quantity);
            return this.ItemID != -1;
        }

        private bool _UpdateItem()
        {
            return clsCartItmsData.UpdateItem(ItemID, CartID, ProductID, Quantity);
        }

        public static bool DeleteItem(int ItemID)
        {
            return clsCartItmsData.DeleteItem(ItemID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewItem())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateItem();
            }
            return false;
        }

        public static DataTable GetAllItemsForCartID(int CartID) 
        {
            return clsCartItmsData.GetAllCartItemsForCartID(CartID);
        }

        public DataTable GetAllItemsForCartID()
        {
            return clsCartItmsData.GetAllCartItemsForCartID(CartID);
        }

        public static bool DoesItemFindInTheCart(int CartID, int ProductID) 
        {
            return clsCartItmsData.DoesItemFindInTheCart(CartID, ProductID);
        }
    }
}
