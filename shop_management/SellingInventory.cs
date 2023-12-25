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
    public partial class SellingInventory : Form
    {
        public SellingInventory()
        {
            InitializeComponent();
        }



   

        private void button2_Click(object sender, EventArgs e)
        {
            adminForm a = new adminForm();
            this.Hide();
            a.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void SellingInventory_Load_1(object sender, EventArgs e)
        {
            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to retrieve the SUM(profit) from the 'history' table
                string query = "SELECT SUM(profit) FROM history";

                using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                {
                    DataSet dataSet = new DataSet();
                    DataTable dataTable;

                    adapter.Fill(dataSet, "b");
                    dataTable = dataSet.Tables["b"];

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];

                        // Display the SUM(profit) in the TotalProfitTextbox
                        TotalProfitTextbox.Text = Convert.ToString(row[row.Table.Columns[0]]);
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
