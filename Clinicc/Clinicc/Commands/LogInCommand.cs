using Clinicc.Model;
using Clinicc.Stores;
using Clinicc.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Commands
{
    public class LogInCommand : CommandBase
    {
        private Hospital _hospital;

        private MainViewModel _MainViewModel;

        private NavigationStore _navigation;

        public LogInCommand(MainViewModel mainViewModel, Hospital hospital,NavigationStore navigation)
        {
            this._hospital = hospital;
            this._MainViewModel = mainViewModel;
            this._navigation = navigation;

            _MainViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        private void HandleMessages(int code)
        {
            if(code == 0)
            {
                _MainViewModel.LoginMessage = "User not found";
            }
            else if(code==2)
            {
                _MainViewModel.LoginMessage = "Wrong password";
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_MainViewModel.UsernameMP) &&
                    !string.IsNullOrEmpty(_MainViewModel.PasswordMP) &&                    
                    base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {
            //1-successfull 2-wrong password 0-no user found 
            int answer = _hospital.LogIn(_MainViewModel.UsernameMP, _MainViewModel.PasswordMP);
            if (answer==1)
            {
                _navigation.CurrentViewModel = new PatHomeViewModel(_hospital,_navigation);
            }
            else
            {
                HandleMessages(answer);
                //well massages need to be handled
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.UsernameMP) ||
                e.PropertyName == nameof(MainViewModel.PasswordMP))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
