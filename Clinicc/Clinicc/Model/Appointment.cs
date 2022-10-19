using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class Appointment
    {
        public int Id_app;
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public DateTime date { get; set; }

        public int Id_doc;
        public TimeSpan Length { get; set; }

        public int Id_pat;

        private bool is_appointment = true;

        private bool is_all_day = false;

        private int status = 0; //0-issued 1-confirmed 2-denied

        //properties below are for getting descriptive information
        public string status_desc { get; set; }

        public String patfullname { get; set; }

        public String docfullname { get; set; }

        public String JustTime { get; set; }

        public int GetStatus()
        {
            return this.status;
        }

        public Appointment(int _id, DateTime _stime, DateTime _end_time, DateTime _date,
                           int _id_doc, int _id_pat, bool isapp, bool isallday, int statusspp)
        {
            Id_app = _id;
            start_time = _stime;
            end_time = _end_time;
            date = _date;
            Id_doc = _id_doc;
            Id_pat = _id_pat;
            is_appointment = isapp;
            is_all_day = isallday;
            status = statusspp;
            ChengeDescriptiveProperties();
            this.Length = end_time - start_time;
            if (start_time.Minute < 10)
            {
                JustTime = start_time.Hour + ":0" + start_time.Minute;
            }
            else
            {
                JustTime = start_time.Hour + ":" + start_time.Minute;
            }

        }

        //Constructor for appointments made by patients; with default values; patient appointments are 30 min long
        public Appointment(int _id, DateTime _stime, DateTime _date,int _id_doc, int _id_pat)
        {
            Id_app = _id;
            start_time = _stime;
            end_time = start_time.AddMinutes(30);
            date = _date;
            Id_doc = _id_doc;
            Id_pat = _id_pat;
            ChengeDescriptiveProperties();
            this.Length = end_time - start_time;
            if (start_time.Minute < 10)
            {
                JustTime = start_time.Hour + ":0" + start_time.Minute;
            }
            else
            {
                JustTime = start_time.Hour + ":" + start_time.Minute;
            }
        }

        //used only to create time options
        //not for creating real appointments
        public Appointment( DateTime startTime)
        {
            this.start_time = startTime;
            this.date = startTime.Date;
            this.end_time = start_time.AddMinutes(30);
            this.Length = end_time - start_time;

            Id_doc = 0;
            Id_pat = 0;
            Id_app = 0;

            patfullname = "";
            docfullname = "";
            if (start_time.Minute < 10)
            {
                JustTime = start_time.Hour + ":0" + start_time.Minute;
            }
            else
            {
                JustTime = start_time.Hour + ":" + start_time.Minute;
            }
        }

        private void ChengeDescriptiveProperties()
        {
            ChangeStatusDesc();
            using (DatabaseEntities db=new DatabaseEntities())
            {
                Clinicc.Doctor dbdoc = (from dr in db.Doctors
                                        where dr.Id == this.Id_doc
                                        select dr).SingleOrDefault();
                docfullname = "dr. "+ dbdoc.name + " " + dbdoc.surname;
            }
            using (DatabaseEntities db = new DatabaseEntities())
            {
                Clinicc.Patient dbpat = (from p in db.Patients
                                        where p.Id == this.Id_pat
                                        select p).SingleOrDefault();
                patfullname = dbpat.name + " " + dbpat.surname;
            }

        }
        private void ChangeStatusDesc()
        {
            if (status == 0)
            {
                status_desc = "issued";
            }
            else if (status == 1)
            {
                status_desc = "confirmed";
            }
            else if (status == 2)
            {
                status_desc = "denied";
            }
        }


        //0-issued 1-confirmed 2-denied
        public void ChangeStatus(int changedStatus)
        {
            status = changedStatus;
            ChangeStatusDesc();
            Clinicc.Appointment dbapp = new Clinicc.Appointment();
            using (var db = new DatabaseEntities())
            {
                dbapp = (from app in db.Appointments
                             where app.Id == this.Id_app
                             select app).SingleOrDefault();               
            }
            dbapp.status = this.status;
            using (var db = new DatabaseEntities())
            {
                db.Entry(dbapp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        static public Clinicc.Appointment ConvertModelAppointmentToDBAppointment(Model.Appointment app)
        {
            Clinicc.Appointment appointment = new Clinicc.Appointment();
            appointment.start_time = app.start_time;
            appointment.end_time = app.end_time;
            appointment.status = app.status;

            if (app.is_appointment)
            { appointment.is_appointment = 1; }
            else { appointment.is_appointment = 0; }

            if (app.is_all_day)
            {
                appointment.all_day = 1;
            }
            else { appointment.all_day = 0; }
            appointment.Id_doc=app.Id_doc;
            appointment.Id_pat = app.Id_pat;
            appointment.Id_schedule = app.Id_doc;     
            return appointment;
        }
    }
}
