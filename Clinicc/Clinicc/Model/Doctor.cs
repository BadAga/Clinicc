using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Doctor
    {
        public int Id_doc { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string PESEL { get; set; }
        public string login { get; set; }
        public string password { get; set; }

        Specialization specialization;

        Schedule schedule;

        public Doctor(int _id, string _name, string _surname, string _pesel, string _login, string _password)
        {
            Id_doc = _id;
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            schedule= new Schedule();

        }
    }
}
