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
    public partial class createstudent : Form
    {
        int tempid;
        Student student;
        Storage db;
        NewCode newcode;
        public createstudent()
        {
            db = new Storage();
            newcode = new NewCode(db.getNewID);
            this.tempid = this.newcode();
            InitializeComponent();
            this.numericUpDown1.Minimum = 1;
            this.numericUpDown1.Maximum = 4;
            this.CenterToScreen();
            this.label2.Text = this.tempid.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

    
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to cancel?", "Create Student",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question)
      == DialogResult.Yes)
            {
                this.Close();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string name, last;
            int yos;
            name = this.textBox1.Text;
            last = this.textBox2.Text;
            yos = Convert.ToInt32(this.numericUpDown1.Value);            
            if (MessageBox.Show("Are you sure you wish to enrol " + name + " " + last + " ?", name + " " + last,
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
            {
                student = new Student(this.tempid, name, last,yos);
                MessageBox.Show(name + " " + last + " has been enrolled!!");
                this.Close();
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
