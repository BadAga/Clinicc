using System;
using System.Collections.Generic;
using System.IO;
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

        public void  AddExistingDoctors()
        {
            string fileName = @"C:\Users\agnie\source\repos\WPF-projects\Clinicc\Clinicc\DataSource\ExistingDoctors.txt";

            IEnumerable<string> lines = File.ReadLines(fileName);
            
            string id = String.Empty;
            string name = String.Empty;
            string surname = String.Empty;
            string pesel = String.Empty;
            string login = String.Empty;
            string password = String.Empty;
            string spec = String.Empty;

            foreach (string line in lines)
            {
                var words = line.Split(' ');
                id = words[0];
                name = words[1];
                surname = words[2];
                pesel = words[3];
                login = words[4];
                password = words[5];
                spec = words[6];
                Doctor new_doctor = new Doctor(int.Parse(id), name, surname, pesel, login, password, spec);
                AddDoctor(new_doctor);
                id = String.Empty;
                name = String.Empty;
                surname = String.Empty;
                pesel = String.Empty;
                login = String.Empty;
                password = String.Empty;
                spec = String.Empty;
            }
        }
        public void AddExistingPatients()
        {
            string fileName = @"C:\Users\agnie\source\repos\WPF-projects\Clinicc\Clinicc\DataSource\ExistingPatients.txt";

            IEnumerable<string> lines = File.ReadLines(fileName);

            string id = String.Empty;
            string name = String.Empty;
            string surname = String.Empty;
            string pesel = String.Empty;
            string login = String.Empty;
            string password = String.Empty;            

            foreach (string line in lines)
            {
                var words = line.Split(' ');
                id = words[0];
                name = words[1];
                surname = words[2];
                pesel = words[3];
                login = words[4];
                password = words[5];
               
                Patient new_patient = new Patient(int.Parse(id), name, surname, pesel, login, password);
                AddPatient(new_patient);
                id = String.Empty;
                name = String.Empty;
                surname = String.Empty;
                pesel = String.Empty;
                login = String.Empty;
                password = String.Empty;                
            }
        }
        public void AddDoctor(Doctor doc)
        {
            if(NoLoginReapeating(doc))
            {
                if(doc.Id==0)
                {
                    doc.Id = doctors.Count()+1;
                }
                doctors.Add(doc.Id, doc);
            }           
            //to do: login exists messagge
        }
        public void AddPatient(Patient pat)
        {
            if (NoLoginReapeating(pat))
            {
                if(pat.Id==0)
                {
                    pat.Id=patients.Count()+1;
                }
                patients.Add(pat.Id, pat);
            }
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
