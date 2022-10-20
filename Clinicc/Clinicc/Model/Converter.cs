using Clinicc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    //class for converting objects from DB to Model and vice versa
    public class Converter
    {
        static public Model.Appointment ConvertAppointment(Clinicc.Appointment db_app)
        {
            DateTime date=db_app.start_time.Date;
            bool is_all_day;
            bool is_appointment;
            int status = (int)db_app.status;
            if (db_app.is_appointment==1)
            {
                is_appointment=true;
                is_all_day=false;
            }
            else
            {
                is_appointment=false;
                if(db_app.all_day==1)
                {
                    is_all_day = true;
                }
                else
                {
                    is_all_day = false;
                }
            }

            Model.Appointment converted = new Model.Appointment(db_app.Id, db_app.start_time, db_app.end_time, date, db_app.Id_doc,
                                                              db_app.Id_pat, is_appointment, is_all_day, status);
            return converted;
        }

        static public Clinicc.Appointment ConvertAppointment(Model.Appointment db_app)
        {
            Clinicc.Appointment appointment = new Clinicc.Appointment();
            appointment.start_time = db_app.start_time;
            appointment.end_time = db_app.end_time;
            appointment.status = db_app.GetStatus();

            if (db_app.is_appointment)
            { appointment.is_appointment = 1; }
            else { appointment.is_appointment = 0; }

            if (db_app.is_all_day)
            {
                appointment.all_day = 1;
            }
            else { appointment.all_day = 0; }
            appointment.Id_doc = db_app.Id_doc;
            appointment.Id_pat = db_app.Id_pat;
            appointment.Id_schedule = db_app.Id_doc;
            return appointment;
        }
        static public Model.Doctor ConvertDoctor(Clinicc.Doctor dbdoc)
        {
            Model.Doctor modeldoc = new Model.Doctor(dbdoc.Id, dbdoc.name, dbdoc.surname, dbdoc.PESEL, dbdoc.login, dbdoc.password, dbdoc.spec_id);
            return modeldoc;
        }

        static public Model.Patient ConvertPatinet(Clinicc.Patient dbpat)
        {
            Model.Patient modelpat = new Model.Patient(dbpat.Id, dbpat.name, dbpat.surname,
                                                     dbpat.PESEL, dbpat.login, dbpat.password);

            return modelpat;
        }

        static public Model.Specialization ConvertSpecialization (Clinicc.Specialization dbspec)
        {
            Model.Specialization modelSpec = new Model.Specialization(dbspec.Id);
            return modelSpec;
        }
    }
}
