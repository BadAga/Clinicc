using Clinicc.Model;
using Clinicc.Stores;
using Clinicc.ViewModels;
using System;
using System.Collections.Generic;
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
        }

        public override void Execute(object parameter)
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
                   // new_doc.SaveInDatabase();
                    _navigation.CurrentViewModel = new PatHomeViewModel(_hospital, _navigation);
                }

            }
            else
            {
                Clinicc.Model.Patient new_pat=new Clinicc.Model.Patient(_signUpViewModel.NameSUP,
                                           _signUpViewModel.SurnameSUP,
                                           _signUpViewModel.PeselSUP,
                                           _signUpViewModel.UsernameSUP,
                                           _signUpViewModel.PasswordSUP);
                if (_hospital.AddPatient(new_pat))
                {
                    new_pat.SaveInDatabase();
                    _navigation.CurrentViewModel = new PatHomeViewModel(_hospital, _navigation);
                }

            }
        }
    }
}
