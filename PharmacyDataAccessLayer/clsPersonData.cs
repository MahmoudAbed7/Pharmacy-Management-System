using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDataAccessLayer
{
    public class clsPersonData
    {
        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName
            , ref string ThirdName, ref string LastName, ref int NationalityCountryID, ref short Gender
            , ref DateTime DateOfBirth, ref string Phone, ref string Email
            , ref string Address, ref string ImageUrl)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetPersonInfoByID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        SqlParameter returnPara = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);

                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                FirstName = (string)reader["FirstName"];

                                if (reader["SecondName"] != DBNull.Value)
                                    SecondName = (string)reader["SecondName"];
                                else
                                    SecondName = "";

                                if (reader["ThirdName"] != DBNull.Value)
                                    ThirdName = (string)reader["ThirdName"];
                                else
                                    ThirdName = "";

                                LastName = (string)reader["LastName"];

                                DateOfBirth = (DateTime)reader["DateOfBirth"];
                                Gender = (byte)reader["Gender"];

                                if (reader["Address"] != DBNull.Value)
                                    Address = (string)reader["Address"];
                                else
                                    Address = "";

                                Phone = (string)reader["Phone"];

                                if (reader["Email"] != DBNull.Value)
                                {
                                    Email = (string)reader["Email"];
                                }
                                else
                                {
                                    Email = "";
                                }

                                NationalityCountryID = (int)reader["NationalityCountryID"];

                                if (reader["ImageUrl"] != DBNull.Value)
                                {
                                    ImageUrl = (string)reader["ImageUrl"];
                                }
                                else
                                {
                                    ImageUrl = "";
                                }
                                EventLog.WriteEntry("Pharmacy_App", "Person info returned with ID: " + PersonID + "successfully", EventLogEntryType.Information);
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
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return IsFound;
        }

        public static int AddNewPerson(string FirstName, string SecondName
            , string ThirdName, string LastName, int NationalityCountryID, short Gender
            , DateTime DateOfBirth, string Phone, string Email
            , string Address, string ImageUrl)
        {
            int PersonID = -1;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("sp_AddNewPerson", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", FirstName);

                        if(SecondName != "")
                        command.Parameters.AddWithValue("@SecondName", SecondName);
                        else command.Parameters.AddWithValue("@SecondName", DBNull.Value);

                        if (ThirdName != "")
                            command.Parameters.AddWithValue("@ThirdName", ThirdName);
                        else command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Phone", Phone);

                        if (Email != "")
                            command.Parameters.AddWithValue("@Email", Email);
                        else
                            command.Parameters.AddWithValue("@Email", DBNull.Value);

                        if (Address != "")
                            command.Parameters.AddWithValue("@Address", Address);
                        else
                            command.Parameters.AddWithValue("@Address", DBNull.Value);

                        if (ImageUrl != "")
                            command.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                        else
                            command.Parameters.AddWithValue("@ImageUrl", DBNull.Value);

                        SqlParameter outputIdPara = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdPara);
                        command.ExecuteScalar();
                        PersonID = (int)command.Parameters["@PersonID"].Value;
                        EventLog.WriteEntry("Pharmacy_App", "New person added successfully", EventLogEntryType.Information);
                    }
                }
            }catch(Exception ex) { PersonID = -1; }
            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName
            , string ThirdName, string LastName, int NationalityCountryID, short Gender
            , DateTime DateOfBirth, string Phone, string Email
            , string Address, string ImageUrl)
        {
            int AffectedRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdatePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@FirstName", FirstName);

                        if(SecondName != null)
                        command.Parameters.AddWithValue("@SecondName", SecondName);
                        else command.Parameters.AddWithValue("@SecondName", DBNull.Value);

                        if (ThirdName != null)
                            command.Parameters.AddWithValue("@ThirdName", ThirdName);
                        else command.Parameters.AddWithValue("@ThirdName", DBNull.Value);

                        command.Parameters.AddWithValue("@LastName", LastName);
                        command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
                        command.Parameters.AddWithValue("@Gender", Gender);
                        command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
                        command.Parameters.AddWithValue("@Phone", Phone);

                        if (Email != null)
                            command.Parameters.AddWithValue("@Email", Email);
                        else
                            command.Parameters.AddWithValue("@Email", DBNull.Value);

                        if (Address != null)
                            command.Parameters.AddWithValue("@Address", Address);
                        else
                            command.Parameters.AddWithValue("@Address", DBNull.Value);

                        if (ImageUrl != null)
                            command.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                        else
                            command.Parameters.AddWithValue("@ImageUrl", DBNull.Value);

                        SqlParameter returnPara = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        AffectedRows = command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "Update Person with ID: " + PersonID + "done successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                AffectedRows = 0;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return AffectedRows > 0 ;
        }

        public static bool DeletePerson(int PersonID)
        {
            int AffectedRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DeletePerson", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        SqlParameter returnPara = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        AffectedRows = command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "Delete Person with ID: " + PersonID + "done successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception ex) 
            {
                AffectedRows = 0;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return AffectedRows > 0;
        }

        public static DataTable GetAllPeople() 
        {
            DataTable dtPeople = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetAllPeople", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if(reader.HasRows) dtPeople.Load(reader);
                            EventLog.WriteEntry($"Pharmacy_App", "Calling People info have done successfully", EventLogEntryType.Information);
                        }
                    }
                }
            } catch (Exception ex) 
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return dtPeople;
        }

        public static bool IsPersonExsist(int PersonID) 
        {
            bool IsFound = false;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_IsPersonExsist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        SqlParameter returnPara = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        {
                            IsFound = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"This Person with id: {PersonID} exsists",EventLogEntryType.Information);
                        }
                    }
                }
            } catch (Exception ex) 
            {
                IsFound = false;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return IsFound;
        }
    }
}
