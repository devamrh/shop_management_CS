using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace shop_management
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

      
    

        private void button1_Click(object sender, EventArgs e)
        {
            loginInfo lo = new loginInfo();

            lo.Username = textbox1.Text;
            lo.Password = textbox2.Text;
            if (lo.ValidateUser() == true)
            {
                var result = lo.CheckTypeOfUser();
                if (result == 2)
                {
                    salesmanForm2 s = new salesmanForm2();
                    this.Hide();
                    s.Show();
                }
                else if (result == 1)
                {
                    adminForm s = new adminForm();
                    this.Hide();
                    s.Show();
                }
                else
                {
                    MessageBox.Show("Invalid Username or password");
                }
           }

            else if (textbox1.Text == "" || textbox2.Text == "")
            {
                MessageBox.Show("please fill up user name and password");
            }
            else
            {
                MessageBox.Show("invalid Username or Password");
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
