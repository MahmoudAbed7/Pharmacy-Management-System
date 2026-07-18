using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyDataAccessLayer
{
    public class clsCartsData
    {
        public static bool GetCartByID(int CartID, ref int CustomerID) 
        {
            bool IsFound = false;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCartByID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartId", CartID);
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
                                CustomerID = (int)reader["CustomerID"];
                                EventLog.WriteEntry("Parmacy_App", "Cart Info returned by ID successfully");
                            }
                            else 
                            {
                                IsFound = false;
                            }
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

        public static bool GetCartByCustomerID(int CustomerID, ref int CartID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCartByCustomerID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        SqlParameter returnPara = new SqlParameter("@CustomerID", System.Data.SqlDbType.Int)
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
                                EventLog.WriteEntry("Parmacy_App", "Cart Info returned by Customer ID successfully");
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

        public static int AddNewCart(int CustomerID) 
        {
            int CartID = -1;
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewCart", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        SqlParameter outputPara = new SqlParameter("@CartID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(outputPara);
                        command.ExecuteScalar();
                        CartID = (int)command.Parameters["@CartID"].Value;
                        EventLog.WriteEntry("Parmacy_App", "Adding Cart has done successfully");
                    }
                }
            } catch (Exception ex) 
            {
                CartID = -1;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return CartID;
        }
    }
}
