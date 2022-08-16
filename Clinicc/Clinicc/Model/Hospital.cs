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
        }
        public void AddPatient(Patient pat)
        {
            NoLoginReapeating(pat);
            patients.Add(pat.Id, pat);
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

    }
}
