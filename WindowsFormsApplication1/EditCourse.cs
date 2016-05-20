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
    internal delegate void Update(Course course, double x);
    public partial class EditCourse : Form
    {
        Course course;
        Storage db;
        Update update;
        public EditCourse(int id)
        {
            InitializeComponent();
            this.CenterToScreen();
            course = new Course(id);
            db = new Storage();
            this.label3.Text = course.getCost().ToString();
            this.numericUpDown1.Maximum = 50000;
            this.numericUpDown1.Minimum = 1;
            update = new Update(this.db.updatecost);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.update(course,(double)this.numericUpDown1.Value);
            MessageBox.Show(course.getName() + "has been updated", this.course.getName());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
