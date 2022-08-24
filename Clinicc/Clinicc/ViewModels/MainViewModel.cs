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
    public class MainViewModel:ViewModelBase
    {
        private string _username;
        public string UsernameMP
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(UsernameMP));
            }
        }

        private string _password;
        public string PasswordMP
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(PasswordMP));
            }
        }

        private string _login_message;
        public string LoginMessage
        {
            get
            {
                return _login_message;
            }
            set
            {
                _login_message = value;
                OnPropertyChanged(nameof(LoginMessage));
            }
        }

        //commands
        public ICommand LogInMPCommand { get; }
        public ICommand ForgotPasswordMPCommand { get; }
        public ICommand SignUpMPCommand { get; }


        public MainViewModel(Hospital hospital, NavigationStore navigation )
        {
            LogInMPCommand = new LogInCommand(this,hospital,navigation);
            SignUpMPCommand = new NavigateToSignUpViewCommand(navigation,hospital);
        }
    }
}
