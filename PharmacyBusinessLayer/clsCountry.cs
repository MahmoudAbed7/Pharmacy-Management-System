using PharmacyDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyBusinessLayer
{
    public class clsCountry
    {
        public int CountryID {  get; set; }
        public string CountryName { get; set; }


        public clsCountry() 
        {
            CountryID = -1; CountryName = string.Empty;
        }

        public clsCountry(int countryID, string countryName)
        {
            CountryID = countryID;
            CountryName = countryName;
        }

        public static clsCountry Find(int CountryID) 
        {
            string countryName = string.Empty;

            bool IsFound = clsCountryData.GetCountryInfoByID(CountryID, ref countryName);

            if (IsFound) return new clsCountry(CountryID, countryName);
            else return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;

            bool IsFound = clsCountryData.GetCountryInfoByName(CountryName, ref CountryID);

            if (IsFound) return new clsCountry(CountryID, CountryName);
            else return null;
        }

        public static DataTable GetAllCountries() 
        {
            return clsCountryData.GetAllCountries();
        }
    }
}
