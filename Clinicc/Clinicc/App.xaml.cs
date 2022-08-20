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
        protected override void OnStartup(StartupEventArgs e)
        {
            
            Clinicc.Model.Hospital users = new Clinicc.Model.Hospital();
            users.AddExistingDoctors();
            users.AddExistingPatients();
            base.OnStartup(e);
        }
        
    }
}
