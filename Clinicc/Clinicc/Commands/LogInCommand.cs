using Clinicc.Model;
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

        public LogInCommand(MainViewModel mainViewModel, Hospital hospital)
        {
            this.hospital = hospital;
            this._MainViewModel = mainViewModel;
        }

        public override void Execute(object parameter)
        {
            if (hospital.LogIn(_MainViewModel.UsernameMP, _MainViewModel.PasswordMP))
            {
                //show homepage
            }
            else
            {
                //well massages need to be handled
            }
        }
    }
}
