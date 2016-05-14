using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace registration
{
    class Courses
    {
        private String Name;
        private int id;
        private double Cost;
        private database db;

        public Courses(String Name, int id, double Cost)
        {
            db = new database();
            this.Name = Name;
            this.id = id;
            this.Cost = Cost;
            db.addCourse(id, Name, Cost);
        }

        public Courses(int id)
        {
            this.db = new database();
            this.id = id;
            this.Name = db.getCourse(id.ToString(), "Name");
            this.Cost = int.Parse(db.getCourse(id.ToString(), "cost"));
            
        }

        public String getName()
        {
            return this.db.getCourse(this.id.ToString(), "Name");
        }
        public int getID()
        {
            return int.Parse(this.db.getCourse(this.id.ToString(), "id"));
        }
        public double getCost()
        {
            return double.Parse(this.db.getCourse(this.id.ToString(), "cost"));
        }
    }
}
