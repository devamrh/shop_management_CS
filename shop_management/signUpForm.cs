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
    public partial class signUpForm : Form
    {
        public signUpForm()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loginInfo a = new loginInfo();
            a.Username = textBox10.Text;
            if (a.UserNameMatching == true)
            {
                if (textBox11.Text == textBox12.Text)
                {
                    a.Username = textBox10.Text;
                    a.Password = textBox11.Text;
                    a.type = comboBox1.SelectedItem.ToString();
                    a.SignUp();
                    MessageBox.Show("Success");
                    textBox10.Text = "";
                    textBox11.Text = "";
                    textBox12.Text = "";
                    comboBox1.Text = "";
                }
                else
                    MessageBox.Show("password does not match !!!!!");
                textBox11.Text = null;
                textBox12.Text = null;
            }
            else
                MessageBox.Show("This user name has already exist ");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            comboBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            adminForm a= new adminForm();
            this.Hide();
            a.Show();

        }
    }
}
