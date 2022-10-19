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
            this.date = dateParam.Date;
            events = new Dictionary<int, Appointment>();
        }

        public List<Appointment> GetEventsList()
        {
            List<Appointment> list= new List<Appointment>();
            foreach (var app in events.Values)
            {
                if (app.GetStatus() == 1)
                {
                    list.Add(app);
                }
            }
            return list;
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
        public bool AddApppointment(Model.Appointment new_appointment)
        {
            if (CheckIfNoAppointmentConflict(new_appointment))
            {
                events.Add(new_appointment.Id_app, new_appointment);
                return true;
            }
            return false;
        }
        public void DeleteApppointment(Model.Appointment appointment)
        {
            events.Remove(appointment.Id_app);
        }
        private bool CheckIfNoAppointmentConflict(Model.Appointment new_appointment)
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

        /// <summary>
        /// For choosing appointments time in BookingAppointment.* (view and viewmodel)
        /// Allows to choose only free time options
        /// Each option is 30 min long, since that's the default length of patinet appointment
        /// Appointment start at 8 am and end at 5 pm
        /// </summary>
        /// <returns>list of time options for patinet to choose from as appointment's start time</returns>
        public List<DateTime> GetAppointmentTimeOptions()
        {
            List<DateTime> options = new List<DateTime>();
            DateTime startingTime = date.AddHours(8);
            while(startingTime!=date.AddHours(17))
            {
                Model.Appointment demo_appointment = new Model.Appointment(startingTime);
                if (CheckIfNoAppointmentConflict(demo_appointment))
                {
                    options.Add(startingTime);
                }
                startingTime = startingTime.AddMinutes(30);
            }
            return options;
        }



    }
}
