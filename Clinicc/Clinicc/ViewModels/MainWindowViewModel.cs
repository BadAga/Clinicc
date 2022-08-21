using Clinicc.Model;
using Clinicc.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.ViewModels
{
    public class MainWindowViewModel:ViewModelBase
    {
        private NavigationStore _navigation;
        public ViewModelBase CurrentViewModel => _navigation.CurrentViewModel;

        public MainWindowViewModel(NavigationStore navigation)
        {
            this._navigation = navigation;

            this._navigation.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        private void OnCurrentViewModelChanged()
        {
           OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
