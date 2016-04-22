using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    class Student
    {
        private String name, admNo;
        private int yearofstudy;
        internal Payments payments;

        public Student(String name, String admNo,int yearofstudy)
        {
            this.name = name;
            this.admNo = admNo;
            this.payments = new Payments();
            this.yearofstudy = yearofstudy;

        }
        public String getName() { return this.name; }
        public String getAdmNo() { return this.admNo; }
        public int getYear() { return this.yearofstudy; }

        //list all courses that have both been paid and not paid for but are registered
        public void listCourses()
        {
            Console.Write("Courses Registered and Paid for: " + payments.getPaid()+"\n");
            Console.Write("Courses Registered and Unpaid: " + payments.getUnpaid()+"\n");

        }

    }
}
