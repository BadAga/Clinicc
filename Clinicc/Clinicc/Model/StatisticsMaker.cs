using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Model
{
    public class StatisticsMaker
    {
        static public int GetWeeklyAppointments(List<Clinicc.Appointment> sortedListOfAppointments)
        {
            int weeklyAppointments = 0;
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            DateTime currentDate = DateTime.Now.Date;
            DateTime startOfWeekDate = currentDate.AddDays(-1 * (int)cal.GetDayOfWeek(currentDate)+1);
            DateTime endOfWeekDate = startOfWeekDate.AddDays(6);
            //list is sorted in ascending order
            bool thisWeek = false;
            foreach(Clinicc.Appointment appointment in sortedListOfAppointments)
            {
                if(appointment.start_time.Date>=startOfWeekDate&& appointment.start_time.Date<=endOfWeekDate)
                {
                    thisWeek = true;
                }
                else
                {
                    thisWeek = false;
                }
                if(thisWeek)
                {
                    weeklyAppointments++;
                }
            }
            return weeklyAppointments;
        }
        static public int GetIncreseWeeklyAppointments(List<Clinicc.Appointment> sortedListOfAppointments)
        {
            int currentWeeklyAppointments = GetWeeklyAppointments(sortedListOfAppointments);
            int prevWeeklyAppointments = 0;
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            DateTime currentDate = DateTime.Now.Date;
            DateTime startOfCurrentWeekDate = currentDate.AddDays(-1 * (int)cal.GetDayOfWeek(currentDate)+1);
            DateTime prevStartOfWeekDate = startOfCurrentWeekDate.AddDays(-1 * 7);

            //list is sorted in ascending order
            bool prevWeek = false;
            foreach (Clinicc.Appointment appointment in sortedListOfAppointments)
            {
                if (appointment.start_time.Date >= prevStartOfWeekDate && appointment.start_time.Date < startOfCurrentWeekDate)
                {
                    prevWeek = true;
                }
                else
                {
                    prevWeek = false;
                }
                if (prevWeek)
                {
                    prevWeeklyAppointments++;
                }
            }
            if (prevWeeklyAppointments > 0)
            {
                int difference = currentWeeklyAppointments - prevWeeklyAppointments;
                double numerator = (difference / prevWeeklyAppointments) * 100;

                return (int)numerator;
            }
            else
            {
                return 0;
            }
        }
    }
}
