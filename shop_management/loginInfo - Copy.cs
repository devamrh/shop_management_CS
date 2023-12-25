using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

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
            MyClassDataContext md = new MyClassDataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C# project\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
            login l = md.logins.SingleOrDefault(x => x.userName == username);
            

            if (username!= "" && password != "")
            {

                if (l == null)
                {
                   return false;
                }

                else if (l.password == password)
                {
                    return true;

                }
          }
            return false;

          }
        public void signUp()
        {
            MyClassDataContext md = new MyClassDataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C# project\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
            login t = new login();
            t.userName =this.username;
            t.password = this.password;
            t.type = this.type;
            md.logins.InsertOnSubmit(t);
            md.SubmitChanges();


        }
        public bool userNameMatching()
        {
            MyClassDataContext md = new MyClassDataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C# project\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
            login l = md.logins.SingleOrDefault(x => x.userName == username);
            if (username != "")
            {

                if (l == null)
                {
                    return true;
                }

             }
            return false;
            

        }
        public int checkTypeOfUser()
        {
            MyClassDataContext md = new MyClassDataContext(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\C# project\shop_management\shop_management\shop.mdf;Integrated Security=True;Connect Timeout=30");
            login l = md.logins.SingleOrDefault(x => x.userName == username);
            if (username != "")
            {

                if (l.type=="Admin")
                {
                    return 1;
                }


            }
            return 2;
            
            

        }
    }
}
