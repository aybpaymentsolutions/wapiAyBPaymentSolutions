using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using wapiAyBPaymentSolutions.Models.JsonClasses;

namespace wapiAyBPaymentSolutions.Models
{
    public class LoginModel
    {

        public LoginResponse doLogin(string deviceID, string pinCode)
        {
            LoginResponse loginResponse = new LoginResponse();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connAyBDev"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    SqlCommand cmdSelUser = new SqlCommand("SELECT usr.UsersID, usr.FirstName, usr.LastName, usr.AccessCode, usr.InstalationID, usr.JobTitleID, usr.EmployeeInActive, usr.STOREID, " +
                        "(SELECT STORENAME FROM STORES WHERE STOREID = usr.STOREID) StoreName FROM USERS usr WHERE InstalationID = '" + deviceID+"' AND AccessCode = '"+pinCode+"'", connection);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = cmdSelUser.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                loginResponse.ResponseCode = "000";
                                loginResponse.ResponseMessage = "Login Successfull";

                                var userResponse = new InfoUser();
                                userResponse.userID = Int32.Parse(reader[0].ToString());
                                userResponse.Fname = reader[1].ToString() + " " + reader[2].ToString();
                                userResponse.pinCode = reader[3].ToString();
                                userResponse.deviceID = reader[4].ToString();
                                userResponse.Rol = reader[5].ToString();
                                userResponse.Status = reader[6].ToString();
                                userResponse.StoreID = reader[7].ToString();
                                userResponse.StoreName = reader[8].ToString();

                                loginResponse.InfoUser = userResponse;
                            }
                            else
                            {
                                loginResponse.ResponseCode = "002";
                                loginResponse.ResponseMessage = "Pin or licence doesn't exist";
                            }

                        }
                    }
                    catch (SqlException sqlex)
                    {
                        loginResponse.ResponseCode = "001";
                        loginResponse.ResponseMessage = "Error: " + sqlex.Message;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return loginResponse;
        }

    }
}