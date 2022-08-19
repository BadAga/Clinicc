using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Hospital
    {
        private Dictionary<int, Doctor> doctors;
        private Dictionary<int, Patient> patients;

        public Hospital()
        {
            doctors = new Dictionary<int, Doctor>();
            patients = new Dictionary<int, Patient>();
        }
        public void AddDoctor(Doctor doc)
        {
            if(NoLoginReapeating(doc))
            {
                doctors.Add(doc.Id, doc);
            }           
            //to do: login exists messagge
        }
        public void AddPatient(Patient pat)
        {
            NoLoginReapeating(pat);
            patients.Add(pat.Id, pat);
            //to do: login exists messagge
        }

        public bool NoLoginReapeating(User user)
        {
            foreach (User existing_user in doctors.Values)
            {
                if(existing_user.login== user.login)
                {
                    return false;
                }
            }
            foreach (User existing_user in patients.Values)
            {
                if (existing_user.login == user.login)
                {
                    return false;
                }
            }
            return true;
        }

        public bool TryLogIn(string login, string password)
        {
            bool login_found = false;
            bool successful_login = false; //true if login and password match, false if wrong password
            foreach (var patient in patients)
            {
                if (patient.Value.login == login)
                {
                    login_found = true;
                    if (patient.Value.password == password)
                    {
                        successful_login = true;
                    }
                    else
                    {
                        successful_login = false;
                        //to do: wrong password message
                    }
                    return successful_login;
                }
            }
            foreach (var doctor in doctors)
            {
                if (doctor.Value.login == login)
                {
                    login_found = true;
                    if (doctor.Value.password == password)
                    {
                        successful_login = true;
                    }
                    else
                    {
                        successful_login = false;
                        //to do: wrong password message
                    }
                    return successful_login;
                }
            }
            //to do: no user found message
            return login_found;
        }
    }
}
