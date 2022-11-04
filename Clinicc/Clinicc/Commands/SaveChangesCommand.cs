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
    public class SaveChangesCommand : CommandBase
    {
        private Model.Doctor doc;

        private string _username;

        private string _name;

        private string _surname;

        private Hospital _hospital;

        private NavigationStore _navigation;

        public SaveChangesCommand(Model.Doctor doc, string username, string name, string surname, Hospital hospital, NavigationStore navigation)
        {
            this.doc = doc;
            _username = username;
            _name = name;
            _surname = surname;
            _hospital = hospital;
            _navigation = navigation;
        }

        public override void Execute(object parameter)
        {
            Clinicc.Doctor dbDoctor = new Clinicc.Doctor();
            using (DatabaseEntities db = new DatabaseEntities())
            {
                dbDoctor = (from d in db.Doctors
                            where d.Id == this.doc.Id
                            select d).SingleOrDefault();
                if (_username != dbDoctor.login)
                {
                    dbDoctor.login = _username;
                }
                if(_name!=dbDoctor.name)
                {
                    dbDoctor.name = _name;
                }
                if(dbDoctor.surname != _surname)
                {
                    dbDoctor.surname=_surname;
                }

            }
            using (var db = new DatabaseEntities())
            {
                db.Entry(dbDoctor).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
