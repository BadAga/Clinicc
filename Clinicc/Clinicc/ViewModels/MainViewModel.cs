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
       
        //commands
        public ICommand LogInMPCommand { get; }
        public ICommand ForgotPasswordMPCommand { get; }
        public ICommand SignUpMPCommand { get; }


        public MainViewModel(Hospital hospital )
        {
            LogInMPCommand = new LogInCommand(this,hospital);
        }
    }
}
