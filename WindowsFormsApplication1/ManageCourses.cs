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
    public partial class ManageCourses : Form
    {
        Storage db;
        Dictionary<int, Course> courses;
        builder build;
        public ManageCourses()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.db = new Storage();
            this.build = new builder(this.loadbox);
            this.build();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int courseID = (comboBox1.SelectedItem as ComboBoxItem).Value;
            Course course = new Course(courseID);
            this.label3.Text = courseID.ToString();
            this.label5.Text = course.getName();
            this.label7.Text = course.getCost().ToString();
        }

        private void ManageCourses_Load(object sender, EventArgs e)
        {
            
        }

        private void loadbox()
        {
            this.courses = db.getCourses();
            this.comboBox1.Items.Clear();
           
            foreach (KeyValuePair<int, Course> x in courses)
            {
                int i = x.Value.getID();
                String stud = x.Value.getName() + ", Cost: " + x.Value.getCost();
                this.comboBox1.Items.Add(new ComboBoxItem(stud, i));
            }           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int courseID = (comboBox1.SelectedItem as ComboBoxItem).Value;
            var form = new EditCourse(courseID);
            form.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new AddCourse();
            form.Show(this);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
