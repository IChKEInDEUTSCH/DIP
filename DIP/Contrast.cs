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
    public delegate void contrast(Bitmap img);
    public partial class Contrast : Form
    {
        public Contrast()
        {
            InitializeComponent();
        }
        public event contrast Sending;
        private void Contrast_Load(object sender, EventArgs e)
        {
            pictureBox2.Image = pictureBox1.Image;
            trackBar1.Value = 0;
            trackBar1.Maximum = 255;
            trackBar1.Minimum = -255;
            label1.Text = "Contrast : 0 ";
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Contrast : " + trackBar1.Value.ToString();
            Bitmap bmp = new Bitmap(pictureBox1.Image);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color pixel = bmp.GetPixel(x, y);

                    int r = pixel.R + (pixel.R - 127) * trackBar1.Value / 255;
                    int g = pixel.G + (pixel.G - 127) * trackBar1.Value / 255;
                    int b = pixel.B + (pixel.B - 127) * trackBar1.Value / 255;

                    r = Math.Max(0, Math.Min(255, r));
                    g = Math.Max(0, Math.Min(255, g));
                    b = Math.Max(0, Math.Min(255, b));

                    bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            pictureBox2.Image = bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Sending(new Bitmap(pictureBox2.Image));
            this.Close(); 
        }
    }
}
