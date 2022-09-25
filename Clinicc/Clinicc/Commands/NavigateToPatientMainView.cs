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
    public class NavigateToPatientMainView:CommandBase
    {
        private Hospital _hospital;
        private NavigationStore _navigation;
        private Model.Patient _pat;

        public NavigateToPatientMainView(Hospital hospital, NavigationStore navigation, Model.Patient pat)
        {
            _hospital = hospital;
            _navigation = navigation;
            _pat = pat;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new PatHomeViewModel(_hospital, _navigation, _pat);
        }
    }
}
