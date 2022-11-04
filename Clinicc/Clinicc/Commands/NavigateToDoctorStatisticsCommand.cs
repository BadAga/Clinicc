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
    public class NavigateToDoctorStatisticsCommand : CommandBase
    {
        private Hospital _hospital;
        private NavigationStore _navigation;
        private Clinicc.Model.Doctor _doc;

        public NavigateToDoctorStatisticsCommand(Hospital hospital, NavigationStore navigation, Model.Doctor doc)
        {
            _hospital = hospital;
            _navigation = navigation;
            _doc = doc;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new DoctorStatisticsViewModel(_hospital, _navigation, _doc);
        }
    }
}
