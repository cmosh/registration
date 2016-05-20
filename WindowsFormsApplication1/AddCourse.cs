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
    internal delegate int NewCode();
    public partial class AddCourse : Form
    {
        Storage db = new Storage();
        Course course;
        NewCode newcode;
        int Code;
        public AddCourse()
        {
            InitializeComponent();
            this.CenterToScreen();
            this.newcode = new NewCode(db.getNewCode);
            this.Code = this.newcode();
            this.label4.Text = Code.ToString();
            this.numericUpDown1.Maximum = 50000;
            this.numericUpDown1.Minimum = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.course = new Course(this.textBox1.Text, Code, (double)this.numericUpDown1.Value);
            MessageBox.Show(course.getName() + "has been created. Students can now be enroled for it.", this.course.getName());
        }
    }
}
