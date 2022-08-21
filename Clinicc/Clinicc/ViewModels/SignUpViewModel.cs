using Clinicc.Commands;
using Clinicc.Model;
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


        //commands
        public ICommand CreateAccountSUPCommand { get; }
        public ICommand LogInSUPCommand { get; }
        public ICommand LogOutSUPCommand { get; }


        public SignUpViewModel()
        {
            CreateAccountSUPCommand = new CreateAccountCommand();
        }
    }
}
