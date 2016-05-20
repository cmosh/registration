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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new createstudent();
            form.Show(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to quit?", "My School Manager",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new ManageStudents();
            form.Show(this);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new ManageCourses();
            form.Show(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var form = new SchoolAccounts();
            form.Show(this);
        }
    }
}
