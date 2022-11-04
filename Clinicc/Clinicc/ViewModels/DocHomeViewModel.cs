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
    public class DocHomeViewModel: ViewModelBase
    {
        public Model.Doctor myDoctor { get; set; }

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

        //commands
        public ICommand LogOutHPCommand { get; }
        public ICommand OverviewDoctorCommand { get; }
        public ICommand MyScheduleDoctorCommand { get; }
        public ICommand AppointmentRequestsCommand { get; }
        public RelayCommand ChangeProfilePicCommand { get; }
        public ICommand StatisticsCommand { get; }
        public ICommand SaveChangesCommand { get; }


        public DocHomeViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Doctor doc)
        {
            myDoctor = doc;

            UsernameHP = myDoctor.login;
            NameHP = myDoctor.name;
            SurnameHP = myDoctor.surname;
            PeselHP = myDoctor.PESEL;

            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewDoctorCommand = new NavigateToDoctorMainView(hospital, navigation, doc);
            MyScheduleDoctorCommand=new NavigateToMyScheduleDoctorCommand(hospital, navigation, doc);
            AppointmentRequestsCommand = new NavigateToAppointmentRequestDoctorCommand(hospital, navigation, doc);
            StatisticsCommand = new NavigateToDoctorStatisticsCommand(hospital, navigation, doc);
            SaveChangesCommand = new SaveChangesCommand(myDoctor,_username, _name, _surname,hospital, navigation);
            ChangeProfilePicCommand = new RelayCommand(LoadProfilePicture);
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
    }    
}
