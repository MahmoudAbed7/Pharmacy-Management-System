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
    public class clsCategoryData
    {
        public static bool GetCategoryInfoByID(int CategoryID, ref string CategoryName
            , ref string Description, ref DateTime LastUpdateDate, ref int AddedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCategoryInfoByID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
                        SqlParameter returnPara = new SqlParameter("@CategoryID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                CategoryName = (string)reader["CategoryName"];
                                Description = (string)reader["Description"];
                                LastUpdateDate = (DateTime)reader["LastUpdateDate"];
                                AddedByUserID = (int)reader["AddedByUserID"];
                                EventLog.WriteEntry("Parmacy_App", "Category Info returned by ID successfully");
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

        public static bool GetCategoryInfoByName(string CategoryName, ref int CategoryID
           , ref string Description, ref DateTime LastUpdateDate, ref int AddedByUserID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCategoryInfoByName", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryName", CategoryName);
                        SqlParameter returnPara = new SqlParameter("@CategoryName", System.Data.SqlDbType.NVarChar)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                CategoryID = (int)reader["CategoryID"];
                                Description = (string)reader["Description"];
                                LastUpdateDate = (DateTime)reader["LastUpdateDate"];
                                AddedByUserID = (int)reader["AddedByUserID"];
                                EventLog.WriteEntry("Parmacy_App", "Category Info returned by ID successfully");
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

        public static int AddNewCategory(string CategoryName, string Description
           , DateTime LastUpdateDate, int AddedByUserID)
        {
            int CategoryID = -1;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewCategory", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryName", CategoryName);
                        command.Parameters.AddWithValue("@Description", Description);
                        command.Parameters.AddWithValue("@LastUpdateDate", LastUpdateDate);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        SqlParameter outputIdPara = new SqlParameter("@CategoryID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdPara);
                        command.ExecuteScalar();
                        CategoryID = (int)command.Parameters["@CategoryID"].Value;
                        EventLog.WriteEntry("Pharmacy_App", "Adding Category has done successfully", EventLogEntryType.Information);
                    }
                }
            } 
            catch(Exception ex)
            {
                CategoryID = -1;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return CategoryID;
        }

        public static bool UpdateCategory(int CategoryID, string CategoryName, string Description
           , DateTime LastUpdateDate, int AddedByUserID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateCategory", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
                        command.Parameters.AddWithValue("@CategoryName", CategoryName);
                        command.Parameters.AddWithValue("@LastUpdateDate", LastUpdateDate);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry("Pharmacy_App", $"Updating category with ID: {CategoryID} has done successfully", EventLogEntryType.Information);
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

        public static bool DeleteCategory(int CategoryID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DeleteCategory", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry("Pharmacy_App", $"Deleting category with ID: {CategoryID} has done successfully", EventLogEntryType.Information);
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

        public static DataTable GetAllCategories() 
        {
            DataTable dt = new DataTable();
            try
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetAllCategories", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.HasRows) 
                            {
                                dt.Load(reader);
                                EventLog.WriteEntry("Pharmacy_App", "Return all categories into has done successfully", EventLogEntryType.Information);
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
