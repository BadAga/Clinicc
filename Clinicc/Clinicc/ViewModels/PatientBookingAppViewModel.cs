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
        Clinicc.Model.Patient pat { get; set; }

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
                if(SelectedDate != null)
                {
                    IsDateSelected = true;
                    CreateTimeOptions();
                }                
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
                CanBook = true;
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

        private bool isDoctorSelected=true;
        public bool IsDoctorSelected
        {
            get { return isDoctorSelected; }
            set
            {
                isDoctorSelected = value;
                OnPropertyChanged(nameof(IsDoctorSelected));
            }
        }
             
        public bool earliestAppointment = false;

        private bool canBook = false;
        public bool CanBook
        {
            get { return canBook; }
            set { canBook = value;
                  OnPropertyChanged(nameof(CanBook));}
        }
        //done commands
        public ICommand OverviewPatientCommand { get; }
        public ICommand BookAppPatientCommand { get; }
        public ICommand LogOutHPCommand { get; }
        public ICommand PatAppointmentsCommand { get; }
        public RelayCommand EarliestAppointmentCommand { get; private set; }
        public RelayCommand IssueAnAppointment { get; private set; }
        //to do commands:



        public PatientBookingAppViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Patient patParam)
        {            
            pat = patParam;
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewPatientCommand = new NavigateToPatientMainView(hospital, navigation, patParam);
            BookAppPatientCommand = new PatientBookingAppointmentCommand(hospital, navigation, patParam);
            PatAppointmentsCommand = new PatAppointmentsCommand(hospital, navigation, patParam);

            EarliestAppointmentCommand= new RelayCommand(EarliestAppToTrue);
            IssueAnAppointment = new RelayCommand(CreateAppointment);

            //Specs = Model.Specialization.GetSpecsNameList();
            Specs=Hospital.GetAllSpecsWithDoctors();
            StartDate=DateTime.Now;
            EndDate = StartDate.AddMonths(4);
            TimeOptions = new List<DateTime>();
        }        
        private void PrepareDocListBasedOnSpecialization()
        {
            Docs = Hospital.GetAllDocsWithChosenSpec(ChosenSpec);
        }               
        public void EarliestAppToTrue(object message)
        {
            earliestAppointment = true;
        }
        private void CreateTimeOptions()
        {
            if (SelectedDate >= DateTime.Now)
            {
                if (ChosenDoc.schedule.calendars.Count == 0)
                {
                    ChosenDoc.PrepareSchedule();
                    TimeOptions = ChosenDoc.GetAppointmentTimeOptions(SelectedDate);
                }
                else
                {
                    TimeOptions = ChosenDoc.GetAppointmentTimeOptions(SelectedDate);
                }
            }
        }


        public void CreateAppointment(object ob)
        {
            if (!earliestAppointment)
            {
                Clinicc.Appointment new_app = new Appointment();
                Model.Appointment app = new Model.Appointment(0, ChosenTime, SelectedDate, ChosenDoc.Id, pat.Id,this.pat);
                new_app =Converter.ConvertAppointment(app);
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
