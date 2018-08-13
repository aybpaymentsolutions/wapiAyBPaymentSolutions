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
    public class UsersModel
    {
        public UsersResponse getUsers(string deviceID)
        {
            UsersResponse usersResponse = new UsersResponse();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connAyBDev"].ConnectionString))
            {
                SqlCommand cmdSelUsers = new SqlCommand("SELECT UsersID, FirstName, LastName, AccessCode, InstalationID, JobTitleID, EmployeeInActive FROM USERS WHERE InstalationID = '" + deviceID + "'", connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmdSelUsers.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            usersResponse.ResponseCode = "000";
                            usersResponse.ResponseMessage = "Proccess complete";
                            List<InfoUser> listUsers = new List<InfoUser>();

                            while (reader.Read())
                            {
                                var user = new InfoUser();
                                user.userID = Int32.Parse(reader["UsersID"].ToString());
                                user.Fname = reader["FirstName"].ToString() + " " + reader["LastName"].ToString();
                                user.pinCode = reader["AccessCode"].ToString();
                                user.deviceID = reader["InstalationID"].ToString();
                                user.Rol = reader["JobTitleID"].ToString();
                                user.Status = reader["EmployeeInActive"].ToString() == "True" ? "Active" : "Inactive";

                                listUsers.Add(user);
                            }

                            usersResponse.InfoUser = listUsers;

                        }
                        else
                        {
                            usersResponse.ResponseCode = "002";
                            usersResponse.ResponseMessage = "Not exist users for this licence";
                        }

                    }
                }
                catch (SqlException sqlex)
                {
                    usersResponse.ResponseCode = "001";
                    usersResponse.ResponseMessage = "Error: " + sqlex.Message;
                }
                catch (Exception ex)
                {
                    usersResponse.ResponseCode = "001";
                    usersResponse.ResponseMessage = "Error: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }

            return usersResponse;
        }



        public UserActionResponse saveUser(UserInfoResponse userInfoData)
        {
            UserActionResponse actionResponse = new UserActionResponse();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connAyBDev"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {

                    command.Connection = connection;
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    if (userInfoData.UserData.PersonalInfo.UserId == 0)
                    {
                        var storeID = "";
                        
                        SqlCommand cmdSelStore = new SqlCommand("SELECT Store FROM Instalations WHERE InstalationID = @instalation; ", connection);
                        cmdSelStore.Parameters.AddWithValue("@instalation", userInfoData.UserData.PersonalInfo.DeviceId);

                        using (SqlDataReader reader = cmdSelStore.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                storeID = reader[0].ToString();
                            }
                        }

                        command.CommandText = "INSERT into Users (FirstName, LastName, " +
                            "AccessCode, InstalationID, StoreID, EmployeeInActive, PayBasis, PayRate, " +
                            "EmailAddress, PhoneNumer, MailingAddress1, MailingAddress2, MailingCity, " +
                            "MailingState, MailingCountry, MailingZipCode, JobTitleID) " +
                        "VALUES (@fname, @lname, @pincode, @instalationid, @storeid, @status, @hourate, @payrate, @email," +
                        "@phone, @addres, @addresscomple, @city, @state, @country, @zipcode, @jobtitle); SELECT SCOPE_IDENTITY()";

                        command.Parameters.AddWithValue("@instalationid", userInfoData.UserData.PersonalInfo.DeviceId);
                        command.Parameters.AddWithValue("@storeid", storeID);
                        command.Parameters.AddWithValue("@jobtitle", 1);

                    } else
                    {
                        command.CommandText = "UPDATE Users set " +
                            "FirstName = @fname, " +
                            "LastName = @lname, " +
                            "AccessCode = @pincode, " +
                            "EmployeeInActive = @status," +
                            "PayBasis = @hourate," +
                            "PayRate = @payrate," +
                            "EmailAddress = @email," +
                            "PhoneNumer = @phone," +
                            "MailingAddress1 = @addres," +
                            "MailingAddress2 = @addresscomple," +
                            "MailingCity = @city," +
                            "MailingState = @state," +
                            "MailingCountry = @country," +
                            "MailingZipCode = @zipcode " +
                            "WHERE UsersID = @userid";

                        command.Parameters.AddWithValue("@userid", userInfoData.UserData.PersonalInfo.UserId);
                    }
                    command.Parameters.AddWithValue("@fname", userInfoData.UserData.PersonalInfo.Fname);
                    command.Parameters.AddWithValue("@lname", userInfoData.UserData.PersonalInfo.Lname);
                    command.Parameters.AddWithValue("@pincode", userInfoData.UserData.PersonalInfo.PinCode);
                    command.Parameters.AddWithValue("@status", userInfoData.UserData.PersonalInfo.Status);
                    command.Parameters.AddWithValue("@hourate", userInfoData.UserData.SalaryInfo.HourlyRate);
                    command.Parameters.AddWithValue("@payrate", userInfoData.UserData.SalaryInfo.PayRate);
                    command.Parameters.AddWithValue("@email", userInfoData.UserData.ContactInfo.Email);
                    command.Parameters.AddWithValue("@phone", userInfoData.UserData.ContactInfo.Phone);
                    command.Parameters.AddWithValue("@addres", userInfoData.UserData.ContactInfo.Street);
                    command.Parameters.AddWithValue("@addresscomple", userInfoData.UserData.ContactInfo.Complementary);
                    command.Parameters.AddWithValue("@city", userInfoData.UserData.ContactInfo.City);
                    command.Parameters.AddWithValue("@state", userInfoData.UserData.ContactInfo.State);
                    command.Parameters.AddWithValue("@country", userInfoData.UserData.ContactInfo.Country);
                    command.Parameters.AddWithValue("@zipcode", userInfoData.UserData.ContactInfo.Zip);

                    try
                    {

                        if (userInfoData.UserData.PersonalInfo.UserId == 0)
                        {
                            int insertedRecord = Convert.ToInt32(command.ExecuteScalar());
                            actionResponse.IdUser = insertedRecord;
                        } else
                        {
                            int updatedRecord = command.ExecuteNonQuery();
                        }

                        actionResponse.ResponseCode = "000";
                        actionResponse.ResponseMessage = "User saved";
                            
                    } catch (SqlException sqlex)
                    {
                        actionResponse.ResponseCode = "001";
                        actionResponse.ResponseMessage = "Error: " + sqlex.Message;
                    }
                }
            }

            return actionResponse;

        }


        public UserInfoResponse getInfo(int idUser)
        {
            UserInfoResponse userInfo = new UserInfoResponse();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connAyBDev"].ConnectionString))
            {
                SqlCommand cmdSelUser = new SqlCommand("SELECT FirstName, LastName, " +
                            "AccessCode, InstalationID, StoreID, EmployeeInActive, PayBasis, PayRate, " +
                            "EmailAddress, PhoneNumer, MailingAddress1, MailingAddress2, MailingCity, " +
                            "MailingState, MailingCountry, MailingZipCode FROM USERS WHERE UsersID = '" + idUser + "'", connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmdSelUser.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            userInfo.ResponseCode = "000";
                            userInfo.ResponseMessage = "Proccess complete";

                            while (reader.Read())
                            {
                                var user = new UserData();
                                var personalinfo = new PersonalInfo();
                                var salaryInfo = new SalaryInfo();
                                var contactInfo = new ContactInfo();

                                personalinfo.DeviceId = reader["InstalationID"].ToString();
                                personalinfo.Fname = reader["FirstName"].ToString();
                                personalinfo.Lname = reader["LastName"].ToString();
                                personalinfo.PinCode = reader["AccessCode"].ToString();
                                personalinfo.UserId = idUser;
                                personalinfo.Status = reader["EmployeeInActive"].ToString();

                                salaryInfo.HourlyRate = reader["PayBasis"].ToString() == "" ? 0 : Int32.Parse(reader["PayBasis"].ToString());
                                salaryInfo.PayRate = reader["PayRate"].ToString() == "" ? 0 : Decimal.Parse(reader["PayRate"].ToString());

                                contactInfo.Email = reader["EmailAddress"].ToString();
                                contactInfo.City = reader["MailingCity"].ToString();
                                contactInfo.Phone = reader["PhoneNumer"].ToString();
                                contactInfo.Street = reader["MailingAddress1"].ToString();
                                contactInfo.State = reader["MailingState"].ToString();
                                contactInfo.Complementary = reader["MailingAddress2"].ToString();
                                contactInfo.Country = reader["MailingCountry"].ToString();
                                contactInfo.Zip = reader["MailingZipCode"].ToString() == "" ? 0 : Int32.Parse(reader["MailingZipCode"].ToString());

                                user.PersonalInfo = personalinfo;
                                user.SalaryInfo = salaryInfo;
                                user.ContactInfo = contactInfo;

                                userInfo.UserData = user;

                            }
                        } else
                        {
                            userInfo.ResponseCode = "002";
                            userInfo.ResponseMessage = "User doesn't exist";
                        }
                    }
                } catch (Exception ex)
                {
                    userInfo.ResponseCode = "001";
                    userInfo.ResponseMessage = "Error: " + ex.Message;
                }
            }
            return userInfo;
        }


        public UserActionResponse inactivateUser(int idUser)
        {
            UserActionResponse actionResponse = new UserActionResponse();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connAyBDev"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE Users set EmployeeInActive = 0 WHERE UsersID = " + idUser;

                    try
                    {

                        connection.Open();
                        int updatedRecord = command.ExecuteNonQuery();

                        actionResponse.ResponseCode = "000";
                        actionResponse.ResponseMessage = "Inactivate successfull";

                    }
                    catch (SqlException sqlex)
                    {
                        actionResponse.ResponseCode = "001";
                        actionResponse.ResponseMessage = "Error: " + sqlex.Message;
                    }
                }
            }
            return actionResponse;
        }



    }
}