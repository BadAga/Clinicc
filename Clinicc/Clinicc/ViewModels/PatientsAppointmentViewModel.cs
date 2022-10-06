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
    public class PatientsAppointmentViewModel:ViewModelBase
    {        
        Clinicc.Model.Patient pat_vm=null;

        private List<Model.Appointment> appointments = new List<Model.Appointment>();
        public List<Model.Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value;
                  OnPropertyChanged(nameof(Appointments));}
        } 


        //done commands
        public ICommand OverviewPatientCommand { get; }
        public ICommand BookAppPatientCommand { get; }
        public ICommand LogOutHPCommand { get; }
        public ICommand PatAppointmentsCommand { get; }

        //to do commands:
        public PatientsAppointmentViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Patient pat)
        {            
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewPatientCommand = new NavigateToPatientMainView(hospital, navigation, pat);
            BookAppPatientCommand = new PatientBookingAppointmentCommand(hospital, navigation, pat);
            PatAppointmentsCommand=new PatAppointmentsCommand(hospital, navigation, pat);

            pat_vm = pat;
            GenerateListOfAppointments();           
        }

        private void GenerateListOfAppointments()
        {
            Appointments = pat_vm.GetListOfAppointments();
        }

    }
}
