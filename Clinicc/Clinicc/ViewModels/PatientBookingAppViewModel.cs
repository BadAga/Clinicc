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
                IsDoctorSelected=true;
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

        private DateTime _selecteddate=DateTime.Now;
        public DateTime SelectedDate
        {
            get
            {
                return _selecteddate;
            }
            set
            {
                _selecteddate = value;
                OnPropertyChanged(nameof(SelectedDate));
            }
        }

        private DateTime _startdate;
        public DateTime StartDate
        {
            get
            {
                return _startdate;
            }
            set
            {
                _startdate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _enddate;
        public DateTime EndDate
        {
            get
            {
                return _enddate;
            }
            set
            {
                _enddate = value;
                OnPropertyChanged(nameof(EndDate));
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

        public bool IsDoctorSelected = false;


        public bool anyDoctor = false;
             
        public bool earliestAppointment = false;     

      
        //done commands
        public ICommand OverviewPatientCommand { get; }
        public ICommand BookAppPatientCommand { get; }
        public ICommand LogOutHPCommand { get; }
        public RelayCommand ChooseAnyDoctorCommand { get; private set; }
        public RelayCommand EarliestAppointmentCommand { get; private set; }
        //to do commands:



        public PatientBookingAppViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Patient pat)
        {
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewPatientCommand = new NavigateToPatientMainView(hospital, navigation, pat);
            BookAppPatientCommand = new PatientBookingAppointmentCommand(hospital, navigation, pat);
            ChooseAnyDoctorCommand = new RelayCommand(AnyDocToTrue);
            EarliestAppointmentCommand= new RelayCommand(EarliestAppToTrue);

            Specs = Model.Specialization.GetSpecsNameList();
            StartDate=DateTime.Now;
            EndDate = StartDate.AddMonths(4);
        }        
        private void PrepareDocListBasedOnSpecialization()
        {
            Docs = Hospital.GetAllDocsWithChosenSpec(ChosenSpec);
        }        
        public void AnyDocToTrue(object message)
        {
            anyDoctor = true;
        }
        public void EarliestAppToTrue(object message)
        {
            earliestAppointment = true;
        }

    }
}
