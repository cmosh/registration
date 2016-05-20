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
    public partial class ManageStudents : Form
    {
        private Storage db;
        private Dictionary<int,Student> students;
        private Dictionary<int, Course> paid, registered;
        private Student student;
        public ManageStudents()
        {
            InitializeComponent();
            this.CenterToScreen();
            db = new Storage();
            this.students = db.getStudents();
            this.button2.Enabled = false;
            foreach (KeyValuePair<int, Student> x in students)
            {
                int i = x.Value.getAdmNo();
                String stud = x.Value.getName() + ", Year " + x.Value.getYear();
                this.comboBox1.Items.Add(new ComboBoxItem(stud, i));
            }
           

        }

        private void ManageStudents_Load(object sender, EventArgs e)
        {

        }

        private void builboxes()
        {
            this.listBox1.Items.Clear();
            this.listBox2.Items.Clear();
            this.paid = this.student.payments.getPaid();
            this.registered = this.student.payments.getUnpaid();   
            foreach (KeyValuePair<int, Course> x in paid)
            {
                int i = x.Value.getID();
                String cs = x.Value.getName() + ", Cost " + x.Value.getCost();
                this.listBox1.Items.Add(new ComboBoxItem(cs, i));
            }
           
            foreach (KeyValuePair<int, Course> x in registered)
            {
                int i = x.Value.getID();
                String cs = x.Value.getName() + ", Cost " + x.Value.getCost();
                this.listBox2.Items.Add(new ComboBoxItem(cs, i));
            }
            
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Admno = (comboBox1.SelectedItem as ComboBoxItem).Value;
            student = new Student(Admno);
            label9.Text = Admno.ToString();
            label10.Text = student.getName();
            label11.Text = student.getYear().ToString();
            double tt = student.payments.getTotal();
            label12.Text = tt.ToString();
            if (tt > 0)
            {
                label15.Text = "not elligible";
                label16.Text = "access denied";
            }
            else
            {
                label15.Text = "elligible";
                label16.Text = "access approved";
            }
            this.builboxes();
            this.button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Admno = (comboBox1.SelectedItem as ComboBoxItem).Value;
            var form = new StudentCourses(Admno);
            this.comboBox1.SelectedValue = "";
            form.Show(this);
        }
    }
}
