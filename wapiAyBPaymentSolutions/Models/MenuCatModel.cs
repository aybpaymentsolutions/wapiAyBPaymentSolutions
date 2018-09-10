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
    public class MenuCatModel
    {
        public CategoriesResponse getCategories(string StoreID)
        {
            CategoriesResponse categoriesResponse = new CategoriesResponse();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connAyBDev"].ConnectionString))
            {
                SqlCommand cmdSqlCategories = new SqlCommand("SELECT MenuCategoryID, MenuCategoryText, (CASE WHEN MenuCategoryInActive = 1 THEN 'Active' ELSE 'Inactive' END) AS MenuCategoryInActive, StoreID FROM MenuCategories WHERE StoreID = '" + StoreID + "'", connection);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = cmdSqlCategories.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            categoriesResponse.ResponseCode = "000";
                            categoriesResponse.ResponseMessage = "Proccess complete";
                            List<InfoCategory> listCategories = new List<InfoCategory>();

                            while (reader.Read())
                            {
                                var category = new InfoCategory();
                                category.MenuCategoryID = Int32.Parse(reader["MenuCategoryID"].ToString());
                                category.MenuCategoryText = reader["MenuCategoryText"].ToString();
                                category.MenuCategoryInActive = reader["MenuCategoryInActive"].ToString();
                                category.StoreID = reader["StoreID"].ToString();

                                listCategories.Add(category);
                            }

                            categoriesResponse.InfoCategory = listCategories;

                        }
                        else
                        {
                            categoriesResponse.ResponseCode = "002";
                            categoriesResponse.ResponseMessage = "Not exist categories for this licence";
                        }

                    }
                }
                catch (SqlException sqlex)
                {
                    categoriesResponse.ResponseCode = "001";
                    categoriesResponse.ResponseMessage = "Error: " + sqlex.Message;
                }
                catch (Exception ex)
                {
                    categoriesResponse.ResponseCode = "001";
                    categoriesResponse.ResponseMessage = "Error: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }

            return categoriesResponse;
        }
    }

}