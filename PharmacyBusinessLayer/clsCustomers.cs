using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsCustomers
    {
        enum enMode { AddNew = 0, Update = 1 }
        private enMode Mode = enMode.AddNew;

        public int CustomerID {  get; set; }
        public int PersonID {  get; set; }
        clsPerson Person;
        public int Balance {  get; set; }

        public int CartID = -1;
        public clsCarts Cart;
 

        public clsCustomers() 
        {
            Mode = enMode.AddNew;
            CustomerID = -1; PersonID = -1; Balance = -1;
        }

        public clsCustomers(int customerID, int personID, int balance)
        {
            Mode = enMode.Update;
            CustomerID = customerID;
            PersonID = personID;
            Person = clsPerson.Find(PersonID);
            Balance = balance;
        }

        public static clsCustomers FindCustomerByID(int CustomerID)
        {
            int PersonID = -1, Balance = -1;

            bool IsFound = clsCustomersData.GetCustomerInfoByID(CustomerID, ref PersonID, ref Balance);

            if(IsFound) return new clsCustomers(CustomerID, PersonID, Balance);
            else return null;
        }

        public static clsCustomers FindCustomerByPersonID(int PersonID)
        {
            int CustomerID = -1, Balance = -1;

            bool IsFound = clsCustomersData.GetCustomerInfoByPersonID(PersonID, ref CustomerID, ref Balance);

            if (IsFound) return new clsCustomers(CustomerID, PersonID, Balance);
            else return null;
        }
      
        private bool _AddNewCustomer() 
        {
            this.CustomerID = clsCustomersData.AddNewCustomer(PersonID, Balance);
            return this.CustomerID != -1;
        }

        private bool _UpdateCustomer()
        {
            return clsCustomersData.UpdateCustomer(CustomerID, PersonID, Balance);
        }

        public bool Save() 
        {
            switch (Mode)
            {   
                case enMode.AddNew:
                    if (_AddNewCustomer()) 
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCustomer();
            }return false;
        }

        public static DataTable GetAllCustomers() 
        {
            return clsCustomersData.GetAllCustomers();
        }

        public static bool IsCustomerExist(int CustomerID) 
        {
            return clsCustomersData.IsCustomerExist(CustomerID);
        }

        public static bool DoesCustomerHaveCart(int CustomerID) 
        {
            return clsCustomersData.DoesCustomerHaveCart(CustomerID);
        }

        public bool DoesCustomerHaveCart()
        {
            return clsCustomersData.DoesCustomerHaveCart(CustomerID);
        }

        public clsCarts CreateCart()
        {
            Cart = new clsCarts();
            Cart.CustomerID = CustomerID;
            if (Cart.Save()) 
            {
                Mode = enMode.Update;
                CartID = Cart.CartID;
                return Cart;
            }
            else 
            {
                return null;
            }
        }
    }
}
