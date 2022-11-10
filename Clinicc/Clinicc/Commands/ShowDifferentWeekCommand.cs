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
    public class ShowDifferentWeekCommand:CommandBase
    {
        private Hospital _hospital;
        private NavigationStore _navigation;
        private Clinicc.Model.Doctor _doc;
        private int week;

        public ShowDifferentWeekCommand(Hospital hospital, NavigationStore navigation, Model.Doctor doc, int week)
        {
            _hospital = hospital;
            _navigation = navigation;
            _doc = doc;
            this.week = week;
        }

        public override void Execute(object parameter)
        {
            _navigation.CurrentViewModel = new DoctorScheduleViewModel(_hospital, _navigation, _doc,week);
        }
    }
}
