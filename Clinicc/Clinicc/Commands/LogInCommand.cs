using Clinicc.Model;
using Clinicc.Stores;
using Clinicc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Commands
{
    public class LogInCommand : CommandBase
    {
        private Hospital hospital;

        private MainViewModel _MainViewModel;

        private NavigationStore _navigation;

        public LogInCommand(MainViewModel mainViewModel, Hospital hospital,NavigationStore navigation)
        {
            this.hospital = hospital;
            this._MainViewModel = mainViewModel;
            this._navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            if (hospital.LogIn(_MainViewModel.UsernameMP, _MainViewModel.PasswordMP))
            {
                _navigation.CurrentViewModel = new PatHomeViewModel();
            }
            else
            {
                //well massages need to be handled
            }
        }
    }
}
