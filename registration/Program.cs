using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    internal delegate void vdmethods();
    internal delegate void vdmethodsops(Student stud);
    internal delegate void vdcoursechooser(Courses course, ref Student stud);

    class Program
    {
        static Courses[] courses = new Courses[3];


        static void Main(string[] args)
        {
            vdmethods mgcts = new vdmethods(ManageAccounts);
            vdmethods mgcourses = new vdmethods(ManageCourses);

            vdmethodsops mgops = new vdmethodsops(ManageOperations);

            database db = new database();
            int choice;
            Options:
            Console.WriteLine("Please Choose an Option:\n1. Create New Student\n2. Manage Students\n3. Manage Courses\n4. School Accounts\n5. Exit");
            choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    int Admno = db.getNewID();
                    Console.WriteLine("Admission number is "+ db.getNewID());
                    Console.WriteLine("Student Name: ");
                    String name = Console.ReadLine();
                    Console.WriteLine("Year of Study: ");
                    int year = int.Parse(Console.ReadLine());
                    Student stud = new Student(Admno, name, year);
                    Console.WriteLine("Student Created!");
                    break;
                case 2:
                    Console.WriteLine("Please Choose a Student");
                    Dictionary<int, Student> Students = db.getStudents();
                    foreach(KeyValuePair<int,Student>Stud in Students)
                    {
                        Console.WriteLine(Stud.Key + ". " + Stud.Value.getName()+" Year: "+ Stud.Value.getYear());
                    }
                    choice = int.Parse(Console.ReadLine());                   
                    mgops(Students[choice]);
                    break;
                case 3:
                    Console.WriteLine("Manage Courses");
                    mgcourses();
                    break;
                case 4:
                    Console.WriteLine("School Accounts");
                    mgcts();
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

        static void ManageAccounts()
        {
        Options:
            Console.WriteLine("Please Choose an Option\n1. Check Total Earnings\n2. Total Uncollected Fees\n3. Exit");
            database db = new database();
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Total Earnings " + db.Total());
                    break;
                case 2:
                    Console.WriteLine("Uncollected Fees " + db.TotalUncollected());
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
        static void ManageCourses()
        {
            Options:
            Console.WriteLine("Please Choose an Option\n1. Add Course\n2. Change Cost\n3. View Courses\n4. Exit");
            database db = new database();
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    int code = db.getNewCode();
                    Console.WriteLine("Course code is " +code);
                    Console.WriteLine("Course Name: ");
                    String Name = Console.ReadLine();
                    Console.WriteLine("Course Cost: ");
                    double Cost = double.Parse(Console.ReadLine());
                    Courses course = new Courses(Name, code, Cost);
                    break;
                case 2:
                    Console.WriteLine("Please Choose a Course");
                    Dictionary<int, Courses> coses = db.getCourses();
                    foreach (KeyValuePair<int, Courses> cos in coses)
                    {
                        Console.WriteLine(cos.Key + ". " + cos.Value.getName() + " Cost: "+cos.Value.getCost());
                    }
                    choice = int.Parse(Console.ReadLine());
                    Courses cs = new Courses(choice);
                    Console.WriteLine("New Amount: ");
                    double newamt = double.Parse(Console.ReadLine());
                    db.updatecost(cs,newamt);
                    Console.WriteLine("Course Updated");
                    break;
                case 3:
                    Console.WriteLine("Here is a list of the courses.");
                    Dictionary<int, Courses> Courses = db.getCourses();
                    foreach (KeyValuePair<int, Courses> cos in Courses)
                    {
                        Console.WriteLine(cos.Key + ". " + cos.Value.getName() + " Cost: " + cos.Value.getCost());
                    }
                    Console.ReadLine();
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

        static void ManageOperations(Student stud)
        {
            vdcoursechooser cschoice = new vdcoursechooser(CourseChooser);
            String temp;
            database db = new database();
            int choice;
            Dictionary<int, Courses> Courses = db.getCourses();
            Options:
            Console.Write("Please Choose an Option\n1. Register Course\n2. Pay For Courses\n3. View Courses\n4. View Admission Details\n5. Check if eligible For Library Access \n6. Check if eligible for exams\n7. Exit\nAnswer:");
            temp = Console.ReadLine();
            choice = int.Parse(temp);
            //evaluate chosen option
            switch (choice)
            {
                case 1:
                    int choice1;
                   // Choose A Course to register
                    Console.WriteLine("Choose Course:\n");
                    foreach (KeyValuePair<int, Courses> cos in Courses)
                    {
                        Console.WriteLine(cos.Key + ". " + cos.Value.getName() + " Cost: " + cos.Value.getCost());
                    }
                    temp = Console.ReadLine();
                    choice1 = int.Parse(temp);
                    //Register course and bill student
                    cschoice(Courses[choice1],ref stud);
                    break;
                case 2:
                    int choice2;
                    Dictionary<int,Courses> UPCourses = stud.payments.getUnpaid();
                    if (UPCourses.Count() == 0) {
                        Console.WriteLine("There are none:\n");
                        break;
                    }
                    Console.WriteLine("Choose Course:\n");
                    //get all unpaid courses
                    foreach (KeyValuePair<int,Courses> i in UPCourses )
                    {
                        Console.Write(i.Key + ". " + i.Value.getName() + " - Cost: " + i.Value.getCost() +"\n");                     
                    }
                    Console.Write("Answer:");
                    temp = (String) Console.ReadLine();
                    choice2 = int.Parse(temp);

                    //pay for the unpaid course
                    Courses thecourse = UPCourses[choice2];

                    stud.payments.pay(thecourse);
                    Console.WriteLine(thecourse.getName() + " has now been paid for, remaining balance is " + stud.payments.getTotal());
                   

                    break;
                case 3:
                    //get all courses, grouped by those paid for and those not paid for
                  Console.Write( stud.listCourses());
                    break;
                case 4:
                    //Show all student details
                    Console.WriteLine("Student Name: "+stud.getName());
                    Console.WriteLine("Student Admisson Number : " + stud.getAdmNo());
                    Console.WriteLine("Year of Study: " + stud.getYear());
                    Console.WriteLine("Balance: " + stud.payments.getTotal());
                    break;
                case 5:
                    if (stud.payments.getTotal() > 0)
                        Console.WriteLine("Cannot Access Library, Pending Fees");
                    else
                        Console.WriteLine("Can Access Library,No pending Fees");
                    break;
                case 6:
                    if (stud.payments.getTotal() > 0)
                        Console.WriteLine("Pending Fees, Advise "+stud.getName()+" clear all balance before sitting for exams");
                    else
                        Console.WriteLine("Student is eligible for exams");
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


            public static void CourseChooser(Courses course, ref Student stud)
        {
           
            //if course is already registered skip, else register course, then bill.
            if (!stud.payments.CheckRegistered(course.getID()))
            {
                stud.payments.bill(course);
                Console.WriteLine("Course "+ course.getName() + " has been registered, KES " + course.getCost() + " has been charged to " + stud.getName() + "'s account");
                Console.WriteLine("KES " + stud.payments.getTotal() + " is now owed to the school by "+ stud.getName());
            }
            else

                Console.WriteLine("Course Already Registered");
    
        }

   
    }
}
