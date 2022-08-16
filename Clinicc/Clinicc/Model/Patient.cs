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

        public Patient(int _id,string _adress, string _mhn, string _name,
            string _surname, string _pesel, string _login, string _password)
        {          
            name = _name;
            surname = _surname;
            PESEL = _pesel;
            login = _login;
            password = _password;
            Id = _id;
            
            adress = _adress;
            med_history_name = _mhn;
        }
    }
}
