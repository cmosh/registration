using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    class Payments
    {
        private  Dictionary<string, double> paid;

        private Dictionary<string, double> unpaid;

        //intialise list of paid and unpaid courses
        public Payments()
        {
            paid = new Dictionary<string, double>();
            unpaid = new Dictionary<string, double>();
        }

        //register course and bill student
       public void bill(string course,double amount)
        {
            unpaid[course] = amount;

        }

        //pay for registered course
        public void pay(string course, double amount)
        {
            unpaid.Remove(course);
            paid[course] = amount;

        }
        //get all paid for courses
        public String getPaid()
        {
            String courses = "";
            foreach (KeyValuePair<string, double> entry in paid)
            {
                courses += entry.Key + ",";

            }
            return courses.TrimEnd(','); 
        }

        //get total outstanding fees
        public double getTotal()
        {
            double courses = 0;
            foreach (KeyValuePair<string, double> entry in unpaid)
            {
                courses += entry.Value;

            }
            return courses;
        }
        //get all courses that are registered but unpaid for
        public String getUnpaid()
        {
            String courses = "";
            foreach (KeyValuePair<string, double> entry in unpaid)
            {
                courses += entry.Key + ",";

            }
            return courses.TrimEnd(',');
        }

        //Check which courses are not paid for
        public String[] CheckUnpaid()
        {
            String [] courses = new String[unpaid.Count];
            int index = 0;
            foreach (KeyValuePair<string, double> entry in unpaid)
            {
                courses [index]= entry.Key;
                index++;

            }

            return courses;

        }
        //Check cost of course
        public double Cost(String name)
        {
            return unpaid[name];
        }
        //Check if a course is registered
        public bool CheckRegistered(String CourseName)
        {
            foreach (KeyValuePair<string, double> entry in paid)
            {
               if(CourseName == entry.Key) return true;

            }

            foreach (KeyValuePair<string, double> entry in unpaid)
            {
                if (CourseName == entry.Key) return true;
                else return false;

            }

            return false;


        }

    }
}
