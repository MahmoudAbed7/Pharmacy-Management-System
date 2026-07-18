using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsCategory
    {
        enum enMode { AddNew, Update};
        enMode _Mode = enMode.AddNew;

        public int CategoryID {  get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdateDate {  get; set; }
        public int AddedByUserID {  get; set; }

        public clsCategory() 
        {
            _Mode = enMode.AddNew;
            CategoryID = -1; CategoryName = ""; Description = "";
            LastUpdateDate = DateTime.Now; AddedByUserID = -1;
        }

        public clsCategory(int categoryID, string categoryName, string description
            , DateTime lastUpdateDate, int addedByUserID)
        {
            _Mode = enMode.Update;
            CategoryID = categoryID; CategoryName = categoryName;
            Description = description;LastUpdateDate = lastUpdateDate;
            AddedByUserID = addedByUserID;
        }

        public static clsCategory Find(int CategoryID) 
        {
            string categoryName = ""; string description = "";
            DateTime lastUpdateDate = DateTime.Now; int addedByUserID = -1;

            bool IsFound = clsCategoryData.GetCategoryInfoByID(CategoryID, ref categoryName
                , ref description, ref lastUpdateDate, ref addedByUserID);

            if(IsFound) return new clsCategory(CategoryID, categoryName, description
                , lastUpdateDate, addedByUserID);
            else return null;
        }

        public static clsCategory Find(string CategoryName)
        {
            int CategoryID = -1; string description = "";
            DateTime lastUpdateDate = DateTime.Now; int addedByUserID = -1;

            bool IsFound = clsCategoryData.GetCategoryInfoByName(CategoryName, ref CategoryID
                , ref description, ref lastUpdateDate, ref addedByUserID);

            if (IsFound) return new clsCategory(CategoryID, CategoryName, description
                , lastUpdateDate, addedByUserID);
            else return null;
        }

        private bool _AddNewCategory() 
        {
            this.CategoryID = clsCategoryData.AddNewCategory(CategoryName, Description
                , LastUpdateDate, AddedByUserID);
            return this.CategoryID != -1;
        }

        private bool _UpdateCategory() 
        {
            return clsCategoryData.UpdateCategory(CategoryID, CategoryName, Description
                , LastUpdateDate, AddedByUserID);
        }

        public static bool DeleteCategory(int CategoryID) 
        {
            return clsCategoryData.DeleteCategory(CategoryID);
        }

        public bool Save() 
        {
            switch (_Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewCategory()) 
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateCategory();
            }return false;
        }

        public static DataTable GetAllCategories() 
        {
            return clsCategoryData.GetAllCategories();
        }
    }
}
