using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DIP
{
    public delegate void histogram_send(Bitmap img);
    public partial class histogram : Form
    {
        public histogram()
        {
            InitializeComponent();
        }
        public event histogram_send Sending;
        private void histogram_Load(object sender, EventArgs e)
        {
            getBrightness();
        }
        private void getBrightness()
        {
            Bitmap image = new Bitmap(pictureBox1.Image);

            bool isGrayscale = true;
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    if (pixelColor.R != pixelColor.G || pixelColor.R != pixelColor.B)
                    {
                        isGrayscale = false;
                        break;
                    }
                }
                if (!isGrayscale)
                {
                    break;
                }
            }

            int[] histogram = new int[256];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int brightness;
                    if (isGrayscale)
                    {
                        brightness = pixelColor.R;
                    }
                    else
                    {
                        brightness = (int)(0.2126 * pixelColor.R + 0.7152 * pixelColor.G + 0.0722 * pixelColor.B);
                    }

                    histogram[brightness]++;
                }
            }
            int maxCount = histogram.Max();

            int histogramWidth = 256;
            int histogramHeight = 256;
            int maxHeight = histogramHeight - 1;
            Bitmap histogramImage = new Bitmap(histogramWidth, histogramHeight);
            Graphics histogramGraphics = Graphics.FromImage(histogramImage);
            SolidBrush brush = new SolidBrush(Color.Black);
            histogramGraphics.FillRectangle(brush, 0, 0, histogramWidth, histogramHeight);
            for (int i = 0; i < 256; i++)
            {
                float value = histogram[i];
                float scaledValue = value * maxHeight / maxCount;
                Pen pen = new Pen(Color.White);
                histogramGraphics.DrawLine(pen, i, histogramHeight, i, histogramHeight - scaledValue);
            }
            pictureBox2.Image = histogramImage;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
