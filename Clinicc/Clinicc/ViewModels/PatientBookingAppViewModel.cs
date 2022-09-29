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
        private List<Model.Specialization> _specs;
        public List<Model.Specialization> Specs
        {
            get
            {
                return _specs;
            }
            set
            {
                _specs = value;
                OnPropertyChanged(nameof(Specs));                
            }            
        }

        private List<Model.Doctor> _docs;
        public List<Model.Doctor> Docs
        {
            get
            {
                return _docs;
            }
            set
            {
                _docs = value;
                OnPropertyChanged(nameof(Docs));
            }            
        }

        private Model.Doctor _doc;
        public Model.Doctor ChosenDoc
        {
            get
            {
                return _doc;
            }
            set
            {
                _doc = value;
                OnPropertyChanged(nameof(ChosenDoc));
            }            
        }
        private Model.Specialization _spec;
        public Model.Specialization ChosenSpec
        {
            get
            {
                return _spec;
            }
            set
            {
                _spec = value;              
                OnPropertyChanged(nameof(ChosenSpec));
                if (ChosenSpec != null)
                {
                    IsSpecializationSelected = true;
                    PrepareDocListBasedOnSpecialization();
                }               
            }            
        }

        private bool _is_specialization_selected = false;
        public bool IsSpecializationSelected
        {
            get
            {
                return _is_specialization_selected;
            }
            set
            {
                _is_specialization_selected = value;
                OnPropertyChanged(nameof(IsSpecializationSelected));               
            }
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
        private void PrepareDocListBasedOnSpecialization()
        {
            Docs = Hospital.GetAllDocsWithChosenSpec(ChosenSpec);
        }
        
        
    }
}
