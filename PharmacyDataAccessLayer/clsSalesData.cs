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
    public class clsSalesData
    {
        public static bool GetSaleInfoByID(int SaleID, ref int CartID, ref byte SaleStatus
            , ref int AddedByUserID, ref DateTime SaleDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetSaleInfoByID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SaleID", SaleID);
                        SqlParameter returnPara = new SqlParameter("@SaleID", System.Data.SqlDbType.Int)
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
                                SaleStatus = (byte)reader["SaleStatus"];
                                AddedByUserID = (int)reader["AddedByUserID"];
                                SaleDate = (DateTime)reader["SaleDate"];
                                EventLog.WriteEntry("Pharmacy_App", "Sale info returned with ID: " + SaleID + "successfully", EventLogEntryType.Information);
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

        public static bool GetSaleInfoByCartID(int CartID, ref int SaleID, ref byte SaleStatus
           , ref int AddedByUserID, ref DateTime SaleDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetSaleInfoByCartID", connection))
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
                                SaleID = (int)reader["SaleID"];
                                SaleStatus = (byte)reader["SaleStatus"];
                                AddedByUserID = (int)reader["AddedByUserID"];
                                SaleDate = (DateTime)reader["SaleDate"];
                                EventLog.WriteEntry("Pharmacy_App", "Sale info returned with Cart ID: " + CartID + "successfully", EventLogEntryType.Information);
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


        public static int AddNewSale(int CartID, byte SaleStatus
            , int AddedByUserID, DateTime SaleDate)
        {
            int SaleID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewSale", connection)) 
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartID", CartID);
                        command.Parameters.AddWithValue("@SaleStatus", SaleStatus);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        command.Parameters.AddWithValue("@SaleDate", SaleDate);
                        SqlParameter outputPara = new SqlParameter("@SaleID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.Output
                        };
                        command.Parameters.Add(outputPara);
                        command.ExecuteScalar();
                        SaleID = (int)command.Parameters["@SaleID"].Value;
                        EventLog.WriteEntry("Pharmacy_App", "New Sale added successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception ex) 
            {
                SaleID = -1;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return SaleID;
        }

        public static bool UpdateSale(int SaleID, int CartID, byte SaleStatus
            , int AddedByUserID, DateTime SaleDate)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateSale", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SaleID", SaleID);
                        command.Parameters.AddWithValue("@CartID", CartID);
                        command.Parameters.AddWithValue("@SaleStatus", SaleStatus);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        command.Parameters.AddWithValue("@SaleDate", SaleDate);

                        SqlParameter returnPara = new SqlParameter("@SaleID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "Update Sale with ID: " + SaleID + "done successfully", EventLogEntryType.Information);
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

        public static DataTable GetAllSales() 
        {
            DataTable dt = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetAllSalesInfo", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if(reader.HasRows) dt.Load(reader);
                            EventLog.WriteEntry($"Pharmacy_App", "Calling Sales info has done successfully", EventLogEntryType.Information);
                        }
                    }
                }
            } catch (Exception ex)
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return dt;

        }

        public static bool CheckBalanceSufficent(int CartID) 
        {
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_CheckBalance", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartID", CartID);
                        command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "customer has sufficent amount of balance", EventLogEntryType.Information);
                        return true;
                    }
                }
            } catch (Exception ex) 
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
                return false;
            }
        }

        public static bool Checkout(int CartID, int AddedByUserID)
        {
     
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using(SqlCommand command = new SqlCommand("sp_checkout", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CartID", CartID);
                        command.Parameters.AddWithValue("@AddedByUserID", AddedByUserID);
                        command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "Checkout process is done", EventLogEntryType.Information);
                        return true;
                    }
                }
            } catch (Exception ex) 
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
                return false;
            }

        }

    }
}
