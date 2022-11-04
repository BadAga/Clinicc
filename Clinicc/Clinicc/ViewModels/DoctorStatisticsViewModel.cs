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

        private int requestsWeekly = 0;
        public int RequestsWeekly
        {
            get { return requestsWeekly; }
            set { requestsWeekly = value; 
                  OnPropertyChanged(nameof(RequestsWeekly));}
        }

        private int requestsMonthly = 1;
        public int RequestsMonthly
        {
            get { return requestsMonthly; }
            set
            {
                requestsMonthly = value;
                OnPropertyChanged(nameof(RequestsMonthly));
            }
        }

        private int requestsYearly = 2;
        public int RequestsYearly
        {
            get { return requestsYearly; }
            set
            {
                requestsYearly = value;
                OnPropertyChanged(nameof(RequestsYearly));
            }
        }

        private int increaseWeekly = 3;
        public int IncreaseWeekly
        {
            get { return increaseWeekly; }
            set { increaseWeekly = value;
                OnPropertyChanged(nameof(IncreaseWeekly));
            }
        }

        private int averageWeekly = 4;
        public int AverageWeekly
        {
            get { return averageWeekly; }
            set
            {
                averageWeekly = value;
                OnPropertyChanged(nameof(AverageWeekly));
            }
        }

        private int averageMonthly = 5;
        public int AverageMonthly
        {
            get { return averageMonthly; }
            set
            {
                averageMonthly = value;
                OnPropertyChanged(nameof(AverageMonthly));
            }
        }

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

            List<int> statistics = doc.GetStatistics();
            RequestsWeekly = statistics[0];

           
            IncreaseWeekly=statistics[5];
        }
    }
}
