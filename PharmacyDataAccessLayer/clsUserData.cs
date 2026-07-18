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
    public class clsUserData
    {
        public static bool GetUserInfoByID(int UserID, ref string UserName, ref string Password
            , ref int PersonID, ref int Permission, ref bool IsActive) 
        {
            bool IsFound = false;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("sp_GetUserInfoByID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        SqlParameter returnParameter = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);

                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                IsFound = true;
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                PersonID = (int)reader["PersonID"];
                                Permission = (int)reader["Permission"];
                                IsActive = (bool)reader["IsActive"];
                                EventLog.WriteEntry("Parmacy_App", "User Info returned by ID successfully");
                            }
                            else 
                            {
                                IsFound = false;
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                IsFound = false;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }

            return IsFound;
        }

        public static bool GetUserInfoByPersonID(int PersonID, ref string UserName, ref string Password
            , ref int UserID, ref int Permission, ref bool IsActive)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetUserInfoByPersonID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserName = (string)reader["UserName"];
                                Password = (string)reader["Password"];
                                UserID = (int)reader["UserID"];
                                Permission = (int)reader["Permission"];
                                IsActive = (bool)reader["IsActive"];
                                EventLog.WriteEntry("Parmacy_App", "User Info returned by PersonID successfully");
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

        public static bool GetUserInfoByUserNameAndPassword(string UserName , string Password
            , ref int UserID, ref int PersonID, ref int Permission, ref bool IsActive)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetUserInfoByUserNameAndPassword", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                UserID = (int)reader["UserID"];
                                PersonID = (int)reader["PersonID"];
                                Permission = (int)reader["Permission"];
                                IsActive = (bool)reader["IsActive"];
                                EventLog.WriteEntry("Parmacy_App", "User Info returned by userName and password successfully");
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

        public static int AddNewUser(string UserName, string Password
            , int PersonID, int Permission, bool IsActive)
        {
            int UserID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewUser", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Permission", Permission);
                        command.Parameters.AddWithValue("@IsActive", IsActive);
                        SqlParameter outputIdPara = new SqlParameter("@UserID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdPara);
                        command.ExecuteScalar();
                        UserID = (int)command.Parameters["@UserID"].Value;
                        EventLog.WriteEntry("Parmacy_App", "Adding user has done successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                UserID = -1;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return UserID;
        }


        public static bool UpdateUser(int UserID, string UserName, string Password
            , int PersonID, int Permission, bool IsActive)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateUser", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        command.Parameters.AddWithValue("@UserName", UserName);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Permission", Permission);
                        command.Parameters.AddWithValue("@IsActive", IsActive);

                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry("Parmacy_App", $"Updating user with ID: {UserID} has done successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                RowAffected = 0;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return RowAffected > 0;
        }

        public static bool DeleteUser(int UserID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DeleteUser", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry("Pharmacy_App", $"Deleting user with ID: {UserID} has done successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                RowAffected = 0;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return RowAffected > 0;
        }

        public static bool IsUserExistByID(int UserID) 
        {
            bool IsFound = false;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_IsUserExistByID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserID", UserID);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            IsFound = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"User found with ID: {UserID}", EventLogEntryType.Information);

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

        public static bool IsUserExistByUserName(string UserName)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_IsUserExistByUserName", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserName", UserName);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"User found with UserName: {UserName}", EventLogEntryType.Information);

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

        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_IsUserExistForPersonID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"User found with PersonID: {PersonID}", EventLogEntryType.Information);

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

        public static DataTable GetAllUsers() 
        {
            DataTable dt = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("sp_GetAllUsers", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using(SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.HasRows) 
                            {
                                dt.Load(reader);
                                EventLog.WriteEntry("Pharmacy_App", "Return all users into has done successfully", EventLogEntryType.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return dt;
        }
    }
}
