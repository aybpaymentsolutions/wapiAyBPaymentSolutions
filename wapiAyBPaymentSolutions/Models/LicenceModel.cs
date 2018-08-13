using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wapiAyBPaymentSolutions.Models.JsonClasses;

namespace wapiAyBPaymentSolutions.Models
{
    public class LicenceModel
    {

        public LicenceResponse insertLicence(string deviceID, string commerceID)
        {

            LicenceResponse objLicence = new LicenceResponse();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connAyBDev"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT into Instalations VALUES (@instalationid, @storeid)";
                    command.Parameters.AddWithValue("@instalationid", deviceID);
                    command.Parameters.AddWithValue("@storeid", commerceID);

                    try
                    {
                        connection.Open();
                        int insertedRecord = command.ExecuteNonQuery();

                        if (insertedRecord > 0)
                        {

                            using (SqlCommand commandUser = new SqlCommand())
                            {

                                int result = 1;
                                SqlCommand cmdSelUser = new SqlCommand("SELECT COUNT(1) COUNTER FROM USERS WHERE InstalationID = @instalation; ", connection);
                                cmdSelUser.Parameters.AddWithValue("@instalation", deviceID);

                                using (SqlDataReader reader = cmdSelUser.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        result = reader.GetInt32(0);
                                    }
                                }


                                if (result == 0)
                                {

                                    commandUser.Connection = connection;
                                    commandUser.CommandType = CommandType.Text;
                                    commandUser.CommandText = "INSERT into Users (FIRSTNAME, LASTNAME, JOBTITLEID, ACCESSCODE, INSTALATIONID, EmployeeInActive, StoreID) VALUES (@fname, @lname, @jobtitle, @accesscode, @instalationid, 1, @store)";
                                    commandUser.Parameters.AddWithValue("@fname", "Admin");
                                    commandUser.Parameters.AddWithValue("@lname", commerceID);
                                    commandUser.Parameters.AddWithValue("@jobtitle", "1");
                                    commandUser.Parameters.AddWithValue("@accesscode", "8588");
                                    commandUser.Parameters.AddWithValue("@instalationid", deviceID);
                                    commandUser.Parameters.AddWithValue("@store", commerceID);

                                    try
                                    {
                                        int insertedUser = commandUser.ExecuteNonQuery();

                                        if (insertedUser > 0)
                                        {
                                            objLicence.responseCode = "000";
                                            objLicence.responseMessage = "Exito";
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        objLicence.responseCode = "001";
                                        objLicence.responseMessage = "Error: " + ex.Message;
                                    }


                                }

                            }
                        }
                    }
                    catch (SqlException sqlex)
                    {
                        objLicence.responseCode = "001";
                        objLicence.responseMessage = "Error: " + sqlex.Message;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return objLicence;
        }


    }
}