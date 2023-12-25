using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace shop_management
{
    public partial class AllProductShowForSalesman : Form
    {
        public AllProductShowForSalesman()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            salesmanForm2 a = new salesmanForm2();
            this.Hide();
            a.Show();
        }

        private void AllProductShowForSalesman_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // SQL query to retrieve specific columns from the 'products' table
                string query = "SELECT Id, ProductName, price, quantity, brand, warranty FROM products";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
