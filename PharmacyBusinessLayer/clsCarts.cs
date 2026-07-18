using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsCarts
    {
        private enum enMode { AddNew, Update};
        enMode _Mode = enMode.AddNew;

        public int CartID {  get; set; }
        public int CustomerID {  get; set; }
        public clsCustomers Customer;

        public clsCarts() 
        {
            _Mode = enMode.AddNew;
            CartID = -1; CustomerID = -1;
        }

        public clsCarts(int cartID, int customerID)
        {
            CartID = cartID;
            CustomerID = customerID;
            Customer = clsCustomers.FindCustomerByID(customerID);
            _Mode = enMode.Update;
        }

        public static clsCarts FindCartByID(int CartID) 
        {
            int customerID = -1;

            bool IsFonud = clsCartsData.GetCartByID(CartID, ref customerID);
            if (IsFonud) return new clsCarts(CartID, customerID);
            else return null;
        }

        public static clsCarts FindCartByCustomerID(int CustomerID)
        {
            int cartID = -1;

            bool IsFonud = clsCartsData.GetCartByCustomerID(CustomerID, ref cartID);
            if (IsFonud) return new clsCarts(cartID, CustomerID);
            else return null;
        }

        private bool _AddNewCart() 
        {
            this.CartID = clsCartsData.AddNewCart(CustomerID);
            return this.CartID != -1;
        }

        public bool Save() 
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewCart()) 
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return true;
            }
            return false;
        }
    }
}
