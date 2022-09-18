using System.Collections.Generic;
using System.Linq;


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

        public KeyValuePair<int, User> LogIn(string login, string password)
        {
            return TryLogIn(login, password);
        }
        private KeyValuePair<int,User> TryLogIn(string login, string password)
        {
            int successful_login = 0;
            User user=new User();
            if (CheckIfUser(login))
            {
                successful_login = -1;
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
                        Clinicc.Model.Doctor doc = new Doctor(doc_to_log.Id,
                                                            doc_to_log.name,
                                                            doc_to_log.surname,
                                                            doc_to_log.PESEL,
                                                            doc_to_log.login,
                                                            doc_to_log.password,
                                                            doc_to_log.spec_id);
                        user = doc;
                    }
                }
                else
                {
                    if (pat_to_log.password == password)
                    {
                        successful_login = 1;
                        Clinicc.Model.Patient pat = new Patient(pat_to_log.Id,
                                                            pat_to_log.name,
                                                            pat_to_log.surname,
                                                            pat_to_log.PESEL,
                                                            pat_to_log.login,
                                                            pat_to_log.password);
                        user = pat;
                    }
                }
            }             
            KeyValuePair<int,User> result = new KeyValuePair<int,User>(successful_login, user);
            return result;
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
