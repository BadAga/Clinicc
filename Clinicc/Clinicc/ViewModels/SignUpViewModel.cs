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
    public class SignUpViewModel:ViewModelBase
    {
        //inputs
        private string _username;
        public string UsernameSUP
        {
            get 
            { 
                return _username; 
            }
            set 
            { 
                _username = value;
                OnPropertyChanged(nameof(UsernameSUP));
            }
        }

        private string _password;
        public string PasswordSUP
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(PasswordSUP));
            }
        }

        private string _name;
        public string NameSUP
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(NameSUP));
            }
        }

        private string _surname;
        public string SurnameSUP
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(SurnameSUP));
            }
        }

        private string _pesel;
        public string PeselSUP
        {
            get
            {
                return _pesel;
            }
            set
            {
                _pesel = value;
                OnPropertyChanged(nameof(PeselSUP));
            }
        }

        //messages
        private string _username_message;
        public string UsernameMessage
        {
            get
            {
                return _username_message;
            }
            set
            {
                _username_message = value;
                OnPropertyChanged(nameof(UsernameMessage));
            }
        }

        private string _password_message;
        public string PasswordMessage
        {
            get
            {
                return _password_message;
            }
            set
            {
                _password_message = value;
                OnPropertyChanged(nameof(PasswordMessage));
            }
        }

        private string _name_message;
        public string NameMessage
        {
            get
            {
                return _name_message;
            }
            set
            {
                _name_message = value;
                OnPropertyChanged(nameof(NameMessage));
            }
        }

        private string _surname_message;
        public string SurnameMessage
        {
            get
            {
                return _surname_message;
            }
            set
            {
                _surname_message = value;
                OnPropertyChanged(nameof(SurnameMessage));
            }
        }

        private string _pesel_message;
        public string PeselMessage
        {
            get
            {
                return _pesel_message;
            }
            set
            {
                _pesel_message = value;
                OnPropertyChanged(nameof(PeselMessage));
            }
        }

  

        public void TrimInputs()
        {
            _username = _username.Trim();
            _password = _password.Trim();
            _name= _name.Trim();
            _surname= _surname.Trim();
            _pesel= _pesel.Trim();
        }
        //commands
        public ICommand CreateAccountSUPCommand { get; }
        public ICommand LogInSUPCommand { get; }
        


        public SignUpViewModel(Hospital hospital, NavigationStore navigation)
        {
            CreateAccountSUPCommand = new CreateAccountCommand(hospital,navigation,this);
            LogInSUPCommand=new NavigateToMainViewCommand(navigation,hospital);
        }
    }
}
