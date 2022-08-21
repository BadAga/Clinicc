using Clinicc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.ViewModels
{
    public class MainWindowViewModel:ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainWindowViewModel(Hospital hospital)
        {
            CurrentViewModel = new MainViewModel(hospital);
        }
    }
}
