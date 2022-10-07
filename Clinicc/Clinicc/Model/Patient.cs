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
            code = "PAT";
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
            code = "PAT";
            adress = String.Empty;
            med_history_name = _surname + _pesel[9];
        }

        public new List<Model.Appointment> GetListOfAppointments()
        {
            List<Model.Appointment> list = new List<Model.Appointment>();
            using(var db=new DatabaseEntities())
            {
                var result=(from ap in db.Appointments
                            where ap.Id_pat==this.Id
                            select ap).ToList();
                foreach(Clinicc.Appointment db_app in result)
                {
                    Model.Appointment model_app=Converter.ConvertAppointment(db_app);
                    list.Add(model_app);
                }
            }
            return list;
        }
       
    }
}
