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
    public partial class SchoolAccounts : Form
    {
        Storage db;
        Dictionary<int, Course> collected, uncollected;
        public SchoolAccounts()
        {
            InitializeComponent();
            this.CenterToScreen();
            db = new Storage();
            this.builboxes();

        }

        private void builboxes()
        {
            collected = db.getCollected();
            uncollected = db.getUncollected();

            foreach (KeyValuePair<int, Course> x in collected)
            {
                int i = x.Value.getID();
                String cs = x.Value.getName() + ", Cost " + x.Value.getCost();
                this.listBox1.Items.Add(new ComboBoxItem(cs, i));
            }
            this.label3.Text = db.Total();
            foreach (KeyValuePair<int, Course> x in uncollected)
            {
                int i = x.Value.getID();
                String cs = x.Value.getName() + ", Cost " + x.Value.getCost();
                this.listBox2.Items.Add(new ComboBoxItem(cs, i));
            }
            this.label4.Text = db.TotalUncollected();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
