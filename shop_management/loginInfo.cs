using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace shop_management
{
    class loginInfo
    {
        
        private string username, password,types;
        
        public string Username
        {
            get { return this.username; }
            set { this.username = value; }
        }
        public string Password
        {
            set { this.password = value; }
        }
        public string type
        {
            get { return this.types; }
            set { this.types= value; }
            
        }
      
        public bool ValidateUser()
        {
            if (username!= "" && password != "")
            {

                return true;
          }
            return false;

          }
        public void SignUp()
        {
            // Establishing the database connection
            using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();

                // SQL query to insert a new record into the 'logins' table
                string query = $@"
            INSERT INTO login (userName, password, type)
            VALUES ('{this.username}', '{this.password}', '{this.type}')";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool UserNameMatching
        {
            get
            {
                // Establishing the database connection
                using (SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connection.Open();

                    // SQL query to check if the username exists in the 'logins' table
                    string query = "SELECT COUNT(*) FROM login WHERE userName = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adding parameter to the query to prevent SQL injection
                        command.Parameters.AddWithValue("@Username", username);

                        int count = (int)command.ExecuteScalar();

                        // If the count is 0, the username doesn't exist
                        return count == 0;
                    }
                }

            }
        }

        public int CheckTypeOfUser()
        {
            try
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\shop-management-final\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
                con.Open();

                var query = "select * from login where userName='" + username + "' and password='" + password+"'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds);
                var dt = ds.Tables[0];

                if(dt.Rows.Count==0)
                    return -1;

                var type = dt.Rows[0]["type"].ToString();

                if (type == "Admin")
                    return 1;
                else
                    return 2;

                //return -1;
            }
            catch (Exception ex){
                return -1;
            }
            //MyClassDataContext md = new MyClassDataContext();
            //login l = md.logins.SingleOrDefault(x => x.userName == username);
            //if (username != "")
            //{

            //    if (l.type=="Admin")
            //    {
            //        return 1;
            //    }


            //}
            //return 2;
            
            

        }
    }
}
