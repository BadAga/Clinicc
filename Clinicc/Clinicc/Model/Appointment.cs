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

        public int Id_doc;

        public int Id_pat;

        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public DateTime date { get; set; }
        public TimeSpan Length { get; set; }       


        public bool is_appointment { get; set; }
        public bool is_all_day { get; set; }


        private int status = 0; //0-issued 1-confirmed 2-denied
        public string status_desc { get; set; }

        public Model.Doctor doc { get; set; }
        public Model.Patient pat { get; set; }
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
            ChangeDescriptiveProperties();
            this.Length = end_time - start_time;
            this.SetPatient();
            this.SetDoctor();

        }

        //Constructor for appointments made by patients; with default values; patient appointments are 30 min long
        public Appointment(int _id, DateTime _stime, DateTime _date,int _id_doc, int _id_pat,Model.Patient patientBooking)
        {
            Id_app = _id;
            start_time = _stime;
            end_time = start_time.AddMinutes(30);
            date = _date;
            Id_doc = _id_doc;
            Id_pat = _id_pat;
            ChangeDescriptiveProperties();
            this.Length = end_time - start_time;            
            this.pat = patientBooking;
            is_all_day = false;
            is_appointment = true;
            this.SetDoctor();

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
        }


        //methods
            private void ChangeDescriptiveProperties()
            {
                ChangeStatusDesc();
                if (start_time.Minute < 10)
                {
                    JustTime = start_time.Hour + ":0" + start_time.Minute;
                }
                else
                {
                    JustTime = start_time.Hour + ":" + start_time.Minute;
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

        public void ChangeStatus(int changedStatus)
        {
            //0-issued 1-confirmed 2-denied
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

            private void SetPatient()
            {
                using(var db = new DatabaseEntities())
                {
                    var Patient=(from p in db.Patients
                                 where p.Id==this.Id_pat
                                 select p).FirstOrDefault();
                    this.pat = Converter.ConvertPatinet(Patient);
                }
            }
            private void SetDoctor()
            {
                using (var db = new DatabaseEntities())
                {
                    var doctorInDb = (from d in db.Doctors
                                   where d.Id == this.Id_doc
                                   select d).FirstOrDefault();
                    if (doctorInDb != null)
                    {
                        this.doc = Converter.ConvertDoctor(doctorInDb);
                    }
                }
            }
    }
}
