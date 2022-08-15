using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Users
    {
        private Dictionary<int, Doctor> doctors;
        private Dictionary<int, Patient> patients;

        public Users()
        {
            doctors = new Dictionary<int, Doctor>();
            patients = new Dictionary<int, Patient>();
        }
        public void AddDoctor(Doctor doc)
        {
            if(NoLoginReapeating(doc))
            {
                doctors.Add(doc.Id_doc, doc);
            }           
        }
        public void AddPatient(Patient pat)
        {
            NoLoginReapeating(pat);
            patients.Add(pat.Id_pat, pat);
        }

        public bool NoLoginReapeating(Doctor doc)
        {
            foreach (Doctor doctor in doctors.Values)
            {
                if(doctor.login==doc.login)
                {
                    return false;
                }
            }
            return true;
        }

        public bool NoLoginReapeating(Patient pat)
        {
            foreach (Patient patient in patients.Values)
            {
                if (patient.login == pat.login)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
