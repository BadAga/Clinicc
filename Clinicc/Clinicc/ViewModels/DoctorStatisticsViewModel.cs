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

        private String requestsWeekly;
        public String RequestsWeekly
        {
            get { return requestsWeekly; }
            set { requestsWeekly = value; 
                  OnPropertyChanged(nameof(RequestsWeekly));}
        }

        private String requestsMonthly ;
        public String RequestsMonthly
        {
            get { return requestsMonthly; }
            set
            {
                requestsMonthly = value;
                OnPropertyChanged(nameof(RequestsMonthly));
            }
        }

        private String requestsYearly ;
        public String RequestsYearly
        {
            get { return requestsYearly; }
            set
            {
                requestsYearly = value;
                OnPropertyChanged(nameof(RequestsYearly));
            }
        }

        private String increaseWeekly ;
        public String IncreaseWeekly
        {
            get { return increaseWeekly; }
            set { increaseWeekly = value;
                OnPropertyChanged(nameof(IncreaseWeekly));
            }
        }

        private String averageWeekly ;
        public String AverageWeekly
        {
            get { return averageWeekly; }
            set
            {
                averageWeekly = value;
                OnPropertyChanged(nameof(AverageWeekly));
            }
        }

        private String averageMonthly ;
        public String AverageMonthly
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

            List<string> statistics = doc.GetStatistics();
            RequestsWeekly = statistics[0];
            RequestsMonthly = statistics[1];
            RequestsYearly= statistics[2];
            AverageWeekly = statistics[3];
            averageMonthly = statistics[4];
            IncreaseWeekly =statistics[5];
        }
    }
}
