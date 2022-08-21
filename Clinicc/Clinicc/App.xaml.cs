using Clinicc.ViewModels;
using Clinicc.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Clinicc
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Hospital _hospital;
        public App()
        {
            _hospital = new Hospital();
            _hospital.AddExistingDoctors();
            _hospital.AddExistingPatients();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_hospital)
            };
            MainWindow.Show();            
            
            base.OnStartup(e);
        }
        
    }
}
