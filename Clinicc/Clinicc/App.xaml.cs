using Clinicc.ViewModels;
using Clinicc.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Clinicc.Stores;

namespace Clinicc
{
    /// <summary>
    /// Logika interakcji dla klasy App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Hospital _hospital;
        private NavigationStore _navigation;
        public App()
        {
            _hospital = new Hospital();            
            _navigation = new NavigationStore();           
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _navigation.CurrentViewModel = new MainViewModel(_hospital,_navigation);

            MainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(_navigation)
            };
            MainWindow.Show();            
            
            base.OnStartup(e);
        }
        
    }
}
