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
    public class clsCustomersData
    {
        public static bool GetCustomerInfoByID(int CustomerID, ref int PersonID
            , ref int Balance)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCustomerInfoByID", connection))
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
                                PersonID = (int)reader["PersonID"];
                                Balance = (int)reader["Balance"];
                                EventLog.WriteEntry("Pharmacy_App", "Customer info returned with ID: " + CustomerID + "successfully", EventLogEntryType.Information);
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

        public static bool GetCustomerInfoByPersonID(int PersonID, ref int CustomerID
            , ref int Balance)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetCustomerInfoByPersonID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        SqlParameter returnPara = new SqlParameter("@PersonID", System.Data.SqlDbType.Int)
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
                                Balance = (int)reader["Balance"];
                                EventLog.WriteEntry("Pharmacy_App", "Customer info returned with Person ID: " + PersonID + "successfully", EventLogEntryType.Information);
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

        public static int AddNewCustomer(int PersonID, int Balance)
        {
            int CustomerID = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewCustomer", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Balance", Balance);


                        SqlParameter outputParaID = new SqlParameter("@CustomerID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputParaID);
                        command.ExecuteScalar();
                        CustomerID = (int)command.Parameters["@CustomerID"].Value;
                        EventLog.WriteEntry("Pharmacy_App", "New Customer added successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                CustomerID = -1;
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }
            return CustomerID;


        }

        public static bool UpdateCustomer(int CustomerID, int PersonID
           , int Balance)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateCustomer", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Balance", Balance);

                        SqlParameter returnPara = new SqlParameter("@CustomerID", System.Data.SqlDbType.Int)
                        {
                            Direction = System.Data.ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        RowAffected =  command.ExecuteNonQuery();
                        EventLog.WriteEntry($"Pharmacy_App", "Update Customer with ID: " + CustomerID + "done successfully", EventLogEntryType.Information);
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

        public static DataTable GetAllCustomers() 
        {
            DataTable dt = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetAllCustomers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.HasRows) dt.Load(reader);
                            EventLog.WriteEntry($"Pharmacy_App", "Calling Customer info has done successfully", EventLogEntryType.Information);
                        }
                    }
                }
            } catch (Exception ex)
            {
                EventLog.WriteEntry("Pharmacy_App", ex.Message, EventLogEntryType.Error);
            }return dt;
        }

        public static bool IsCustomerExist(int CustomerID) 
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_IsCustomerExist", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        SqlParameter returnPara = new SqlParameter("@CustomerID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"This Person with id: {CustomerID} exists", EventLogEntryType.Information);
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

        public static bool DoesCustomerHaveCart(int CustomerID)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_DoesCustomerHaveCart", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@CustomerID", CustomerID);
                        SqlParameter returnPara = new SqlParameter("@CustomerID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            IsFound = reader.HasRows;
                            EventLog.WriteEntry("Pharmacy_App", $"This Customer with id: {CustomerID} has cart", EventLogEntryType.Information);
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
    }
}
