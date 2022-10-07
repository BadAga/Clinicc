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
    public class DoctorAppointmentRequestsViewModel:ViewModelBase
    {
        public Model.Doctor Doctor { get; set; }

        private List<Model.Appointment> appointments = new List<Model.Appointment>();
        public List<Model.Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged(nameof(Appointments));
            }
        }
        public ICommand LogOutHPCommand { get; }

        public ICommand OverviewDoctorCommand { get; }

        public ICommand MyScheduleDoctorCommand { get; }

        public ICommand AppointmentRequestsCommand { get; }

        public DoctorAppointmentRequestsViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Doctor doc)
        {
            Doctor = doc;

            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewDoctorCommand = new NavigateToDoctorMainView(hospital, navigation, doc);
            MyScheduleDoctorCommand = new MyScheduleDoctorCommand(hospital, navigation, doc);
            AppointmentRequestsCommand = new AppointmentRequestDoctorCommand(hospital, navigation, doc);
            GenerateListOfAppointmentRequests();
        }

        private void GenerateListOfAppointmentRequests()
        {
            Appointments = Doctor.GetListOfAppointments();
        }

    }
}
