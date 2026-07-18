using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsProduct
    {
        enum enMode { AddNew, Update};
        enMode Mode = enMode.AddNew;

        public int ProductID {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryID { get; set; }
        public DateTime ProducingDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public DateTime LastRefillTime{ get; set; }
        public int AddedByUserID{ get; set; }
        public string ImageUrl{ get; set; }
        public DateTime LastOperationDate { get; set; }

        public clsProduct() 
        {
            Mode = enMode.AddNew;
            ProductID = -1; Name = ""; Description = ""; CategoryID = -1;
            ProducingDate = DateTime.Now; ExpiryDate = DateTime.Now; Quantity = 0;
            Price = 0; LastRefillTime = DateTime.Now; AddedByUserID = -1;
            ImageUrl = ""; LastOperationDate = DateTime.Now;
        }

        public clsProduct(int productID, string name, string description, int categoryID
            , DateTime producingDate, DateTime expiryDate, int quantity, int price
            , DateTime lastRefillTime, int addedByUserID, string imageUrl, DateTime lastOperationDate)
        {
            Mode = enMode.Update;
            ProductID = productID; Name = name; Description = description; CategoryID = categoryID;
            ProducingDate = producingDate; ExpiryDate = expiryDate; Quantity = quantity;
            Price = price; LastRefillTime = lastOperationDate; AddedByUserID = addedByUserID;
            ImageUrl = imageUrl; LastOperationDate = lastOperationDate;
        }

        public static clsProduct FindByID(int productID)
        {
            string name = ""; string description = ""; int categoryID = -1;
            DateTime producingDate = DateTime.Now; DateTime expiryDate = DateTime.Now;
            int quantity = 0; int price = 0; DateTime lastRefillTime = DateTime.Now; int addedByUserID = -1;
            string imageUrl = ""; DateTime lastOperationDate = DateTime.Now;

            bool IsFound = clsProductData.GetProductInfoByID(productID, ref name, ref description
                , ref categoryID, ref producingDate, ref expiryDate, ref quantity, ref price
                , ref lastRefillTime, ref addedByUserID, ref imageUrl, ref lastOperationDate);

            if (IsFound) return new clsProduct(productID, name, description, categoryID, producingDate
                , expiryDate, quantity, price, lastRefillTime, addedByUserID, imageUrl, lastOperationDate);
            else return null;
        }

        public static clsProduct FindByName(string name)
        {
            int productID = -1; string description = ""; int categoryID = -1;
            DateTime producingDate = DateTime.Now; DateTime expiryDate = DateTime.Now;
            int quantity = 0; int price = 0; DateTime lastRefillTime = DateTime.Now; int addedByUserID = -1;
            string imageUrl = ""; DateTime lastOperationDate = DateTime.Now;

            bool IsFound = clsProductData.GetProductInfoByName(name, ref productID, ref description
                , ref categoryID, ref producingDate, ref expiryDate, ref quantity, ref price
                , ref lastRefillTime, ref addedByUserID, ref imageUrl, ref lastOperationDate);

            if (IsFound) return new clsProduct(productID, name, description, categoryID, producingDate
                , expiryDate, quantity, price, lastRefillTime, addedByUserID, imageUrl, lastOperationDate);
            else return null;
        }

        private bool _AddNewProduct() 
        {
            this.ProductID = clsProductData.AddNewProduct(Name, Description, CategoryID, ProducingDate
                , ExpiryDate, Quantity, Price, LastRefillTime, AddedByUserID, ImageUrl, LastOperationDate);
            return ProductID != -1;
        }

        private bool _UpdateProduct()
        {
            return clsProductData.UpdateProduct(ProductID, Name, Description, CategoryID, ProducingDate
                , ExpiryDate, Quantity, Price, LastRefillTime, AddedByUserID, ImageUrl, LastOperationDate);
        }

        public bool Save() 
        {
            switch (Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewProduct()) 
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateProduct();
            }return false;
        }

        public static bool DeleteProduct(int ProductID) 
        {
            return clsProductData.DeleteProduct(ProductID);
        }

        public static DataTable GetAllProducts() 
        {
            return clsProductData.GetAllProducts();
        }

        public static bool IsProductOutOfStock(int productID)
        {
            return clsProductData.IsProductOutOfStock(productID);
        }

        public bool IsProductOutOfStock()
        {
            return clsProductData.IsProductOutOfStock(this.ProductID);
        }

    }
}
