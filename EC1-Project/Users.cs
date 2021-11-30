using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC1_Project
{   
    public class Users
    {
        private int id;
        private string fname;
        private string lname;
        private string username;

        public Users()
        {
            this.id = 0;
            this.fname = "<null>";
            this.lname = "<null>";
            this.username = "<null>";
        }
        public Users(int id, string fname,string lname,string username)
        {
            this.id = id;
            this.fname = fname;
            this.lname = lname;
            this.username = username;
        }

        public int Id
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Fname
        {
            get { return fname; }
            set { this.fname = value; }
        }

        public string Lname
        {
            get { return lname; }
            set { this.lname = value; }
        }

       public string Username
        {
            get { return username; }
            set { this.username = value; }
        }
    }
}