using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Schedule
    {
        private int id_sch;
        private int id_doc;
        public Dictionary< DateTime , Day> calendars;
        private DateTime schedule_till;

        public Schedule(int id)
        {
            //ids of schedule and doctor are the same
            this.id_doc = id;
            id_sch = GetNumberOFExistingSchedules();
            calendars = new Dictionary<DateTime, Day>();
            schedule_till = DateTime.Now.AddMonths(4);
            SaveSchedulInDB();
        }

        private int GetNumberOFExistingSchedules()
        {
            int num_of_schedules=0;
            using (var db = new DatabaseEntities())
            {
                num_of_schedules = (from s in db.Schedules
                                        select s).Count();
            }
            return num_of_schedules;
        }
        private void SaveSchedulInDB()
        {
            Clinicc.Schedule sch=new Clinicc.Schedule();
            sch.id_doctor=id_doc;           
            sch.schedlue_till=schedule_till;
           
            using (var db=new DatabaseEntities())
            {
                db.Schedules.Add(sch);
                db.SaveChanges();
            }
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

        public void AddDay(Day day)
        {
            calendars.Add(day.date, day);
        }
        public bool AddAppointment(Appointment new_appointment)
        {
           if(calendars.ContainsKey(new_appointment.date))
            {
                return calendars[new_appointment.date].AddApppointment(new_appointment);                 
            }
            return false;
        }

        public List<DateTime> GetAppointmentTimeOptions(DateTime dateOfAppointment)
        {
            return calendars[calendars[dateOfAppointment].date].GetAppointmentTimeOptions();    
        }

        public List<Appointment> GetListOfWeekdayAppointments(DayOfWeek weekday,int week=0)
        {
            List<Appointment> weekdayAppointments = new List<Appointment>();
            int counter = -1;
            foreach(var day in calendars.Values)
            {
                if(day.date.DayOfWeek==weekday)
                {
                    counter++;
                    if(counter==week)
                    {
                        weekdayAppointments = day.GetEventsList();
                    }
                }
            }
            return weekdayAppointments;
        }

    }
}
