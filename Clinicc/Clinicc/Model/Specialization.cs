using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        static public string  GetSpecializationName(int spec_id)
        {
            string fileName = @"C:\Users\agnie\source\repos\C#\TryOut\TryOut\Spec.txt";

            IEnumerable<string> lines = File.ReadLines(fileName);
            //Console.WriteLine(String.Join(Environment.NewLine, lines));
            string spec_name = String.Empty;
            string spec_code = String.Empty;
            Dictionary<int, string> specializations = new Dictionary<int, string>();
            foreach (string line in lines)
            {
                var words = line.Split(' ');
                spec_name = words[0];
                spec_code = words[1];
                specializations.Add(int.Parse(spec_code), spec_name);
                spec_name = String.Empty;
                spec_code = String.Empty;
            }
            string wanted_spec = String.Empty;
            if (specializations.TryGetValue(spec_id, out wanted_spec))
            {
                return wanted_spec;
            }
            else
            {
                return String.Empty;
                //to do:wrong spec dictionary input
            }
        }
    }
}
