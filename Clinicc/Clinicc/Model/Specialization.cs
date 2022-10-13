using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;

namespace Clinicc.Model
{
    public class Specialization
    {
        public int Id_SPEC { get; set; }
        public string name { get; set; }

        public Specialization(int _id)
        {
            Id_SPEC = _id;
            name = GetSpecializationName(_id);
        }

        public Specialization()
        {
            Id_SPEC = 0;
            name = String.Empty;
        }

        static public string  GetSpecializationName(int spec_id)
        {
            using(var db=new DatabaseEntities())
            {
                var name = (from spec in db.Specializations
                            where spec.Id == spec_id
                            select spec.name).SingleOrDefault();
                return name;
            }
        }

        static public int GetSpecializationId(string spec_name)
        {
            using (var db = new DatabaseEntities())
            {
                var id = (from s in db.Specializations
                             where s.name==spec_name
                             select s.Id).SingleOrDefault();
                return id;
            }
        }
        static public string GetSpecIdFromDB(string pesel_to_check)
        {
            using (var db = new DatabaseEntities())
            {
                var id = (from dr in db.Doctors
                          where dr.PESEL == pesel_to_check
                          select dr.spec_id).SingleOrDefault();
                return id.ToString();
            }
        }

        static public Model.Specialization ConvertDBSpecToModelSpec(Clinicc.Specialization dbspec)
        {
            Model.Specialization spec = new Model.Specialization();
            spec.Id_SPEC = dbspec.Id;
            spec.name = dbspec.name;
            return spec;
        }
        static public List<Specialization> GetSpecsNameList()
        {            
            using (var db = new DatabaseEntities())
            {
                var dbspecializations = (from s in db.Specializations
                             select s);
                List<Specialization> model_specs_list = new List<Specialization>();
                foreach ( var dbs in dbspecializations)
                {
                    model_specs_list.Add(ConvertDBSpecToModelSpec(dbs));
                }
                return model_specs_list;
            }            
        }
    }
}
