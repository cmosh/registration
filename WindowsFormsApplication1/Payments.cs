using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Payments
    {

        private Storage db;
        private int admNo;


        //intialise list of paid and unpaid courses
        public Payments(int admNo)
        {
            this.admNo = admNo;
            db = new Storage();
        }

        //register course and bill student
        public void bill(Course course)
        {
            db.billStudent(course, this.admNo);

        }

        //pay for registered course
        public void pay(Course course)
        {
            db.paybillStudent(course, this.admNo);

        }
        //get all paid for courses
        public Dictionary<int, Course> getPaid()
        {
            return db.getPaidPayments(this.admNo);
        }

        //get total outstanding fees
        public double getTotal()
        {
            return db.getTotalUnpaid(this.admNo);

        }
        //get all courses that are registered but unpaid for
        public Dictionary<int, Course> getUnpaid()
        {
            return db.getUnpaidPayments(this.admNo);

        }

        public Dictionary<int,Course> getUnregistered()
        {
            Dictionary<int, Course> courses, unregistered;
            unregistered = new Dictionary<int, Course>();
            courses = db.getCourses();
            foreach (KeyValuePair<int, Course> x in courses)
            {
                int i = x.Value.getID();
                if (!this.CheckRegistered(i)) unregistered[i] = x.Value;
             
            }

            return unregistered;
        }

        //Check if a course is registered
        public bool CheckRegistered(int CourseID)
        {

            String paymentstatus = db.getPayment(CourseID, this.admNo);

            if (paymentstatus != "none")
            {
                return true;
            }
            else
            {
                return false;
            }



        }

    }
}
