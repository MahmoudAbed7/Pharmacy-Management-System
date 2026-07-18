using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PharmacyBusinessLayer
{
    public class clsUser
    {
        enum enMode { AddNew, Update};
        enMode _Mode = enMode.AddNew;

        public enum enPermission 
        { All = -1, AddUpdatePeople = 1, ListPeople = 2, AddUpdateUsers = 4, ListUsers = 8
                , JoiningApplication = 16, AddUpdateProducts = 32, ListProducts = 64}
        public int UserID {  get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PersonID {  get; set; }
        public clsPerson PersonInfo;
        public int Permissions {  get; set; }
        public bool IsActive {  get; set; }

        public clsUser() 
        {
            _Mode = enMode.AddNew;
            UserID = -1; UserName = ""; Password = "";
            PersonID = -1; Permissions = 0; IsActive = false;
        }

        public clsUser(int userID, string userName, string password
            , int personID, enPermission permission, bool isActive)
        {
            _Mode = enMode.Update;
            UserID = userID; UserName = userName; Password = password;
            PersonID = personID; Permissions = (int)permission; IsActive = isActive;
            PersonInfo = clsPerson.Find(personID);
        }

        public static clsUser FindByID(int UserID) 
        {
            string userName = ""; string password = "";
            int personID = -1; int permission = 0; bool isActive = false;

            bool Found = clsUserData.GetUserInfoByID(UserID, ref userName, ref password, ref personID
                , ref permission, ref isActive);
            if (Found) return new clsUser(UserID, userName, password, personID
                , (enPermission)permission, isActive);
            else return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            string userName = ""; string password = "";
            int userID = -1; int permission = 0; bool isActive = false;

            bool Found = clsUserData.GetUserInfoByPersonID(PersonID, ref userName, ref password, ref userID
                , ref permission, ref isActive);
            if (Found) return new clsUser(userID, userName, password, PersonID
                , (enPermission)permission, isActive);
            else return null;
        }

        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int userID = -1; int permission = 0; bool isActive = false;
            int personID = -1;

            bool Found = clsUserData.GetUserInfoByUserNameAndPassword(UserName, Password, ref userID
                ,ref personID, ref permission, ref isActive);
            if (Found) return new clsUser(userID, UserName, Password, personID
                , (enPermission)permission, isActive);
            else return null;
        }

        public bool checkPermission(int permission)
        {
            if (this.Permissions == (int)enPermission.All) return true;
            if ((this.Permissions & permission) == permission) return true;
            return false;
        }

        private bool _AddNewUser() 
        {
            this.UserID = clsUserData.AddNewUser(UserName, Password, PersonID, Permissions, IsActive);
            return this.UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(UserID, UserName, Password, PersonID, Permissions, IsActive);
        }

        public bool Save() 
        {
            switch (_Mode) 
            {
                case enMode.AddNew:
                    if (_AddNewUser()) 
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }return false;
        }

        public static bool DeleteUser(int UserID) 
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID) 
        {
            return clsUserData.IsUserExistByID(UserID);
        }

        public static bool IsUserExistByUserName(string UserName)
        {
            return clsUserData.IsUserExistByUserName(UserName);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }

        public static DataTable GetAllUsers() 
        {
            return clsUserData.GetAllUsers();
        }
    }
}
