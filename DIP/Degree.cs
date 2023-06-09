using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DIP
{
    public delegate void degree_send(double t);
    public partial class Degree : Form
    {
        public Degree()
        {
            InitializeComponent();
        }
        public event degree_send Sending;

        private void Degree_Load(object sender, EventArgs e)
        {
            textBox1.Text = "180";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Sending(Convert.ToDouble(textBox1.Text));
            this.Close();
        }
    }
}
