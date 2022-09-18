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

        private User _user;

        public LogInCommand(MainViewModel mainViewModel, Hospital hospital,NavigationStore navigation)
        {
            this._hospital = hospital;
            this._MainViewModel = mainViewModel;
            this._navigation = navigation;
            this._user=new User();
            _MainViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        private void HandleMessages(int code)
        {
            if(code == 0)
            {
                _MainViewModel.LoginMessage = "User not found";
            }
            else if(code==-1)
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
            //1-sucessfull -1-wrong password 0-no user found             
            KeyValuePair<int, User> answer = _hospital.LogIn(_MainViewModel.UsernameMP, _MainViewModel.PasswordMP);
            if (answer.Key==1)
            {
                if(answer.Value.code=="PAT")
                {
                    Clinicc.Model.Patient pat = (Model.Patient)answer.Value;
                    _navigation.CurrentViewModel = new PatHomeViewModel(_hospital,_navigation,pat);
                }
                else if(answer.Value.code == "DOC")
                {
                    Clinicc.Model.Doctor doc = (Model.Doctor)answer.Value;
                    _navigation.CurrentViewModel = new DocHomeViewModel(_hospital, _navigation,doc);
                }
            }
            else
            {
                HandleMessages(answer.Key);
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
