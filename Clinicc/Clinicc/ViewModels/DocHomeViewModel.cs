using Clinicc.Commands;
using Clinicc.Model;
using Clinicc.Stores;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Clinicc.ViewModels
{
    public class DocHomeViewModel: ViewModelBase
    {
        public Model.Doctor MyDoctor { get; set; }
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

        private String profilePictureSource = "/Images/user.png";
        public String ProfilePictureSource
        {
            get { return profilePictureSource; }
            set
            {
                profilePictureSource = value;
                OnPropertyChanged(nameof(ProfilePictureSource));
            }
        }

        private String _editPicture = "/Images/edit.png";
        public String EditPicture
        {
            get { return _editPicture; }
            set
            {
                _editPicture = value;
                OnPropertyChanged(nameof(EditPicture));
            }
        }
        private bool _canEdit = false;
        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; OnPropertyChanged(nameof(CanEdit)); }
        }


        //commands
        public ICommand LogOutHPCommand { get; }
        public ICommand OverviewDoctorCommand { get; }
        public ICommand MyScheduleDoctorCommand { get; }
        public ICommand AppointmentRequestsCommand { get; }
        public RelayCommand ChangeProfilePicCommand { get; }
        public ICommand StatisticsCommand { get; }
        public RelayCommand SaveChangesCommand { get; }
        public RelayCommand ChangeEditProperties { get; }

        public DocHomeViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Doctor doc)
        {
            MyDoctor = doc;
            this.hospital = hospital;
            UsernameHP = MyDoctor.login;
            NameHP = MyDoctor.name;
            SurnameHP = MyDoctor.surname;
            PeselHP = MyDoctor.PESEL;

            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewDoctorCommand = new NavigateToDoctorMainView(hospital, navigation, doc);
            MyScheduleDoctorCommand=new NavigateToMyScheduleDoctorCommand(hospital, navigation, doc);
            AppointmentRequestsCommand = new NavigateToAppointmentRequestDoctorCommand(hospital, navigation, doc);
            StatisticsCommand = new NavigateToDoctorStatisticsCommand(hospital, navigation, doc);
            SaveChangesCommand = new RelayCommand(SaveChanges);
            ChangeProfilePicCommand = new RelayCommand(LoadProfilePicture);
            ChangeEditProperties = new RelayCommand(ChangeEditState);
        }

        private void SaveChanges(object o)
        {
            Clinicc.Doctor dbDoctor = new Clinicc.Doctor();
            using (DatabaseEntities db = new DatabaseEntities())
            {
                dbDoctor = (from d in db.Doctors
                            where d.Id == this.MyDoctor.Id
                            select d).SingleOrDefault();
                if (_username != dbDoctor.login)
                {
                    if (hospital.NoLoginReapeating(UsernameHP))
                    {
                        dbDoctor.login = UsernameHP;
                        MyDoctor.login = UsernameHP;
                    }
                }

            }
            using (var db = new DatabaseEntities())
            {
                db.Entry(dbDoctor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
        private void LoadProfilePicture(object o)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.DefaultExt = (".png");
            open.Filter = "Pictures (*.jpg;*.gif;*.png)|*.jpg;*.gif;*.png";

            if (open.ShowDialog() == true)
            {
                ProfilePictureSource = open.FileName;
            }

        }

        private void ChangeEditState(object o)
        {
            if(CanEdit)
            {
                CanEdit = false;
                //edit icon brighter
                EditPicture = "/Images/edit.png";                
            }
            else
            {
                CanEdit = true;
                //edit icon faded
                EditPicture = "/Images/edit_faded.png";
            }
        }
    }    
}
