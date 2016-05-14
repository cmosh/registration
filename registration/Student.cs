using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    class Student
    {
        private String name;
        private int admNo,yearofstudy;
        internal database db;
        internal Payments payments;

        public Student(int admNo,String name,int yearofstudy)
        {
            this.name = name;
            this.admNo = admNo;
            this.payments = new Payments(admNo);
            this.yearofstudy = yearofstudy;
            this.db = new database();
            db.addStudent(admNo,name, yearofstudy);           

        }

        public Student(int admNo)
        {
            this.db = new database();
            this.admNo = admNo;
            this.name = db.getStudent(admNo.ToString(), "Name");
            this.payments = new Payments(admNo);
            this.yearofstudy = int.Parse(db.getStudent(admNo.ToString(), "Year"));
            
        }
        public String getName()
        {
            return this.db.getStudent(this.admNo.ToString(),"Name");
        }

        public int getAdmNo()
        {
            return int.Parse(this.db.getStudent(this.admNo.ToString(), "id"));
        }

        public int getYear()
        {
            return int.Parse(this.db.getStudent(this.admNo.ToString(),"Year"));
        }

        //list all courses that have both been paid and not paid for but are registered
        public String listCourses()
        {
            String list = "";
            list +="Courses Registered and Paid for: ";
            foreach (KeyValuePair<int,Courses> x in payments.getPaid())
            {
                list += (x.Value.getName()+",");
            }

            list += ("\n");

            list += ("Courses Registered and Unpaid: ");
            foreach (KeyValuePair<int, Courses> x in payments.getUnpaid())
            {
                list += (x.Value.getName() + ",");
            }
            list += ("\n");
            return list;
        }

    }
}
