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
        private List<Clinicc.Appointment> sortedListOfAppointments;

        public StatisticsMaker(List<Clinicc.Appointment> sortedListOfAppointments)
        {
            this.sortedListOfAppointments = sortedListOfAppointments;
        }

        public int GetWeeklyAppointments()
        {
            int weeklyAppointments = 0;
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            DateTime currentDate = DateTime.Now.Date;
            DateTime startOfWeekDate = currentDate.AddDays(-1 * (int)cal.GetDayOfWeek(currentDate) + 1);
            DateTime endOfWeekDate = startOfWeekDate.AddDays(6);
            //list is sorted in ascending order
            bool thisWeek = false;
            foreach (Clinicc.Appointment appointment in sortedListOfAppointments)
            {
                if (appointment.start_time.Date >= startOfWeekDate && appointment.start_time.Date <= endOfWeekDate)
                {
                    thisWeek = true;
                }
                else
                {
                    thisWeek = false;
                }
                if (thisWeek)
                {
                    weeklyAppointments++;
                }
            }
            return weeklyAppointments;
        }
        public int GetMonthlyAppointments()
        {
            int monthlyAppointments = 0;
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            DateTime currentDate = DateTime.Now.Date;

            int currentYear = currentDate.Date.Year;
            int currentMonth = currentDate.Date.Month;
            //list is sorted in ascending order
            foreach (Clinicc.Appointment appointment in sortedListOfAppointments)
            {
                if (appointment.start_time.Date.Year == currentYear)
                {
                    if (appointment.start_time.Date.Month == currentMonth)
                    {
                        monthlyAppointments++;
                    }
                }
            }
            return monthlyAppointments;
        }
        public int GetYearlyAppointments()
        {
            int yearlyAppointments = 0;
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            DateTime currentDate = DateTime.Now.Date;

            int currentYear = currentDate.Date.Year;
            //list is sorted in ascending order
            foreach (Clinicc.Appointment appointment in sortedListOfAppointments)
            {
                if (appointment.start_time.Date.Year == currentYear)
                {
                    yearlyAppointments++;
                }
            }
            return yearlyAppointments;
        }
        public int GetIncreseWeeklyAppointments()
        {
            int currentWeeklyAppointments = this.GetWeeklyAppointments();
            int prevWeeklyAppointments = 0;
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            DateTime currentDate = DateTime.Now.Date;
            DateTime startOfCurrentWeekDate = currentDate.AddDays(-1 * (int)cal.GetDayOfWeek(currentDate) + 1);
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
                double numerator = (difference * 100) / prevWeeklyAppointments;

                return (int)numerator;
            }
            else
            {
                return 0;
            }
        }
        public double GetAverageWeeklyNumAppointments()
        {
            double numOfAppointments =(double) this.sortedListOfAppointments.Count;
            double avg= numOfAppointments / 52.0;
            return avg;
        }
        public double GetAverageMonthlyNumAppointments()
        {
            double numOfAppointments = (double)this.sortedListOfAppointments.Count;
            double avg= numOfAppointments /12.0;
            return avg;
           
        }
    }
}
