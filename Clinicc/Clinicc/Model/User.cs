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
            string line = Id.ToString() + ' ' + name + ' ' + surname + ' ' + PESEL + ' ' + login + ' ' + password;
            return line;
        }
        protected void SaveUserInDatabase(string filename, string data_to_insert)
        {
            using (TextWriter tw = new StreamWriter(filename, true))
            {
                tw.WriteLine(data_to_insert);
            }
        }
        static public bool CheckIfDoctor(string pesel_to_check)
        {
            string fileName = @"C:\Users\agnie\source\repos\WPF-projects\Clinicc\Clinicc\DataSource\DocPeselList.txt";
            IEnumerable<string> pesel_list = File.ReadLines(fileName);
            string pesel = String.Empty;
            foreach(var line in pesel_list)
            {
                var words = line.Split(' ');
                if (words.Length > 0)
                {
                    pesel = words[0];
                    if (pesel == pesel_to_check)
                    {
                        return true;
                    }
                }               
            }
            return false;
        }
        public bool SucessfullLogIn(string entered_password)
        {
            if(password==entered_password)
            {
                return true;
            }
            return false;
        }

    }
}
