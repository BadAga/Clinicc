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

        private Model.Appointment selectedAppointment;
        public Model.Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set { selectedAppointment = value; 
                OnPropertyChanged(nameof(SelectedAppointment)); }
        }
        
        //commands
        public ICommand LogOutHPCommand { get; }

        public ICommand OverviewDoctorCommand { get; }

        public ICommand MyScheduleDoctorCommand { get; }

        public ICommand AppointmentRequestsCommand { get; }

        public RelayCommand ConfirmAppointment { get; private set; }
        public RelayCommand DenyAppointment { get; private set; }

        public DoctorAppointmentRequestsViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Doctor doc)
        {
            Doctor = doc;

            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewDoctorCommand = new NavigateToDoctorMainView(hospital, navigation, doc);
            MyScheduleDoctorCommand = new MyScheduleDoctorCommand(hospital, navigation, doc);
            AppointmentRequestsCommand = new AppointmentRequestDoctorCommand(hospital, navigation, doc);
            ConfirmAppointment = new RelayCommand(ConfirmAppointmentImpl);
            DenyAppointment = new RelayCommand(DenyAppointmentImpl);
            GenerateListOfAppointmentRequests();
        }

        private void GenerateListOfAppointmentRequests()
        {
            Appointments = Doctor.GetListOfAppointments();
        }
        private void ConfirmAppointmentImpl(object o)
        {
            if(!(o is Model.Appointment))
            {
                return;
            }
            else
            {
                SelectedAppointment = (Model.Appointment)o;
                SelectedAppointment.ChangeStatus(1);

            }            
        }
        private void DenyAppointmentImpl(object o)
        {
            if (!(o is Model.Appointment))
            {
                return;
            }
            else
            {
                SelectedAppointment = (Model.Appointment)o;
                SelectedAppointment.ChangeStatus(2);

            }
        }

    }
}
