using Clinicc.Commands;
using Clinicc.Model;
using Clinicc.Stores;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Clinicc.ViewModels
{
    public class PatHomeViewModel:ViewModelBase
    {
        private Clinicc.Model.Patient patient;
        private Hospital hospital;
        private string _username;
        public string UsernameHP
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UsernameHP));
            }
        }

        private string _name;
        public string NameHP
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(NameHP));
            }
        }

        private string _surname;
        public string SurnameHP
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(SurnameHP));
            }
        }

        private string _pesel;
        public string PeselHP
        {
            get
            {
                return _pesel;
            }
            set
            {
                _pesel = value;
                OnPropertyChanged(nameof(PeselHP));
            }
        }

        private string _address;
        public string AddressHP
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(AddressHP));
            }
        }

        private string profilePictureSource="/Images/user.png";
        public string ProfilePictureSourceHP
        {
            get { return profilePictureSource; }
            set
            {
                profilePictureSource = value;
                OnPropertyChanged(nameof(ProfilePictureSourceHP));
            }
        }

        private bool _canEditAddress = false;
        public bool CanEditAddress
        {
            get { return _canEditAddress; }
            set
            {
                _canEditAddress = value;
                OnPropertyChanged(nameof(CanEditAddress));
            }
        }

        private bool _canEditUsername = false;
        public bool CanEditUsername
        {
            get { return _canEditUsername; }
            set
            {
                _canEditUsername = value;
                OnPropertyChanged(nameof(CanEditUsername));
            }
        }

        private string editPictureAddress= "/Images/edit.png";
        public string EditPictureAddress
        {
            get { return editPictureAddress; }
            set
            {
                editPictureAddress = value;
                OnPropertyChanged(nameof(EditPictureAddress));
            }
        }

        private string editPictureUsername = "/Images/edit.png";
        public string EditPictureUsername
        {
            get { return editPictureUsername; }
            set
            {
                editPictureUsername = value;
                OnPropertyChanged(nameof(EditPictureUsername));
            }
        }
        //done commands
        public ICommand OverviewPatientCommand { get; }        
        public ICommand BookAppPatientCommand { get; }
        public ICommand LogOutHPCommand { get; }
        public ICommand PatAppointmentsCommand { get; }
        public RelayCommand ChangeProfilePicCommand { get; }
        public RelayCommand EditAddressHPCommand { get; }
        public RelayCommand EditUsernameHPCommand { get; }

        //to do commands:
        public ICommand RecentActivityCommand { get; }        
        public ICommand PatPrescriptionsCommand { get; }     
        public ICommand SaveChangesCommand { get; }

        public PatHomeViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Patient pat)
        {
            patient = pat;
            this.hospital = hospital;
            UsernameHP = pat.login;
            NameHP= pat.name;
            SurnameHP = pat.surname;
            PeselHP = pat.PESEL;
            AddressHP = pat.adress;

            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewPatientCommand=new NavigateToPatientMainView(hospital, navigation, pat);
            BookAppPatientCommand = new PatientBookingAppointmentCommand(hospital, navigation, pat);
            PatAppointmentsCommand = new PatAppointmentsCommand(hospital, navigation, pat);
            ChangeProfilePicCommand = new RelayCommand(LoadProfilePicture);
            SaveChangesCommand = new RelayCommand(SaveChanges);
            EditAddressHPCommand = new RelayCommand(ChangeEditAddressState);
            EditUsernameHPCommand = new RelayCommand(ChangeEditUsernameState);
        }

        private void ChangeEditAddressState(Object o)
        {
            if (CanEditAddress)
            {
                CanEditAddress = false;
                //edit icon brighter
                EditPictureAddress = "/Images/edit.png";
            }
            else
            {
                CanEditAddress = true;
                //edit icon faded
                EditPictureAddress = "/Images/edit_faded.png";
            }
        }
        private void ChangeEditUsernameState(Object o)
        {
            if (CanEditUsername)
            {
                CanEditUsername = false;
                //edit icon brighter
                EditPictureUsername = "/Images/edit.png";
            }
            else
            {
                CanEditUsername = true;
                //edit icon faded
                EditPictureUsername = "/Images/edit_faded.png";
            }
        }
        private void SaveChanges(object o)
        {
            Clinicc.Patient dbPatient = new Clinicc.Patient();
            using (DatabaseEntities db = new DatabaseEntities())
            {
                dbPatient = (from p in db.Patients
                             where p.Id == this.patient.Id
                             select p).SingleOrDefault();
                //checkingg hat have been modified
                if (dbPatient != null)
                {
                    if (dbPatient.login != UsernameHP)
                    {
                        if(hospital.NoLoginReapeating(UsernameHP))
                        {
                            dbPatient.login = UsernameHP;
                            patient.login = UsernameHP;
                        }
                    }
                    if (dbPatient.adress != this.AddressHP)
                    {
                        dbPatient.adress = AddressHP;
                        patient.adress = this.AddressHP;
                    }
                }
            }
            using (var db = new DatabaseEntities())
            {
                db.Entry(dbPatient).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void LoadProfilePicture(object o)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
                ProfilePictureSourceHP = open.FileName;
        }
    }
}
