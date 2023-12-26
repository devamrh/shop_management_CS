using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace shop_management
{
    class DatabaseAccess
    {
        public static int Id;
        public static int Quantity;
        public static string Name;
        public static double Price;
        public static double OriginalPrice;
        public static string Brand;
        public static string Warranty;

        public static double Discount;
        public static double Profit;
        //public static DateTime Date;





        //public static void UpdateforCart()
        //{
        //    MyClassDataContext md = new MyClassDataContext(@"Data Source=(localdb)\ProjectModels;Initial Catalog=ShopManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        //    product p = md.products.SingleOrDefault(x => x.productName== Name);
        //    Id = p.Id;
        //    p.quantity -= Quantity;
        //    Profit = Convert.ToDouble(( Quantity*( p.price - p.originalPrice))-Discount);
        //    md.SubmitChanges();


        //}

        public static void UpdateforCart()
        {
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    // Fetch the current product data
                    string selectQuery = $"SELECT Id, price, originalPrice, quantity FROM products WHERE productName = '{Name}'";
                    SqlCommand selectCmd = new SqlCommand(selectQuery, con);

                    using (SqlDataReader reader = selectCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int Id = (int)reader["Id"];
                            double price = (double)reader["price"];
                            double originalPrice = (double)reader["originalPrice"];
                            int currentQuantity = (int)reader["quantity"];

                            if (currentQuantity >= Quantity)
                            {
                                double Profit = (Quantity * (price - originalPrice)) - Discount;

                                reader.Close(); // Close the reader before executing another command

                                // Update the product data
                                string updateQuery = $"UPDATE products SET quantity = quantity - {Quantity} WHERE Id = {Id}";
                                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                                updateCmd.ExecuteNonQuery();
                            }
                            else
                            {
                                // Handle insufficient stock
                            }
                        }
                        else
                        {
                            // Handle product not found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }


        public static void UpdateforCartQuantity()
        {
           // MyClassDataContext md = new MyClassDataContext(@"Data Source=(localdb)\ProjectModels;Initial Catalog=ShopManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

           // product p = md.products.SingleOrDefault(x => x.productName == Name);
           //// Id = p.Id;
           // p.quantity += Quantity;
           // md.SubmitChanges();

        }


        public static bool AddItem()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();
                var query = "insert into products(productName,price,quantity,brand,warranty,originalprice) values('" + Name + "'," + Price + "," + Quantity + ",'" + Brand + "','" + Warranty + "'," + OriginalPrice + ")";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static void DeleteItem(int Id)
        {
            try
            {
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    var query = "DELETE FROM products WHERE id = @Id";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // For example, log the exception or throw it to be handled by the calling code
            }
        }


        public static void SearchItem()
        {
            int id = Id;

            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to retrieve product details based on the Id
                string query = $"SELECT * FROM products WHERE Id = {id}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            
                            Name = reader["productName"].ToString();
                            Price = (double)Convert.ToDecimal(reader["price"]);
                            Quantity = Convert.ToInt32(reader["quantity"]);
                            Brand = reader["brand"].ToString();
                            Warranty = reader["warranty"].ToString();
                            OriginalPrice = (int)Convert.ToDouble(reader["originalPrice"]);
                            
                        }
                        else
                        {
                          
                        }
                    }
                }
            }
        }


        public static void UpdateItem()
        {
            int id = Id;

            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to update product details based on the Id
                string query = $@"
            UPDATE products
            SET productName = '{Name}',
                price = {Price},
                quantity = {Quantity},
                brand = '{Brand}',
                warranty = '{Warranty}',
                originalPrice = {Convert.ToInt16(OriginalPrice)}
            WHERE Id = {id}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();

                    // Check if any rows were affected by the update
                    if (rowsAffected > 0)
                    {
                        // Update successful
                    }
                    else
                    {
                        
                    }
                }
            }
        }


        public static void CartHistoryInsert()
        {
            int productId = Id;
            string name = Name;
            int quantity = Quantity;
            decimal discount = (decimal)Discount;
            decimal profit = (decimal)Profit;
            DateTime date = DateTime.Now;

            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to insert a new record into the 'histories' table
                string query = $@"
            INSERT INTO history (productId, name, quantity, discount, profit, date)
            VALUES ({productId}, '{name}', {quantity}, {discount}, {profit}, '{date.ToString("yyyy-MM-dd HH:mm:ss")}')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }




    }
}
