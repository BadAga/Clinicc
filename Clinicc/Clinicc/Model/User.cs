using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class User
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string PESEL { get; set; }
        public string password { get; set; }
        public string login { get; set; }
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
        protected string GetLineDatabase()
        {
            string line = Id + ' ' + name + ' ' + surname + ' ' + PESEL + ' ' + login + ' ' + password;
            return line;
        }
        protected void SaveUserInDatabase(string filename, string data_to_insert)
        {
            using (TextWriter tw = new StreamWriter(filename, true))
            {
                tw.WriteLine(data_to_insert);
            }
        }
    }
}
