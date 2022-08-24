using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Doctor:User
    {
        public Specialization specialization;

        public Schedule schedule;

        public Doctor(int _id,string _name, string _surname, string _pesel, string _login, string _password, string spec)
        {
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            Id = _id;

            schedule = new Schedule(Id);
            specialization = new Specialization(int.Parse(spec));
        }
        public Doctor(string _name, string _surname, string _pesel, string _login, string _password, string spec)
        {
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            Id = 0;

            schedule = new Schedule(Id);
            specialization = new Specialization(int.Parse(spec));
        }
        public Doctor(string _name, string _surname,
             string _pesel, string _login, string _password)
        {
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            Id = 0;

            schedule = new Schedule(Id);
            specialization = new Specialization();
        }
        public string GetFullLineDatabase()
        {
            string line = GetLineDatabase() + ' ' + specialization.Id_SPEC;
            return line;
        }

        public void SaveInDatabase()
        {
            string fileName = @"C:\Users\agnie\source\repos\WPF-projects\Clinicc\Clinicc\DataSource\ExistingDoctors.txt";
            SaveUserInDatabase(fileName, GetFullLineDatabase());
        }

        public void SetSpecializationFromDictionary()
        {
            Specialization spec = new Specialization(int.Parse(Specialization.GetSpecIdFromFile(this.PESEL)));
            this.specialization = spec;
        }
    }
}
