using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    class Payments
    {
        
        private database db;
        private int admNo;
       

        //intialise list of paid and unpaid courses
        public Payments(int admNo)
        {
            this.admNo = admNo;           
            db = new database();
        }

        //register course and bill student
       public void bill(Courses course)
        {
            db.billStudent(course, this.admNo);

        }

        //pay for registered course
        public void pay(Courses course)
        {
            db.paybillStudent(course, this.admNo);

        }
        //get all paid for courses
        public Dictionary<int, Courses> getPaid()
        {
            return db.getPaidPayments(this.admNo);
        }

        //get total outstanding fees
        public double getTotal()
        {
           return db.getTotalUnpaid(this.admNo);
       
        }
        //get all courses that are registered but unpaid for
        public Dictionary<int,Courses> getUnpaid()
        {
            return db.getUnpaidPayments(this.admNo);

        }

       
        //Check if a course is registered
        public bool CheckRegistered(int CourseID)
        {

           String paymentstatus =  db.getPayment(CourseID, this.admNo);

            if(paymentstatus != "none")
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
