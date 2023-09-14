using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myChat
{
  
    public class User
    {
        private int _id;
        public int Id { get { return _id; }set { _id = value; } }
        private string _login;
        public string LoginName { get { return _login; }set { _login = value; }  }
        private int _password;
        public int Password { get { return _password; }set { _password = value; } }
        private int _administration;
        public int isAdministration { get { return _administration; }set { _administration = value; } }
        private bool _status;
        public bool isOnline { get { return _status; }set { _status = value; } }
        private string _username;
        public string Name { get { return _username; ; } set { _username = value; } }
        public User(string login, int password, int isAdmin = 0,string name = "<none>")
        {
            _login = login;
            _password = password;
            _administration = isAdmin;
            _username = name;
        }
        public User()
        {
        }
    }
}
