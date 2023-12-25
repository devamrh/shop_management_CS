using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace shop_management
{
    class shopingCart:productsInfo
    {
        public static string cartProductName;
        public static new int Quantity;


        public void InsertShoppingCart()
        {
            //MyClassDataContext md = new MyClassDataContext(@"Data Source=(localdb)\ProjectModels;Initial Catalog=ShopManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            //product p = md.products.SingleOrDefault(x => x.productName ==ProductName);

            //this.ProductName = p.productName;
            //double calcCartSingleProductPrice = p.price*this.ProductQuantity;
            //this.ProductPrice = calcCartSingleProductPrice;
            //this.ProductBrand = p.brand;
            //this.ProductWarranty = p.warranty;

            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(localdb)\ProjectModels;Initial Catalog=ShopManagement;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
                con.Open();

                var query = "Insert into products (productName,price,quantity,brand,warranty,originalPrice) values('"+ProductName+","+ProductPrice+","+ProductQuantity+","+ProductBrand+","+ProductWarranty+","+ProductOriginalPrice+"')";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                var dt = ds.Tables[0];

            }
            catch (Exception)
            {
                //return -1;
            }

        }
        public static double CalculateTotalPriceC()
        {
            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to retrieve the product price based on the product name
                string query = "SELECT price FROM products WHERE productName = @CartProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adding parameter to the query to prevent SQL injection
                    command.Parameters.AddWithValue("@CartProductName", cartProductName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            double productPrice = Convert.ToDouble(reader["price"]);

                            // Calculating the total price
                            TotalPrice -= Quantity * productPrice;

                            return TotalPrice;
                        }
                        else
                        {
                            // Handle the case when the product with the specified name is not found
                            // You can add your error handling logic here
                            // For example, throw an exception or log a message
                            return TotalPrice;
                        }
                    }
                }
            }
        }




    }
}
