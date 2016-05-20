using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace WindowsFormsApplication1
{
    class Storage
    {
        internal SQLiteConnection con;
        internal SQLiteCommand com;
        internal Storage()
        {
           this.con = new SQLiteConnection("data source=school.db3");
          
        }

        internal int getNewID()
        {
            int id;
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT Max(Students.id) + 1 FROM Students; ";
            id = int.Parse(com.ExecuteScalar().ToString());
            this.con.Close();
            return id;
        }

        internal double getTotalUnpaid(int id)
        {
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT ifnull(Sum(payments.amount),0) FROM payments WHERE payments.studentID = " + id+" AND payments.paid = 0";
            String temp = (String)com.ExecuteScalar().ToString();
            double amt = double.Parse(temp);
            this.con.Close();
            return amt;
        }

        internal int getNewCode()
        {
            int id;
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT Max(courses.id) + 1 FROM courses; ";
            id = int.Parse(com.ExecuteScalar().ToString());
            this.con.Close();
            return id;
        }
        internal void addStudent(int id,String name ,String last, int year)
        {
            string insertstudent = "INSERT INTO Students (id,Name,Last,Year) Values ('" + id + "','" + name+ "','" + last + "','" + year+"');";
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = insertstudent;
            com.ExecuteNonQuery();
            this.con.Close();
            
        }

        internal void addCourse(int id, String name, double cost)
        {
            string insertcourse = "INSERT INTO courses (id,Name,cost) Values ('" + id + "','" + name + "','" + cost + "');";
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = insertcourse;
            com.ExecuteNonQuery();
            this.con.Close();

        }

        internal String getPayment(int courseID,int studentID)
        {
                      
            this.con.Open();
            com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT payments.paid FROM payments WHERE payments.studentID = " + studentID + " AND payments.courseID = " + courseID + " ; ";
            if (com.ExecuteScalar() == null)
            {
                this.con.Close();
                return "none";
            }else
            {
                this.con.Close();
                return "there";
            }

            
           
        }
        internal Dictionary<int, Student> getStudents()
        {
           int tempint;
           this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "Select * FROM Students";
           SQLiteDataReader reader = com.ExecuteReader();
            Dictionary<int, Student> students = new Dictionary<int,Student>();
            while (reader.Read())
            {
                tempint = int.Parse(reader["id"].ToString());
                Student temp = new Student(tempint);
                students[tempint] = temp;
            }
          
            this.con.Close();
            return students;
        }

        internal void updatecost(Course cs,double newcost)
        {
             String updatecostcom = "UPDATE courses SET cost = " + newcost.ToString() + " WHERE ID = " + cs.getID().ToString()+";";
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = updatecostcom;
            com.ExecuteNonQuery();
            this.con.Close();
        }
        internal Dictionary<int,Course> getUnpaidPayments(int id)
        {
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT payments.courseID FROM payments WHERE payments.studentID = "+id+" AND payments.paid = 0 ; ";
            SQLiteDataReader reader = com.ExecuteReader();
            Dictionary<int, int> courses = new Dictionary<int, int>();
            int  tempid;
            while (reader.Read())
            {
                tempid = int.Parse(reader["courseID"].ToString());
             courses[tempid] = (tempid);
            }
            this.con.Close();

            Dictionary<int, Course> courses2 = new Dictionary<int, Course>();

            foreach (KeyValuePair<int,int> cs in courses)
            {
                courses2[cs.Key] = new Course(cs.Key);
            }
            return courses2;
        }

        internal Dictionary<int, Course> getPaidPayments(int id)
        {
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT payments.courseID FROM payments WHERE payments.studentID = " + id + " AND payments.paid = 1 ; ";
            SQLiteDataReader reader = com.ExecuteReader();
            Dictionary<int, int> courses = new Dictionary<int, int>();
            int tempid;
            while (reader.Read())
            {
                tempid = int.Parse(reader["courseID"].ToString());
                courses[tempid] = (tempid);
            }
            this.con.Close();

            Dictionary<int, Course> courses2 = new Dictionary<int, Course>();

            foreach (KeyValuePair<int, int> cs in courses)
            {
                courses2[cs.Key] = new Course(cs.Key);
            }
            return courses2;

        }

        internal Dictionary<double, Course> getCollected()
        {
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT payments.courseID, payments.amount FROM payments WHERE payments.paid = 1 ; ";
            SQLiteDataReader reader = com.ExecuteReader();
            Dictionary<int, double> courses = new Dictionary<int, double>();
            int tempid;
            double tempcost;
            while (reader.Read())
            {
                tempid = int.Parse(reader["courseID"].ToString());
                tempcost = double.Parse((reader["amount"]).ToString());
                courses[tempid] = (tempcost);
            }
            this.con.Close();

            Dictionary<double, Course> courses2 = new Dictionary<double, Course>();

            foreach (KeyValuePair<int, double> cs in courses)
            {
                courses2[cs.Value] = new Course(cs.Key);
            }
            return courses2;

        }
        internal Dictionary<int, Course> getCourses()
        {
            int tempid;
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "Select * FROM courses";
            SQLiteDataReader reader = com.ExecuteReader();
            Dictionary<int, Course> courses = new Dictionary<int, Course>();
            while (reader.Read())
            {
                tempid = int.Parse(reader["id"].ToString());
                courses[tempid] = new Course(tempid);
            }
            this.con.Close();
            return courses;
        }
        internal String getStudent(String id,String attribute)
        {
            String Value = "";
           
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "Select Students."+attribute+" FROM Students WHERE Students.id = "+id;
            Value = com.ExecuteScalar().ToString();          
            this.con.Close();
            return Value;

        }

        internal String getCourse(String id, String attribute)
        {
            String Value = "";
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "Select courses." + attribute + " FROM courses WHERE courses.id = " + id;
            Value = com.ExecuteScalar().ToString();
            this.con.Close();
            return Value;

        }

        internal String TotalUncollected()
        {
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT ifnull(Sum(payments.amount),0) FROM payments WHERE payments.paid = 0;";
            String Value = com.ExecuteScalar().ToString();
            this.con.Close();
            return Value;
        }


        internal String Total()
        {
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT ifnull(Sum(payments.amount),0) FROM payments WHERE payments.paid = 1;";
            String Value = com.ExecuteScalar().ToString();
            this.con.Close();
            return Value;
        }

        internal void billStudent(Course course,int AdmNo)
        {
            int id = course.getID();
            double cost = course.getCost();
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "INSERT INTO payments (studentID,courseID,amount,paid) Values ('" + AdmNo + "','" +id+ "','" + cost + "',0);";
            com.ExecuteNonQuery();
            this.con.Close();
        }


        internal void paybillStudent(Course course, int AdmNo)
        {
            int id = course.getID();
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "UPDATE payments SET paid = 1 WHERE studentID = "+AdmNo+" AND courseID = "+id+";";
            com.ExecuteNonQuery();
            this.con.Close();
        }

        internal Dictionary<double, Course> getUncollected()
        {
            this.con.Open();
            SQLiteCommand com = new SQLiteCommand(this.con);
            com.CommandText = "SELECT payments.courseID , payments.amount FROM payments WHERE payments.paid = 0 ; ";
            SQLiteDataReader reader = com.ExecuteReader();
            Dictionary<int, double> courses = new Dictionary<int, double>();
            int tempid;
            double tempcost;
            while (reader.Read())
            {
                tempid = int.Parse(reader["courseID"].ToString());
                tempcost = double.Parse((reader["amount"]).ToString());
                courses[tempid] = (tempcost);
            }
            this.con.Close();

            Dictionary<double, Course> courses2 = new Dictionary<double, Course>();

            foreach (KeyValuePair<int, double> cs in courses)
            {
                courses2[cs.Value] = new Course(cs.Key);
            }
            return courses2;
        }

    }
}
