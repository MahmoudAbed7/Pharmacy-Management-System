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
    public class clsUserJoiningApplicationData
    {
        public static bool GetApplicationByID(int ApplicationID, ref int PersonID, ref int Status
            , ref DateTime ApplicationDate) 
        {
            bool IsFound = false;
            try 
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString)) 
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetApplicationByID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        SqlParameter returnParameter = new SqlParameter("@ApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnParameter);
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.Read()) 
                            {
                                IsFound = true;
                                PersonID = (int)reader["PersonID"];
                                Status = (int)reader["Status"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                EventLog.WriteEntry("Pharmacy_App", "UserJoiningApplication Info returned by ID Successfully", EventLogEntryType.Information);
                            }
                            else 
                            {
                                IsFound = false;
                            }
                        }
                    }
                }
            } 
            catch (Exception e) 
            {
                IsFound = false;
                EventLog.WriteEntry("Pharmacy_App", e.Message, EventLogEntryType.Error);
            }
            return IsFound;
        }

        public static bool GetApplicationByPersonID(int PersonID, ref int ApplicationID, ref int Status
            , ref DateTime ApplicationDate)
        {
            bool IsFound = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_GetApplicationByPersonID", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        SqlParameter returnPara = new SqlParameter("@PersonID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                IsFound = true;
                                ApplicationID = (int)reader["ApplicationID"];
                                Status = (int)reader["Status"];
                                ApplicationDate = (DateTime)reader["ApplicationDate"];
                                EventLog.WriteEntry("Pharmacy_App", "UserJoiningApplication Info returned by personID Successfully", EventLogEntryType.Information);
                            }
                            else
                            {
                                IsFound = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                IsFound = false;
                EventLog.WriteEntry("Pharmacy_App", e.Message, EventLogEntryType.Error);
            }
            return IsFound;
        }

        public static int AddNewApplication(int PersonID, int Status, DateTime ApplicationDate) 
        {
            int ApplicationID = -1;
            try
            {
                using(SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_AddNewApplication", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlParameter outputIdPara = new SqlParameter("@ApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(outputIdPara);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        command.ExecuteScalar();
                        ApplicationID = (int)command.Parameters["@ApplicationID"].Value;
                        EventLog.WriteEntry("Pharmacy_App", "UserJoiningApplication added Successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception e) 
            {
                ApplicationID = -1;
                EventLog.WriteEntry("Pharamcy_App", e.Message, EventLogEntryType.Error);
            }
            return ApplicationID;
        }

        public static bool UpdateApplication(int ApplicationID, int PersonID
            , int Status, DateTime ApplicationDate)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_UpdateApplication", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                        command.Parameters.AddWithValue("@PersonID", PersonID);
                        command.Parameters.AddWithValue("@Status", Status);
                        command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
                        SqlParameter returnPara = new SqlParameter("@ApplicationID", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.ReturnValue
                        };
                        command.Parameters.Add(returnPara);
                        RowAffected = command.ExecuteNonQuery();
                        EventLog.WriteEntry("Pharmacy_App", "UserJoiningApplication Info updated Successfully", EventLogEntryType.Information);
                    }
                }
            }
            catch (Exception e) 
            {
                RowAffected = 0;
                EventLog.WriteEntry("Pharmacy_App", e.Message, EventLogEntryType.Error);
            }
            return RowAffected > 0;
        }

        public static DataTable GetAllApplications() 
        {
            DataTable dt = new DataTable();
            try 
            {
                using (SqlConnection connection = new SqlConnection(clsSettingsData.connectionString))
                {
                    connection.Open ();
                    using (SqlCommand command = new SqlCommand("sp_GetAllApplications", connection)) 
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = command.ExecuteReader()) 
                        {
                            if (reader.HasRows) dt.Load(reader);
                            EventLog.WriteEntry("Pharmacy_App", "UserJoiningApplication Info table returned Successfully", EventLogEntryType.Information);

                        }
                    }
                }
            } catch (Exception e) 
            {
                EventLog.WriteEntry("Pharmacy_App", e.Message, EventLogEntryType.Error);
            }
            return dt;
        }

    }
}
