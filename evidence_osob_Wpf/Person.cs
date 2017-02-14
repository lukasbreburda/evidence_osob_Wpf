using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace evidence_osob_Wpf
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string jmeno { get; set; }
        public string prijmeni { get; set; }
        public string r_num { get; set; }
        public string r_num2 { get; set; }
        public int pohlavi { get; set; }
        
        public Person()
        {
        }

        public override string ToString()
        {
            return jmeno + " " + prijmeni + " ";
        }
    }
}
