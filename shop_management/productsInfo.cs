using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop_management
{
    public class productsInfo
    {
        protected int Id;
        protected string Name;
        protected int Quantity;
        protected double Price;
        protected static double TotalPrice;
        protected string Brand;
        protected string Warranty;
        protected double OriginalPrice;

        public int ProductId
        {
            get { return this.Id; }
            set { this.Id = value; }
        }
        public string ProductName
        {
            get { return this.Name; }
            set { this.Name = value; }
        }
        public int ProductQuantity
        {
            get { return this.Quantity; }
            set { this.Quantity = value; }
        }

        public double ProductPrice
        {
            get { return this.Price; }
            set { this.Price = value; }

        }
        public string ProductBrand
        {
            get { return this.Brand; }
            set { this.Brand= value; }

        }
        public string ProductWarranty
        {
            get { return this.Warranty; }
            set { this.Warranty = value; }

        }
        public double ProductOriginalPrice
        {
            get { return this.OriginalPrice; }
            set { this.OriginalPrice = value; }

        }

        public double CalculateTotalPrice()
        {
            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to retrieve the product price based on the product name
                string query = "SELECT price FROM products WHERE productName = @ProductName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adding parameter to the query to prevent SQL injection
                    command.Parameters.AddWithValue("@ProductName", ProductName);

                    // Retrieving the product price from the database
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        double productPrice = Convert.ToDouble(result);

                        // Calculating the total price
                        TotalPrice += Quantity * productPrice;

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
