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
    public class clsProductData
    {
        public static bool GetProductInfoByID(int ProductID, ref string Name
            , ref string Description, ref int CategoryID, ref DateTime ProducingDate
            , ref DateTime ExpiryDate, ref int Quantity, ref int Price
            , ref DateTime LastRefillDate, ref int AddedByUserID, ref string ImageUrl
            , ref DateTime LastOperationDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetProductInfoByID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                Name = (string)reader["Name"];
                                Description = (string)reader["Description"];
                                CategoryID = (int)reader["CategoryID"];
                                ProducingDate = (DateTime)reader["ProducingDate"];
                                ExpiryDate = (DateTime)reader["ExpiryDate"];
                                Quantity = (int)reader["Quantity"];
                                Price = (int)reader["Price"];
                                LastRefillDate = (DateTime)reader["LastRefillDate"];
                                AddedByUserID = (int)reader["AddedByUserID"];

                                if (reader["ImageUrl"] != DBNull.Value)
                                    ImageUrl = (string)reader["ImageUrl"];
                                else
                                    ImageUrl = "";

                                LastOperationDate = (DateTime)reader["LastOperationDate"];
                                EventLog.WriteEntry("Pharmacy_App", "Product Info returned by ID successfully", EventLogEntryType.Information);

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

        public static bool GetProductInfoByName(string Name, ref int ProductID
           , ref string Description, ref int CategoryID, ref DateTime ProducingDate
           , ref DateTime ExpiryDate, ref int Quantity, ref int Price
           , ref DateTime LastRefillDate, ref int AddedByUserID, ref string ImageUrl
           , ref DateTime LastOperationDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetProductInfoByName", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", Name);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ProductID = (int)reader["ProductID"];
                                Description = (string)reader["Description"];
                                CategoryID = (int)reader["CategoryID"];
                                ProducingDate = (DateTime)reader["ProducingDate"];
                                ExpiryDate = (DateTime)reader["ExpiryDate"];
                                Quantity = (int)reader["Quantity"];
                                Price = (int)reader["Price"];
                                LastRefillDate = (DateTime)reader["LastRefillDate"];
                                AddedByUserID = (int)reader["AddedByUserID"];

                                if (reader["ImageUrl"] != DBNull.Value)
                                    ImageUrl = (string)reader["ImageUrl"];
                                else
                                    ImageUrl = "";

                                LastOperationDate = (DateTime)reader["LastOperationDate"];
                                EventLog.WriteEntry("Pharmacy_App", "Product Info returned by ID successfully", EventLogEntryType.Information);

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

        public static int AddNewProduct(string Name, string Description
            , int CategoryID, DateTime ProducingDate, DateTime ExpiryDate, int Quantity,
            int Price, DateTime LastRefillDate, int AddedByUserID, string ImageUrl
            , DateTime LastOperationDate)
        {
            int ProductID = -1;
            try
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewProduct", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Description", Description);
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
                        command.Parameters.AddWithValue("@ProducingDate", ProducingDate);
                        command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@Price", Price);
                        command.Parameters.AddWithValue("@LastRefillDate", LastRefillDate);
                        command.Parameters.AddWithValue("@LastOperationDate", LastOperationDate);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);

                        if (ImageUrl != "")
                            command.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                        else
                            command.Parameters.AddWithValue("@ImageUrl", DBNull.Value);

                        SqlParameter outputIdPara = new SqlParameter("@ProductID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdPara);
                        command.ExecuteScalar();
                        ProductID = (int)command.Parameters["@ProductID"].Value;
                        EventLog.WriteEntry("Pharmacy_App", "New Product added successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception ex) 
            {
                ProductID = -1;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return ProductID;
        }

        public static bool UpdateProduct(int ProductID, string Name, string Description
            , int CategoryID, DateTime ProducingDate, DateTime ExpiryDate, int Quantity,
            int Price, DateTime LastRefillDate, int AddedByUserID, string ImageUrl
            , DateTime LastOperationDate)
        {
            int AffectedRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateProduct", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Description", Description);
                        command.Parameters.AddWithValue("@CategoryID", CategoryID);
                        command.Parameters.AddWithValue("@ProducingDate", ProducingDate);
                        command.Parameters.AddWithValue("@ExpiryDate", ExpiryDate);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        command.Parameters.AddWithValue("@Price", Price);
                        command.Parameters.AddWithValue("@LastRefillDate", LastRefillDate);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);

                        if(ImageUrl != "")
                        command.Parameters.AddWithValue("@ImageUrl", ImageUrl);
                        else
                            command.Parameters.AddWithValue("@ImageUrl", DBNull.Value);

                        command.Parameters.AddWithValue("@LastOperationDate", LastOperationDate);
                        SqlParameter returnPara = new SqlParameter("@ProductID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        AffectedRows = command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "Update Product with ID: " + ProductID + "successfully", EventLogEntryType.Information);
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

        public static bool DeleteProduct(int ProductID)
        {
            int AffectedRows = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateProduct", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter returnPara = new SqlParameter("@ProductID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        AffectedRows = command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "Delete Product with ID: " + ProductID + "successfully", EventLogEntryType.Information);
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

        public static DataTable GetAllProducts() 
        {
            DataTable dt = new DataTable();
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("sp_GetAllProducts", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.HasRows) 
                            {
                                dt.Load(reader);
                                EventLog.WriteEntry($"Pharmacy_App", "Calling Product info have done successfully", EventLogEntryType.Information);
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

        public static bool IsProductOutOfStock(int ProductID) 
        {
            bool OutOfStock = false;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_IsProductOutOfStock", connection)) 
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        SqlParameter returnPara = new SqlParameter("@ProductID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            OutOfStock = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"This Product with id: {ProductID} is out of stock", EventLogEntryType.Information);
                        }
                    }
                }
            } catch (Exception ex)
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return OutOfStock;
        }
    }
}
