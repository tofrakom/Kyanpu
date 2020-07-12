using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kyanpu
{
    public class Person
    {
        //Overall Properties
        public string Name { get; set; }
        public string Parent1 { get; set; }
        public string Parent2 { get; set; }
        public DateTime Birthday { get; set; }
        public string Birthplace { get; set; }

        //Important Properties
        public string Insurance { get; set; }
        public int Number { get; set; }

        //Activity Properties
        public int Participation { get; set; }
        public int CanSwim { get; set; }
        public int PermSwim { get; set; }
        public int Riding { get; set; }

        //Health Properties
        public string Diseases { get; set; }
        public string Medication { get; set; }
        public string Allergies { get; set; }

        //SQL-Only Properties
        public int ID { get; set; }

        public Person(string name, string parent1, string parent2, DateTime birthday, string birthplace, string insurance, int number, int participation, int canSwim, int permSwim, int riding, string diseases, string medication, string allergies, int id)
        {
            ID = id;
            Name = name;
            Parent1 = parent1;
            Parent2 = parent2;
            Birthday = birthday;
            Birthplace = birthplace;
            Insurance = insurance;
            Number = number;
            Participation = participation;
            CanSwim = canSwim;
            PermSwim = permSwim;
            Riding = riding;
            Diseases = diseases;
            Medication = medication;
            Allergies = allergies;
        }
    }
}
