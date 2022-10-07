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
    public class DoctorScheduleViewModel:ViewModelBase
    {
        public Model.Doctor Doctor { get; set; }
        public ICommand LogOutHPCommand { get; }

        public ICommand OverviewDoctorCommand { get; }

        public ICommand MyScheduleDoctorCommand { get; }

        public ICommand AppointmentRequestsCommand { get; }

        public DoctorScheduleViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Doctor doc)
        {
            Doctor = doc;

            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewDoctorCommand = new NavigateToDoctorMainView(hospital, navigation, doc);
            MyScheduleDoctorCommand = new MyScheduleDoctorCommand(hospital, navigation, doc);
            AppointmentRequestsCommand = new AppointmentRequestDoctorCommand(hospital, navigation, doc);
            
        }
    }
}
