using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Schedule
    {
        private int id_doc;
        public Dictionary< DateTime , Day> calendars;

        public Schedule()
        {
            calendars = new Dictionary<DateTime, Day>();
        }

        public IEnumerable<Appointment> GetAppointmentsForPatient(int id_pat)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach (var day in calendars.Values)
            {
                foreach(var result in day.GetAppointmentsForPatient(id_pat))
                {
                    appointments.Add(result);
                }                
            }
            return null;
        }

        public bool AddAppointment(Appointment new_appointment)
        {
           if(calendars.ContainsKey(new_appointment.date))
            {
                return calendars[new_appointment.date].AddApppointment(new_appointment);                 
            }
            return false;
        }
    }
}
