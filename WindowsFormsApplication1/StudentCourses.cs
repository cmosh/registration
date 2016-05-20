using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
 
    public partial class StudentCourses : Form
    {
        Storage db = new Storage();
        Dictionary<int, Course> unpaidcourses, unregistered;
        Student student;
        builder build;

        private void button1_Click(object sender, EventArgs e)
        {
            int courseID = (comboBox1.SelectedItem as ComboBoxItem).Value;
            Course course = new Course(courseID);
            student.payments.bill(course);
            MessageBox.Show(student.getName() + " has registered for "+course.getName()+".", "Registeration");           
            this.build();
        }

        private void loadBoxes()
        {
            unpaidcourses = student.payments.getUnpaid();
            unregistered = student.payments.getUnregistered();
            this.comboBox1.Items.Clear();
            this.comboBox2.Items.Clear();            
            foreach (KeyValuePair<int, Course> x in unregistered)
            {
                int i = x.Value.getID();
                String stud = x.Value.getName() + ", Cost: " + x.Value.getCost();
                this.comboBox1.Items.Add(new ComboBoxItem(stud, i));
            }

            foreach (KeyValuePair<int, Course> x in unpaidcourses)
            {
                int i = x.Value.getID();
                String stud = x.Value.getName() + ", Cost: " + x.Value.getCost();
                this.comboBox2.Items.Add(new ComboBoxItem(stud, i));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int courseID = (comboBox2.SelectedItem as ComboBoxItem).Value;
            Course course = new Course(courseID);
            student.payments.pay(course);
            MessageBox.Show("Course "+course.getName() + " has been paid for by " + student.getName(), "Payment");
            this.build();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void StudentCourses_Load(object sender, EventArgs e)
        {
            this.build = new builder(this.loadBoxes);
        }

        public StudentCourses(int Admno)
        {
            InitializeComponent();
            this.CenterToScreen();
            this.student = new Student(Admno);
            this.build();
        }
    }
}
