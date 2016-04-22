using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    class Courses
    {
        private String Name, Code;
        private double Cost;

        public Courses(String Name, String Code, double Cost)
        {
            this.Name = Name;
            this.Code = Code;
            this.Cost = Cost;
        }

        public String getName() { return Name; }
        public String getCode() { return Code; }
        public double getCost() { return Cost; }
    }
}
