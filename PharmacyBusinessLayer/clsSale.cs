using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsSale
    {
        private enum enMode { AddNew = 0, Update = 1,}
        private enMode _Mode = enMode.AddNew;

        public enum enStatus { New = 0, Cancelled = 1, Completed}

        public int SaleID {  get; set; }
        public int CartID {  get; set; }
        public clsCarts Cart;
        public byte SaleStatus { get; set; }
        public int AddedByUserID { get; set; }

        public DateTime SaleDate {  get; set; }
       
        public clsSale() 
        {
            _Mode = enMode.AddNew;
            SaleID = -1; CartID = -1; SaleDate = DateTime.Now;
            AddedByUserID = -1; SaleStatus = (byte)enStatus.New;
        }

        public clsSale(int saleID, int cartID, enStatus saleStatus, int addedByUserID, DateTime saleDate)
        {
            _Mode = enMode.Update;
            SaleID = saleID;
            CartID = cartID;
            Cart = clsCarts.FindCartByID(cartID);
            SaleDate = saleDate;
            AddedByUserID = addedByUserID;
            SaleStatus = (byte)saleStatus;
        }

        public static clsSale FnidByID(int SaleID) 
        {
            int cartID = -1; DateTime SaleDate = DateTime.Now; int TotalAmount = 0;
            int AddedByUserID = -1; byte SaleStatus = (byte)enStatus.New;

            bool IsFound = clsSalesData.GetSaleInfoByID(SaleID, ref cartID, ref SaleStatus
                , ref AddedByUserID, ref SaleDate);

            if(IsFound) return new clsSale(SaleID, cartID, (enStatus)SaleStatus, AddedByUserID, SaleDate);
            else return null;
        }

        private bool _AddNewSale()
        {
            this.SaleID = clsSalesData.AddNewSale(CartID, SaleStatus, AddedByUserID, SaleDate);
            return this.SaleID != -1;
        }

        private bool _UpdateSale() 
        {
            return clsSalesData.UpdateSale(SaleID, CartID, SaleStatus, AddedByUserID, SaleDate);
        }

        public bool Save() 
        {
            switch (_Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewSale()) 
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateSale();
            }return false;
        }

        public static DataTable GetAllSales() 
        {
            return clsSalesData.GetAllSales();
        }

        public static bool checkout(int CartID, int AddedByUserID) 
        {
            return clsSalesData.Checkout(CartID, AddedByUserID);
        }

        public bool checkout(int AddedByUserID)
        {
            return clsSalesData.Checkout(CartID, AddedByUserID);
        }

        public static bool CheckBalance(int CartID) 
        {
            return clsSalesData.CheckBalanceSufficent(CartID);
        }

        public  bool CheckBalance()
        {
            return clsSalesData.CheckBalanceSufficent(CartID);
        }
    }
}
