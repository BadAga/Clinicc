using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Day
    {
        public int id_day { get; set; }
        public DateTime date { get; set; }
        private Dictionary<int, Appointment> events;


        public Day(int id,DateTime dateParam)
        {
            this.id_day = id;
            this.date = dateParam;
            events = new Dictionary<int, Appointment>();
        }
        public IEnumerable<Appointment> GetAppointmentsForPatient(int id_pat)
        {
            List<Appointment> appointments = new List<Appointment>();
            foreach(var appointment in events.Values)
            {
                if(appointment.Id_pat==id_pat)
                {
                    appointments.Add(appointment);
                }
            }
            return appointments;
        }
        public bool AddApppointment(Appointment new_appointment)
        {
            if (CheckIfNoAppointmentConflict(new_appointment))
            {
                events.Add(new_appointment.Id_app, new_appointment);
                return true;
            }
            return false;
        }
        public void DeleteApppointment(Appointment appointment)
        {
            events.Remove(appointment.Id_app);
        }

        private bool CheckIfNoAppointmentConflict(Appointment new_appointment)
        {
            foreach(var existing_app in events)
            {
                if(existing_app.Value.start_time<=new_appointment.start_time &&
                    existing_app.Value.end_time >= new_appointment.end_time)
                {
                    return false;
                }
                else if(existing_app.Value.start_time <= new_appointment.start_time &&
                    existing_app.Value.end_time >= new_appointment.start_time)
                {
                    return false;
                }
                else if (existing_app.Value.start_time <= new_appointment.end_time &&
                    existing_app.Value.end_time >= new_appointment.end_time)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
