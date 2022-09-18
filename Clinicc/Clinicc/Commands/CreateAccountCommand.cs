using Clinicc.Model;
using Clinicc.Stores;
using Clinicc.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinicc.Commands
{
    public class CreateAccountCommand : CommandBase
    {
        private Hospital _hospital;
        private NavigationStore _navigation;
        private SignUpViewModel _signUpViewModel;

        public CreateAccountCommand(Hospital hospital, NavigationStore navigation,
                                    SignUpViewModel signUpViewModel)
        {
            _hospital = hospital;
            _navigation = navigation;
            _signUpViewModel = signUpViewModel;

            _signUpViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private bool CheckAllInputs()
        {
            bool answer = CheckInputName();
            bool answer2 = CheckInputSurname();
            bool answer3= CheckInputPesel();
            return answer && answer2&&answer3;
        }
            
        private bool CheckInputName()
        {            
            if (!InputValidator.InputOnlyLetters(_signUpViewModel.NameSUP))
            {
                _signUpViewModel.NameMessage = "Invalid input: only letters";
                return false;
            }
            return true;
        }
        private bool CheckInputSurname()
        {
            if(! InputValidator.InputOnlyLetters(_signUpViewModel.SurnameSUP))
            {
                _signUpViewModel.SurnameMessage = "Invalid input: only letters";
                return false;
            }
            return true;
        }
        private bool CheckInputPesel()
        {
            if (!InputValidator.InputOnlyNumbers(_signUpViewModel.PeselSUP))
            {
                _signUpViewModel.PeselMessage = "Invalid input: only nuumbers";
                return false;
            }
            if(_signUpViewModel.PeselSUP.Length!=11)
            {
                _signUpViewModel.PeselMessage = "Invalid input: invalid length";
                return false;
            }
            return true;           
            //to do: acctually check
        }

        public override bool CanExecute(object parameter)
        {
            return  !string.IsNullOrEmpty(_signUpViewModel.UsernameSUP)&&
                    !string.IsNullOrEmpty(_signUpViewModel.PasswordSUP) &&
                    !string.IsNullOrEmpty(_signUpViewModel.NameSUP) &&
                    !string.IsNullOrEmpty(_signUpViewModel.SurnameSUP) &&
                    !string.IsNullOrEmpty(_signUpViewModel.PeselSUP) &&
                    base.CanExecute(parameter);
        }
        public override void Execute(object parameter)
        {            
            _signUpViewModel.TrimInputs();
            if (CheckAllInputs())
            {
                if (User.CheckIfDoctor(_signUpViewModel.PeselSUP))
                {
                    Clinicc.Model.Doctor new_doc = new Clinicc.Model.Doctor(_signUpViewModel.NameSUP,
                                               _signUpViewModel.SurnameSUP,
                                               _signUpViewModel.PeselSUP,
                                               _signUpViewModel.UsernameSUP,
                                               _signUpViewModel.PasswordSUP);
                    new_doc.SetSpecializationFromDictionary();
                    if (_hospital.AddDoctor(new_doc))
                    {       
                      _navigation.CurrentViewModel = new DocHomeViewModel(_hospital, _navigation,new_doc);
                    }

                }
                else
                {
                    Clinicc.Model.Patient new_pat = new Clinicc.Model.Patient(_signUpViewModel.NameSUP,
                                               _signUpViewModel.SurnameSUP,
                                               _signUpViewModel.PeselSUP,
                                               _signUpViewModel.UsernameSUP,
                                               _signUpViewModel.PasswordSUP);
                    if (_hospital.AddPatient(new_pat))
                    {                       
                      _navigation.CurrentViewModel = new PatHomeViewModel(_hospital, _navigation,new_pat);
                    }
                }
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SignUpViewModel.UsernameSUP) ||
                e.PropertyName == nameof(SignUpViewModel.PasswordSUP) ||
                e.PropertyName == nameof(SignUpViewModel.NameSUP) ||
                e.PropertyName == nameof(SignUpViewModel.SurnameSUP) ||
                e.PropertyName == nameof(SignUpViewModel.PeselSUP))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
