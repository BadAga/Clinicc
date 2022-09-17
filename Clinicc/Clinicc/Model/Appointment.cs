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
        public TimeSpan Length => end_time - start_time;

        public int Id_pat;
     
        public Appointment(int _id, DateTime _stime, DateTime _end_time, DateTime _date, int _id_doc, int _id_pat)
        {
            Id_app = _id;
            start_time = _stime;
            end_time = _end_time;
            date = _date;
            Id_doc = _id_doc;
            Id_pat = _id_pat;
        }

        
    }
}
