using Clinicc.Commands;
using Clinicc.Model;
using Clinicc.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clinicc.ViewModels
{
    public class DoctorScheduleViewModel : ViewModelBase
    {
        private List<Model.Appointment> modayApp;
        public List<Model.Appointment> MondayApp
        {
            get { return modayApp; }
            set { modayApp = value;
                OnPropertyChanged(nameof(MondayApp)); }
        }

        private List<Model.Appointment> tuesdayApp;
        public List<Model.Appointment> TuesdayApp
        {
            get { return tuesdayApp; }
            set
            {
                tuesdayApp = value;
                OnPropertyChanged(nameof(TuesdayApp));
            }
        }

        private List<Model.Appointment> wednesdayApp;
        public List<Model.Appointment> WednesdaApp
        {
            get { return wednesdayApp; }
            set
            {
                wednesdayApp = value;
                OnPropertyChanged(nameof(WednesdaApp));
            }
        }

        private List<Model.Appointment> thursdayApp;
        public List<Model.Appointment> ThursdayApp
        {
            get { return thursdayApp; }
            set
            {
                thursdayApp = value;
                OnPropertyChanged(nameof(ThursdayApp));
            }
        }

        private List<Model.Appointment> fridayApp;
        public List<Model.Appointment> FridayApp
        {
            get { return fridayApp; }
            set
            {
                fridayApp = value;
                OnPropertyChanged(nameof(FridayApp));
            }
        }

        public Model.Doctor Doctor { get; set; }
        public ICommand LogOutHPCommand { get; }

        public ICommand OverviewDoctorCommand { get; }

        public ICommand MyScheduleDoctorCommand { get; }

        public ICommand AppointmentRequestsCommand { get; }

        public ICommand StatisticsCommand { get; }

        public DoctorScheduleViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Doctor doc)
        {
            Doctor = doc;
            doc.PrepareSchedule();
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewDoctorCommand = new NavigateToDoctorMainView(hospital, navigation, doc);
            MyScheduleDoctorCommand = new NavigateToMyScheduleDoctorCommand(hospital, navigation, doc);
            AppointmentRequestsCommand = new NavigateToAppointmentRequestDoctorCommand(hospital, navigation, doc);
            StatisticsCommand = new NavigateToDoctorStatisticsCommand(hospital, navigation, doc);
            FillAppointmentLists();

        }

        private void FillAppointmentLists()
        {
            MondayApp=Doctor.schedule.GetListOfWeekdayAppointments(DayOfWeek.Monday);
            TuesdayApp = Doctor.schedule.GetListOfWeekdayAppointments(DayOfWeek.Tuesday);
            WednesdaApp= Doctor.schedule.GetListOfWeekdayAppointments(DayOfWeek.Wednesday);
            ThursdayApp = Doctor.schedule.GetListOfWeekdayAppointments(DayOfWeek.Thursday);
            FridayApp= Doctor.schedule.GetListOfWeekdayAppointments(DayOfWeek.Friday);
        }
    }
}
