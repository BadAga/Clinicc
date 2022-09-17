using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Clinicc.Model
{
    public class Hospital
    {        
        public Dictionary<int, Doctor> doctors { get; set; }
        public Dictionary<int, Patient> patients { get; set; }

        public Hospital()
        {
            doctors = new Dictionary<int, Doctor>();
            patients = new Dictionary<int, Patient>();
        }

        public void AddExistingDoctors()
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
                var words = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 7)
                {
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

                var words = line.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
                if (words.Length == 6)
                {
                    id = words[0];
                    name = words[1];
                    surname = words[2];
                    pesel = words[3];
                    login = words[4];
                    password = words[5];

                    Patient new_patient = new Patient(int.Parse(id), name, surname, pesel, login, password);
                    AddPatient(new_patient);                   
                }
                id = String.Empty;
                name = String.Empty;
                surname = String.Empty;
                pesel = String.Empty;
                login = String.Empty;
                password = String.Empty;
            }
        }
        public bool AddDoctor(Doctor doc)
        {
            if (NoLoginReapeating(doc))
            {
                Clinicc.Doctor doc_to_update;
                using (var db = new DatabaseEntities())
                {
                    doc_to_update = db.Doctors.Where(dr => dr.PESEL == doc.PESEL).SingleOrDefault<Clinicc.Doctor>();                              
                }
                if (doc_to_update != null)
                {
                    doc_to_update.name = doc.name;
                    doc_to_update.surname = doc.surname;
                    doc_to_update.password = doc.password;
                    doc_to_update.login = doc.login;
                    doc_to_update.spec_id = doc.specialization.Id_SPEC;
                    var existing_doc = new Clinicc.Doctor { PESEL = doc.PESEL, name = doc.name };
                }
                using (var db = new DatabaseEntities())
                {
                    db.Entry(doc_to_update).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            return false;         

        }
        public bool AddPatient(Patient pat)
        {
            if (NoLoginReapeating(pat))
            {
                Clinicc.Patient new_pat = new Clinicc.Patient();
                new_pat.login=pat.login;
                new_pat.surname=pat.surname;
                new_pat.password=pat.password;
                new_pat.name = pat.name;
                new_pat.PESEL = pat.PESEL;                
                using (var db = new DatabaseEntities())
                {
                    db.Patients.Add(new_pat);
                    db.SaveChanges();
                }
                return true;
            }
            return false;
            
            //to do: login exists messagge
        }

        private bool NoLoginReapeating(User user)
        {

            DatabaseEntities db = new DatabaseEntities();

            
            var docs = from d in db.Doctors where d.login != null select d;
            
            foreach (var doc in docs)
            {
                if ((doc.login == user.login))
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

        public int LogIn(string login, string password)
        {
            return TryLogIn(login, password);
        }
        private int TryLogIn(string login, string password)
        {
            int successful_login = 0;
            if (CheckIfUser(login))
            {
                successful_login = 2;
                Clinicc.Doctor doc_to_log;
                Clinicc.Patient pat_to_log;

                using (var db = new DatabaseEntities())
                {
                    doc_to_log = db.Doctors.Where(dr => dr.login == login).FirstOrDefault<Clinicc.Doctor>();
                }
                using (var db = new DatabaseEntities())
                {
                    pat_to_log = db.Patients.Where(pat => pat.login == login).FirstOrDefault<Clinicc.Patient>();
                }
                if(doc_to_log!=null)
                {
                    if(doc_to_log.password==password)
                    {
                        successful_login = 1;
                    }
                }
                else
                {
                    if (pat_to_log.password == password)
                    {
                        successful_login = 1;
                    }
                }
            }             
            return successful_login;
        }

        private bool CheckIfUser(string login)
        {
            Clinicc.Doctor doc_to_log;
            Clinicc.Patient pat_to_log;
  
            using (var db = new DatabaseEntities())
            {
                doc_to_log = db.Doctors.Where(dr => dr.login == login).FirstOrDefault<Clinicc.Doctor>();
            }
            using (var db = new DatabaseEntities())
            {
                pat_to_log = db.Patients.Where(pat => pat.login == login).FirstOrDefault<Clinicc.Patient>();
            }

            if (doc_to_log == null&&pat_to_log==null)
            {
                return false;
            }
            return true;     
        }
       
    }
}
