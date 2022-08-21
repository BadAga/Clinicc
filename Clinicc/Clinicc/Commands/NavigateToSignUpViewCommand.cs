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
    public class NavigateToSignUpViewCommand : CommandBase
    {
        private Hospital _hospital;
        private NavigationStore _navigation;

        public NavigateToSignUpViewCommand(NavigationStore navigation, Hospital hospital)
        {
            _navigation = navigation;
            _hospital = hospital;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new SignUpViewModel(_hospital,_navigation);
        }
    }
}
