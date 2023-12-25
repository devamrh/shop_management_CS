using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace shop_management
{
    public partial class adminForm : Form
    {
        public adminForm()
        {

            InitializeComponent();
            timer2.Start();
            textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col = new AutoCompleteStringCollection();
            //MyClassDataContext m = new MyClassDataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C# project\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
            //int a=m.products.Count();

            //int id = 1;
            //while (id <= a)
            //{

            //    MyClassDataContext md = new MyClassDataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C# project\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
            //    product t = md.products.SingleOrDefault(x => x.Id == id);

            //    col.Add(t.productName);
            //    id++;
            //}
            textBox1.AutoCompleteCustomSource = col;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxID.Text != "" && textBoxQuantity.Text != "" && textBoxPrice.Text != "" && textBoxName.Text != "" && textBoxWarranty.Text != "" && textBoxBrand.Text != "" && textBoxOriginalPrice.Text != "")
            {
                DatabaseAccess.Id = Convert.ToInt32(textBoxID.Text);
                DatabaseAccess.Name = textBoxName.Text;
                DatabaseAccess.Price = Convert.ToDouble(textBoxPrice.Text);
                DatabaseAccess.Quantity = Convert.ToInt16(textBoxQuantity.Text);
                DatabaseAccess.Brand = textBoxBrand.Text;
                DatabaseAccess.Warranty = textBoxWarranty.Text;
                DatabaseAccess.OriginalPrice = Convert.ToInt16(textBoxOriginalPrice.Text);

                var result = DatabaseAccess.AddItem();
                if (result)
                {
                    textBoxID.Text = null;
                    textBoxQuantity.Text = null;
                    textBoxPrice.Text = null;
                    textBoxName.Text = null;
                    textBoxBrand.Text = null;
                    textBoxWarranty.Text = null;
                    textBoxOriginalPrice.Text = null;
                    MessageBox.Show("Completed");
                    this.LoadProducts();
                }
                else
                {
                    MessageBox.Show("Error");
                }

            }
            else
                MessageBox.Show("please ID,price,quantity,product name,Brand,Warranty,Original price Insert correctly");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.LoadProducts();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                try
                {
                    int id = Convert.ToInt32(textBox5.Text);
                    DatabaseAccess.DeleteItem(id);
                    textBox5.Text = string.Empty;
                    MessageBox.Show("Item deleted successfully.");

                    // Refresh the UI here
                    // Assuming you have a DataGridView named dataGridView1
                    dataGridView1.DataSource = null; // Clear the data source
                    dataGridView1.Refresh();
                    this.LoadProducts();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid numeric ID.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please insert an ID for the item you want to delete.");
            }
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBoxIDforUp.Text != "")
            {
                DatabaseAccess.Id = Convert.ToInt16(textBoxIDforUp.Text);
                DatabaseAccess.SearchItem();
                textBoxNameforUp.Text = DatabaseAccess.Name;
                textBoxPriceforUp.Text = Convert.ToString(DatabaseAccess.Price);
                textBoxQunforUp.Text = Convert.ToString(DatabaseAccess.Quantity);
                textBoxBrandforUp.Text = DatabaseAccess.Brand;
                textBoxWarrantyforUp.Text = DatabaseAccess.Warranty;
                textBoxOriginalPriceforUp.Text = Convert.ToString(DatabaseAccess.OriginalPrice);
            }
            else
                MessageBox.Show("Please Insert id 1st :p");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBoxIDforUp.Text != "" && textBoxNameforUp.Text != "" && textBoxPriceforUp.Text != "" && textBoxQunforUp.Text != "")
            {
                DatabaseAccess.Id = Convert.ToInt16(textBoxIDforUp.Text);
                DatabaseAccess.Quantity = Convert.ToInt16(textBoxQunforUp.Text);
                DatabaseAccess.Name = textBoxNameforUp.Text;
                DatabaseAccess.Price = Convert.ToDouble(textBoxPriceforUp.Text);
                DatabaseAccess.Brand = textBoxBrandforUp.Text;
                DatabaseAccess.Warranty = textBoxWarrantyforUp.Text;
                DatabaseAccess.OriginalPrice = Convert.ToDouble(textBoxOriginalPriceforUp.Text);


                DatabaseAccess.UpdateItem();
                textBoxIDforUp.Text = null;
                textBoxNameforUp.Text = null;
                textBoxPriceforUp.Text = null;
                textBoxQunforUp.Text = null;
                textBoxBrandforUp.Text = null;
                textBoxWarrantyforUp.Text = null;
                textBoxOriginalPriceforUp.Text = null;
            }
            else
                MessageBox.Show("please search Item before Update :)");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoginForm l = new LoginForm();
            this.Hide();
            l.Show();
        }





        private void button6_Click_1(object sender, EventArgs e)
        {
            signUpForm a = new signUpForm();
            a.Show();
            this.Hide();

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            search s = new search();
            s.ProductName = textBox1.Text;
            s.CaclSearch();

            // Create a DataTable and define its columns
            DataTable ss = new DataTable();
            ss.Columns.Add("Id");
            ss.Columns.Add("Name");
            ss.Columns.Add("Quantity"); // Corrected column name
            ss.Columns.Add("Price");
            ss.Columns.Add("Brand");
            ss.Columns.Add("Warranty");
            ss.Columns.Add("Original Price"); // Corrected column name

            // Create a DataRow and populate it with search results
            DataRow row = ss.NewRow();
            row["Id"] = Convert.ToString(s.ProductId);
            row["Name"] = Convert.ToString(s.ProductName);
            row["Quantity"] = Convert.ToString(s.ProductQuantity);
            row["Price"] = Convert.ToString(s.ProductPrice);
            row["Brand"] = Convert.ToString(s.ProductBrand);
            row["Warranty"] = Convert.ToString(s.ProductWarranty);
            row["Original Price"] = Convert.ToString(s.ProductOriginalPrice);

            // Add the DataRow to the DataTable
            ss.Rows.Add(row);

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = ss;

            // Optionally, you can auto-size the columns for better visibility
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }


        private void button9_Click_2(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && textBox1.Text.Any(char.IsDigit))
            {
                // Check if the DataGridView is data-bound to a DataTable
                if (dataGridView1.DataSource is DataTable dt)
                {
                    // Clear the rows of the DataTable
                    dt.Rows.Clear();
                }

                // Clear the DataGridView
                dataGridView1.Refresh();
                textBox1.Text = "";
            }
        }





        private void timer2_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.time_lbl.Text = dateTime.ToString();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SellingInventory s = new SellingInventory();
            this.Hide();
            s.Show();
        }

        private void adminForm_Load(object sender, EventArgs e)
        {
            this.LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                var query = "select * from products";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                var dt = ds.Tables[0];

                dataGridView1.DataSource = dt;
                dataGridView1.AutoGenerateColumns = true;
                dataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
