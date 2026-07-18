using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDataAccessLayer
{
    public class clsCountryData
    {
        public static bool GetCountryInfoByID(int CountryID, ref string CountryName) 
        {
            bool IsFound = false;
            try
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCountryInfoByID", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CountryID", CountryID);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                IsFound = true;
                                CountryName = (string)reader["CountryName"];
                            }
                            else 
                            {
                                IsFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                IsFound = false;
            }
            return IsFound;
        }
        public static bool GetCountryInfoByName(string ConutryName, ref int CountryID) 
        {
            bool IsFound = false;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("sp_GetCountryInfoByName", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CountryName", ConutryName);
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                IsFound = true;
                                CountryID = (int)reader["CountryID"];
                            }
                            else 
                            {
                                IsFound = false;
                            }
                        }

                    }
                }
            } catch (Exception ex) { IsFound = false; }
            return IsFound;
        }
        public static DataTable GetAllCountries() 
        {
            DataTable dtCountries = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using(SqlCommand command =new SqlCommand("sp_GetAllCountries", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.HasRows) dtCountries.Load(reader);
                        }
                    }
                }
            }catch(Exception ex) { }
            return dtCountries;
        }
    }
}
