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
    public class PatHomeViewModel:ViewModelBase
    {
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

        //commands
        public ICommand OverviewHPCommand { get; }
        public ICommand RecentActivityHPCommand { get; }
        public ICommand BookAppHPCommand { get; }
        public ICommand PatAppointmentsHPCommand { get; }
        public ICommand PatPrescriptionsHPCommand { get; }
        public ICommand ChangePicHPCommand { get; }

        public ICommand LogOutHPCommand { get; }
        public ICommand EditNameHPCommand { get; }
        public ICommand EditSurnameHPCommand { get; }
        public ICommand EditPeselHPCommand { get; }
        public ICommand EditAddressHPCommand { get; }
        public ICommand EditUsernameHPCommand { get; }


        public PatHomeViewModel(Hospital hospital, NavigationStore navigation)
        {
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital); 
        }
    }
}
