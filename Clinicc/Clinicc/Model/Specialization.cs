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
           // name = GetSpecializationName(_id);
        }

        static public void GetSpecializationName()
        {
            String s_name =String.Empty;
            string fileName = @"C:\Users\agnie\source\repos\WPF-projects\Clinicc\Clinicc\DataSource\SpecializationDictionary.txt";

            IEnumerable<string> lines = File.ReadLines(fileName);
            Console.WriteLine(String.Join(Environment.NewLine, lines));

            //return s_name;
        }
    }
}
