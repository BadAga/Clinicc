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

        public string code { get; set; }
        public User(string _name, string _surname, string _pesel, string _login, string _password)
        {
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            code = string.Empty;
        }
        public User()
        {
            name = "no data";
            surname = "no data";
            PESEL = "no data";
            login = "no data";
            password = "no data";
            code = string.Empty;
        }           
        static public bool CheckIfDoctor(string pesel_to_check)
        {
            using (var db =new DatabaseEntities())
            {
                var doc = (from d in db.Doctors
                           where d.PESEL.Equals(pesel_to_check)
                           select d).SingleOrDefault();
                if (doc != null)
                {
                    return true;
                }
            }            
            return false;
        }

        public List<Model.Appointment> GetListOfAppointments()
        {
            List<Model.Appointment> list = new List<Model.Appointment>();            
            return list;
        }
    }
}
