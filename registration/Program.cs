using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    class Program
    {
        static Courses[] courses = new Courses[3];
        static void Main(string[] args)
        {
            String temp,studentname,admNo;
            int choice, year;
            //create new student
            Console.Write("PLease Register a Student\n");
            Console.Write("Students Name: ");
            temp = Console.ReadLine();
            studentname = temp;
            Console.Write("Admission Number: ");
            temp = Console.ReadLine();
            admNo = temp;
            Console.Write("Year of Study: ");
            temp = Console.ReadLine();
            year = int.Parse(temp);
            Student stud = new Student(studentname, admNo,year);
            //Add arbitrary courses
            courses[0] = new Courses("Math","MAT101", 23400.01);
            courses[1] = new Courses("Philosophy", "PHY101", 12456.23);
            courses[2] = new Courses("Communication Skills", "COM101", 17890.50);
            Options:
            Console.Write("PLease Choose an Option\n1. Register Course\n2. Pay For Courses\n3. View Courses\n4. View Admission Details\n5. Exit\nAnswer:");
            temp = Console.ReadLine();
            choice = int.Parse(temp);
            //evaluate chosen option
            switch (choice)
            {
                case 1:
                    int choice1;
                   // Choose A Course to register
                    Console.WriteLine("Choose Course:\n1. Math\n2. Philosophy\n3. Communication Skills\nAnswer:");
                    temp = Console.ReadLine();
                    choice1 = int.Parse(temp);
                    //Register course and bill student
                    CourseChooser(choice1,ref stud);
                    break;
                case 2:
                    int choice2;
                    int index = 1;
                    Console.WriteLine("Choose Course:\n");
                    //get all unpaid courses
                    foreach (String i in stud.payments.CheckUnpaid())
                    {
                        Console.Write(index + ". " + i + " - Cost: " + stud.payments.Cost(i) +"\n");
                        index++;
                    }
                    Console.Write("Answer:");
                    temp = Console.ReadLine();
                    choice2 = int.Parse(temp);
                    //pay for the unpaid course
                    CoursePay(stud.payments.CheckUnpaid()[choice2-1], ref stud);

                    break;
                case 3:
                    //get all courses, grouped by those paid for and those not paid for
                   stud.listCourses();
                    break;
                case 4:
                    //Show all student details
                    Console.WriteLine("Student Name: "+stud.getName());
                    Console.WriteLine("Student Admisson Number : " + stud.getAdmNo());
                    Console.WriteLine("Year of Study: " + stud.getYear());
                    Console.WriteLine("Balance: " + stud.payments.getTotal());
                    break;
                default:
                    goto Exits;
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            goto Options;


        Exits:
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();


        }


            public static void CourseChooser(int choice, ref Student stud)
        {
            choice -= 1;
            //get cost and name of chosen course
            String name = courses[choice].getName();
            double cost = courses[choice].getCost();
            //if course is already registered skip, else register course, then bill.
            if (!stud.payments.CheckRegistered(name))
            {
                stud.payments.bill(name, cost);
                Console.WriteLine(name + " has been registered, " + cost + " has been charged to " + stud.getName() + "'s account");
                Console.WriteLine(stud.payments.getTotal() + " is now owed to the school");
            }
            else

                Console.WriteLine("Course Already Registered");

        }

        public static void CoursePay(String choice, ref Student stud)
        {

            String name = choice;
            double cost = stud.payments.Cost(choice);
            stud.payments.pay(name, cost);          

             Console.WriteLine(name + " has now been paid for, remaining balance is " + stud.payments.getTotal());

        }
    }
}
