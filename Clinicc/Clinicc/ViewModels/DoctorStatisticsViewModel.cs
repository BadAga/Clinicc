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
    public class DoctorStatisticsViewModel:ViewModelBase
    {
        Model.Doctor myDoctor { get; set; }
        //commands
        public ICommand LogOutHPCommand { get; }

        public ICommand OverviewDoctorCommand { get; }

        public ICommand MyScheduleDoctorCommand { get; }

        public ICommand AppointmentRequestsCommand { get; }

        public ICommand StatisticsCommand { get; }

        public DoctorStatisticsViewModel(Hospital hospital, NavigationStore navigation, Clinicc.Model.Doctor doc)
        {
            myDoctor = doc;
            doc.PrepareSchedule();
            LogOutHPCommand = new NavigateToMainViewCommand(navigation, hospital);
            OverviewDoctorCommand = new NavigateToDoctorMainView(hospital, navigation, doc);
            MyScheduleDoctorCommand = new NavigateToMyScheduleDoctorCommand(hospital, navigation, doc);
            AppointmentRequestsCommand = new NavigateToAppointmentRequestDoctorCommand(hospital, navigation, doc);
            StatisticsCommand=new NavigateToDoctorStatisticsCommand(hospital, navigation, doc);
        }
    }
}
