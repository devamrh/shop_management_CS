using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop_management
{
    class search:productsInfo
    {


        public void CaclSearch()
        {
            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to retrieve product details based on the product name
                string query = "SELECT Id, productName, price, quantity, brand, warranty FROM products WHERE productName = @ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adding parameter to the query to prevent SQL injection
                    command.Parameters.AddWithValue("@ProductName", ProductName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assigning values to the properties
                            ProductId = Convert.ToInt32(reader["Id"]);
                            ProductName = reader["productName"].ToString();
                            ProductPrice = (double)Convert.ToDecimal(reader["price"]);
                            ProductQuantity = Convert.ToInt32(reader["quantity"]);
                            ProductBrand = reader["brand"].ToString();
                            ProductWarranty = reader["warranty"].ToString();
                            ProductOriginalPrice = Convert.ToInt32(reader["originalPrice"]);
                        }
                        else
                        {
                            // Handle the case when the product with the specified name is not found
                            // You can add your error handling logic here
                            // For example, throw an exception or log a message
                        }
                    }
                }
            }
        }

    }
}
