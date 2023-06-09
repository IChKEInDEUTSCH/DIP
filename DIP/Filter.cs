using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DIP
{
    public delegate void f_send(Bitmap img);
    public partial class Filter : Form
    {
        public ToolStripLabel tsl;
        internal double[,] kkernel;
        internal string _case;
        public Filter()
        {
            InitializeComponent();
        }
        public event f_send Sending;
        private void Filter_Load(object sender, EventArgs e)
        {
            trackBar1.Value = 1;
            trackBar1.Maximum = 10;
            trackBar1.Minimum = 1;
            label1.Text = "Kernel Size: " + trackBar1.Value.ToString();
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            int size = trackBar1.Value;
            label1.Text = "Kernel Size: " + trackBar1.Value.ToString();
            double[,] kernel = new double[trackBar1.Value, trackBar1.Value];
            switch (_case)
            {
                case "avg":
                    groupBox2.Visible = false;
                    break;
                case "guss":
                    trackBar2.Visible = true;
                    label2.Visible = true;
                    label2.Text = "Sigma: 1";
                    trackBar2.Value = 1;
                    trackBar2.Maximum = 10;
                    trackBar2.Minimum = 1;
                    break;
                case "prew_hori":
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    size = 3;
                    kernel = new double[,]
                    {
                        {-1, 0, 1},
                        {-1, 0, 1},
                        {-1, 0, 1}
                    };
                    Convolution(ref bmp, size, kernel);
                    break;
                case "prew_vert":
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    size = 3;
                    kernel = new double[,]
                    {
                        {-1, -1, -1},
                        {0, 0, 0},
                        {1, 1, 1}
                    };
                    Convolution(ref bmp, size, kernel);
                    break;
                case "sobel_hori":
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    size = 3;
                    kernel = new double[,]
                    {
                        {-1, 0, 1},
                        {-2, 0, 2},
                        {-1, 0, 1}
                    };
                    Convolution(ref bmp, size, kernel);
                    break;
                case "sobel_vert":
                    groupBox1.Visible = false;
                    groupBox2.Visible = false;
                    size = 3;
                    kernel = new double[,]
                    {
                        {-1, -2, -1},
                        {0, 0, 0},
                        {1, 2, 1}
                    };
                    Convolution(ref bmp, size, kernel);
                    break;
                case "media":
                    groupBox2.Visible = false;
                    break;
            }
        }

        private double margin(double n)
        {
            if (n < 0)
            {
                return 0;
            }
            else if (n > 255)
            {
                return 255;
            }
            else
            {
                return n;
            }
        }
        private void Convolution(ref Bitmap _bmp, double _size, double[,] _kernel)
        {
            double r, g, b;
            for (int y = 0; y < _bmp.Height; y++)
            {
                for (int x = 0; x < _bmp.Width; x++)
                {
                    r = 0; g = 0; b = 0;
                    for (int i = 0; i < _size; i++)
                    {
                        for (int j = 0; j < _size; j++)
                        {
                            if (x + j <= _bmp.Height - 1 && x + j >= 0 && y + i <= _bmp.Width - 1 && y + i >= 0)
                            {
                                Color pixel = _bmp.GetPixel(x + j, y + i);
                                r += pixel.R * _kernel[i, j];
                                g += pixel.G * _kernel[i, j];
                                b += pixel.B * _kernel[i, j];
                            }
                        }
                    }
                    r = margin(r);
                    g = margin(g);
                    b = margin(b);
                    _bmp.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                }
            }
            pictureBox2.Image = _bmp;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            int size = trackBar1.Value;
            label1.Text = "Kernel Size: " + size.ToString();

            double r, g, b;
            double[,] kernel = new double[size, size];
            switch (_case)
            {
                case "avg":
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            kernel[i, j] = 1 / (double)(size * size);
                        }
                    }
                    Convolution(ref bmp, size, kernel);
                    break;
                case "guss":
                    double sigma, sum = 0, x2, y2, gus;
                    sigma = (double)(trackBar2.Value) / 10.0;
                    label2.Text = "Sigma: " + sigma.ToString();
                    int center = size / 2;
                    for (int i = 0; i < size; i++)
                    {
                        x2 = Math.Pow(i - center, 2);
                        for (int j = 0; j < size; j++)
                        {
                            y2 = Math.Pow(j - center, 2);
                            gus = Math.Exp(-(x2 + y2) / (2 * sigma * sigma));
                            gus /= 2 * Math.PI * sigma;
                            sum += gus;
                            kernel[i, j] = gus;
                        }
                    }
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            kernel[i, j] /= sum;
                        }
                    }
                    Convolution(ref bmp, size, kernel);
                    break;
                case "media":
                    size = trackBar1.Value;
                    int[] sort_r = new int[size * size];
                    int[] sort_g = new int[size * size];
                    int[] sort_b = new int[size * size];
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            r = 0; g = 0; b = 0;
                            for (int i = 0; i < size; i++)
                            {
                                for (int j = 0; j < size; j++)
                                {
                                    if (x + j <= bmp.Height - 1 && x + j >= 0 && y + i <= bmp.Width - 1 && y + i >= 0)
                                    {
                                        Color pixel = bmp.GetPixel(x + j, y + i);
                                        sort_r[i * size + j] = pixel.R;
                                        sort_g[i * size + j] = pixel.G;
                                        sort_b[i * size + j] = pixel.B;
                                    }
                                }
                            }
                            Array.Sort(sort_r);
                            Array.Sort(sort_g);
                            Array.Sort(sort_b);
                            r = sort_r[size * size / 2];
                            g = sort_g[size * size / 2];
                            b = sort_b[size * size / 2];
                            bmp.SetPixel(x, y, Color.FromArgb((int)r, (int)g, (int)b));
                        }
                    }
                    pictureBox2.Image = bmp;
                    break;
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            int size = trackBar1.Value;
            label1.Text = "Kernel Size: " + size.ToString();
            double[,] kernel = new double[size, size];
            switch (_case)
            {
                case "avg":
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            kernel[i, j] = 1 / (double)(size * size);
                        }
                    }
                    break;
                case "guss":
                    double sigma, sum = 0, x2, y2, gus;
                    sigma = (double)(trackBar2.Value) / 10.0;
                    label2.Text = "Sigma: " + sigma.ToString();
                    int center = size / 2;
                    for (int i = 0; i < size; i++)
                    {
                        x2 = Math.Pow(i - center, 2);
                        for (int j = 0; j < size; j++)
                        {
                            y2 = Math.Pow(j - center, 2);
                            gus = Math.Exp(-(x2 + y2) / (2 * sigma * sigma));
                            gus /= 2 * Math.PI * sigma;
                            sum += gus;
                            kernel[i, j] = gus;
                        }
                    }
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            kernel[i, j] /= sum;
                        }
                    }
                    break;
            }
            Convolution(ref bmp, size, kernel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Sending(new Bitmap(pictureBox2.Image));
            this.Close();
        }
    }
}
