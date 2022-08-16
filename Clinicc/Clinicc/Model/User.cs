using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class User
    {
        public int Id;
        public string name;
        public string surname;
        public string PESEL;
        public string password;
        public string login;

        public User(string _name, string _surname, string _pesel, string _login, string _password)
        {
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
        }
        public User()
        {
            name = "no data";
            surname = "no data";
            PESEL = "no data";
            login = "no data";
            password = "no data";
        }
    }
}
