using Clinicc.Commands;
using Clinicc.Model;
using Clinicc.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clinicc.ViewModels
{
    public class PatientBookingAppViewModel : ViewModelBase
    {
        private ObservableCollection<String> _specs;
        public ObservableCollection<String> Specs
        {
            get { return _specs; }
            set { _specs = value; }
        }

        private ObservableCollection<String> _spec;
        public ObservableCollection<String> Spec
        {
            get { return _spec; }
            set { _spec = value; }
        }

        //done commands
        public ICommand OverviewPatientCommand { get; }
        public ICommand BookAppPatientCommand { get; }
        public ICommand LogOutHPCommand { get; }
        //to do commands:


        public PatientBookingAppViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Patient pat)
        {
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewPatientCommand = new NavigateToPatientMainView(hospital, navigation, pat);
            BookAppPatientCommand = new PatientBookingAppointmentCommand(hospital, navigation, pat);
            Specs = Model.Specialization.GetSpecsNameList();
        }
    }
}
