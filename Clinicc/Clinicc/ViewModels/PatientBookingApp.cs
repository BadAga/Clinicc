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
    public class PatientBookingApp : ViewModelBase
    {
        //done commands
        public ICommand OverviewPatientCommand { get; }
        public ICommand BookAppPatientCommand { get; }
        public ICommand LogOutHPCommand { get; }
        //to do commands:


        public PatientBookingApp(Hospital hospital, NavigationStore navigation, Clinicc.Model.Patient pat)
        {
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewPatientCommand = new NavigateToPatientMainView(hospital, navigation, pat);
            BookAppPatientCommand = new PatientBookingAppointmentCommand(hospital, navigation, pat);
        }
    }
}
