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
    public class clsCartItmsData
    {
        public static bool GetCartItemByID(int ItemID, ref int CartID, ref int ProductID, ref int Quantity) 
        {
            bool IsFound = false;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCartItemByID", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemID", ItemID);
                        SqlParameter returnPara = new SqlParameter("@ItemID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                CartID = (int)reader["CartID"];
                                ProductID = (int)reader["ProductID"];
                                Quantity = (int)reader["Quantity"];
                                EventLog.WriteEntry("Parmacy_App", "Cart Item Info returned by ID successfully");
                            }
                            else 
                            {
                                IsFound = false;
                            }
                        }
                    }
                }
            } catch(Exception ex) 
            {
                IsFound = false;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return IsFound;
        }

        public static bool GetCartItemByCartID(int CartID, ref int ItemID, ref int ProductID, ref int Quantity)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCartItemByCartID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartID", CartID);
                        SqlParameter returnPara = new SqlParameter("@CartID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ItemID = (int)reader["ItemID"];
                                ProductID = (int)reader["ProductID"];
                                Quantity = (int)reader["Quantity"];
                                EventLog.WriteEntry("Parmacy_App", "Cart Item Info returned by ID successfully");
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

        public static bool GetCartItemByProductID(int ProductID, ref int ItemID, ref int CartID, ref int Quantity)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCartItemByProductID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        SqlParameter returnPara = new SqlParameter("@ProductID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ItemID = (int)reader["ItemID"];
                                CartID = (int)reader["CartID"];
                                Quantity = (int)reader["Quantity"];
                                EventLog.WriteEntry("Parmacy_App", "Cart Item Info returned by ID successfully");
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


        public static int AddNewCartItem(int CartID, int ProductID, int Quantity)
        {
            int ItemID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewItem", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartID", CartID);
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);
                        SqlParameter outputIdPara = new SqlParameter("@ItemID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdPara);
                        command.ExecuteScalar();
                        ItemID = (int)command.Parameters["@ItemID"].Value;
                        EventLog.WriteEntry("Parmacy_App", "Adding item has done successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                ItemID = -1;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return ItemID;
        }


        public static bool UpdateItem(int ItemID, int CartID, int ProductID, int Quantity)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateItem", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemID", ItemID);
                        command.Parameters.AddWithValue("@CartID", CartID);
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        command.Parameters.AddWithValue("@Quantity", Quantity);

                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry("Parmacy_App", $"Updating Item with ID: {ItemID} has done successfully");
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

        public static bool DeleteItem(int ItemID)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DeleteItem", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ItemID", ItemID);
                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry("Pharmacy_App", $"Deleting item with ID: {ItemID} has done successfully", EventLogEntryType.Information);
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

        public static DataTable GetAllCartItemsForCartID(int CartID)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetAllCartItemsForCartID", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartID", CartID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                                EventLog.WriteEntry("Pharmacy_App", $"Return all items for this cart with id {CartID} has done successfully", EventLogEntryType.Information);
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

        public static bool DoesItemFindInTheCart(int CartID, int ProductID) 
        {
            bool IsFound;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DoesItemFindInTheCart", connection))
                    {
                        command.CommandType= CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartID", CartID);
                        command.Parameters.AddWithValue("@ProductID", ProductID);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            IsFound = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"This item with id: {ProductID} is already exists in your cart", EventLogEntryType.Information);
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
                IsFound = false;
            }
            return IsFound;
        }

    }
}
