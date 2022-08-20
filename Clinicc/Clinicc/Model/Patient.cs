using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Patient:User
    {              
        public string adress;
        public string med_history_name;

        public Patient(int _id,  string _name,string _surname,
            string _pesel, string _login, string _password)
        {          
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            Id = _id;
            
            adress = String.Empty;
            med_history_name = _surname + _pesel[9];
        }
        public Patient( string _name, string _surname,
           string _pesel, string _login, string _password)
        {
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            Id = 0;

            adress = String.Empty;
            med_history_name = _surname + _pesel[9];
        }
        public void SaveInDatabase()
        {
            string fileName = @"C:\Users\agnie\source\repos\WPF-projects\Clinicc\Clinicc\DataSource\ExistingPatients.txt";
            SaveUserInDatabase(fileName,GetLineDatabase());
        }
    }
}
