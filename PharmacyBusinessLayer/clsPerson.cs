using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsPerson
    {
        enum enMode { AddNew, Update};
        enMode _Mode = enMode.AddNew;
        public int PersonID {  get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        { get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; } }
        public int NationailtyCountryID {  get; set; }
        public clsCountry CountryInfo;
        public short Gender {  get; set; }
        public DateTime DateOfBirth {  get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ImageUrl {  get; set; }

        public clsPerson() 
        {
            _Mode = enMode.AddNew;
            PersonID = -1; FirstName = ""; SecondName = ""; ThirdName = ""; LastName = "";
            Gender = 0; DateOfBirth = DateTime.Now; Address = ""; Phone = "";
            Email = ""; ImageUrl = ""; NationailtyCountryID = -1;
        }

        public clsPerson(int personID, string firstName, string secondName, string thirdName
            , string lastName, int nationailtyCountryID, short gender, DateTime dateOfBirth
            , string address, string phone, string email, string imageUrl)
        {
            _Mode = enMode.Update;
            PersonID = personID;
            FirstName = firstName;
            SecondName = secondName;
            ThirdName = thirdName;
            LastName = lastName;
            NationailtyCountryID = nationailtyCountryID;
            CountryInfo = clsCountry.Find(NationailtyCountryID);
            Gender = gender;
            DateOfBirth = dateOfBirth;
            Address = address;
            Phone = phone;
            Email = email;
            ImageUrl = imageUrl;
        }

        public static clsPerson Find(int PersonID) 
        {
            string firstName = ""; string secondName = ""; string thirdName = "";
            string lastName = ""; int nationailtyCountryID = -1; short gender = 0;
            DateTime dateOfBirth = DateTime.Now; string address = ""; string phone = "";
            string email = ""; string imageUrl = "";

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref firstName, ref secondName, ref thirdName
                , ref lastName, ref nationailtyCountryID, ref gender, ref dateOfBirth
                , ref phone, ref email, ref address, ref imageUrl);

            if(IsFound) return new clsPerson(PersonID, firstName, secondName, thirdName, lastName
                , nationailtyCountryID, gender, dateOfBirth, address, phone, email, imageUrl);

            else return null;
        }


        private bool _AddNewPerson() 
        {
             this.PersonID = clsPersonData.AddNewPerson(FirstName, SecondName, ThirdName, LastName
                , NationailtyCountryID, Gender, DateOfBirth, Phone, Email, Address, ImageUrl);

            return this.PersonID != -1;
        }

        private bool _UpdatePerson()
        {
            return clsPersonData.UpdatePerson(PersonID, FirstName, SecondName, ThirdName, LastName
                , NationailtyCountryID, Gender, DateOfBirth, Phone, Email, Address, ImageUrl);
        }

        public bool Save() 
        {
            switch(_Mode) 
            {
                case enMode.AddNew: 
                    {
                        if (_AddNewPerson())
                        {
                            _Mode = enMode.Update;
                            return true;
                        }
                        else 
                        {
                            return false;
                        }
                    }
                case enMode.Update: 
                    {
                        return _UpdatePerson();
                    }
            }return false;
        }

        public static bool DeletePerson(int PersonID) 
        {
            return clsPersonData.DeletePerson(PersonID);
        }

        public static bool IsPersonExsist(int PersonID) 
        {
            return clsPersonData.IsPersonExsist(PersonID);
        }

        public static DataTable GetAllPeople() 
        {
            return clsPersonData.GetAllPeople();
        }

    }
}
