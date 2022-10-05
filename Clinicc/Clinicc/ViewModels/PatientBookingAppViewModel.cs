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
        public int _patient_id=0;

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
                IsDateSelected = true;
                TRYOUTCreateTime();
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

        private List<DateTime> _time_options;
        public List<DateTime> TimeOptions
        {
            get { return _time_options; }
            set { _time_options = value; 
                OnPropertyChanged(nameof(TimeOptions));                
            }
        }

        private DateTime _chosen_time;
        public DateTime ChosenTime 
        { 
            get { return _chosen_time; }
            set { _chosen_time = value;              

                OnPropertyChanged(nameof(_chosen_time));}
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
        private bool _is_Date_selected = false;
        public bool IsDateSelected
        {
            get
            {
                return _is_Date_selected;
            }
            set
            {
                _is_Date_selected = value;
                OnPropertyChanged(nameof(IsDateSelected));
            }
        }

        public bool IsDoctorSelected = false;

        public bool anyDoctor = false;
             
        public bool earliestAppointment = false;

        public bool CanBook = true;
        //done commands
        public ICommand OverviewPatientCommand { get; }
        public ICommand BookAppPatientCommand { get; }
        public ICommand LogOutHPCommand { get; }
        public ICommand PatAppointmentsCommand { get; }
        public RelayCommand ChooseAnyDoctorCommand { get; private set; }
        public RelayCommand EarliestAppointmentCommand { get; private set; }
        public RelayCommand IssueAnAppointment { get; private set; }
        //to do commands:



        public PatientBookingAppViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Patient pat)
        {
            _patient_id = pat.Id;

            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewPatientCommand = new NavigateToPatientMainView(hospital, navigation, pat);
            BookAppPatientCommand = new PatientBookingAppointmentCommand(hospital, navigation, pat);
            PatAppointmentsCommand = new PatAppointmentsCommand(hospital, navigation, pat);

            ChooseAnyDoctorCommand = new RelayCommand(AnyDocToTrue);
            EarliestAppointmentCommand= new RelayCommand(EarliestAppToTrue);
            IssueAnAppointment = new RelayCommand(CreateAppointment);

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
        private void TRYOUTCreateTime()
        {
            List<DateTime> list = new List<DateTime>();
            TimeOptions = list;
            DateTime time = new DateTime();
            time = SelectedDate.AddHours(8);
            for(int i=0; i<=7; i++)
            {               
                time = time.AddMinutes(30);
                TimeOptions.Add(time);
            }  
        }


        public void CreateAppointment(object ob)
        {
            if (!earliestAppointment && !anyDoctor)
            {
                Clinicc.Appointment new_app = new Appointment();
                Model.Appointment app = new Model.Appointment(0, ChosenTime, SelectedDate, ChosenDoc.Id, _patient_id);
                new_app = Model.Appointment.ConvertModelAppointmentToDBAppointment(app);
                new_app.Id_schedule = ChosenDoc.Id;
                using(var db = new DatabaseEntities())
                {
                    db.Appointments.Add(new_app);
                    db.SaveChanges();
                }
            }
        }
    }
}
