using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DIP
{
    using System;

    public class ConnectedComponentLabeling
    {
        private int[][] labels;
        private int currentLabel;

        public int[][] LabelComponents(int[][] image)
        {
            int height = image.Length;
            int width = image[0].Length;

            labels = new int[height][];
            for (int i = 0; i < height; i++)
            {
                labels[i] = new int[width];
            }

            currentLabel = 1;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (image[i][j] == 1 && labels[i][j] == 0)
                    {
                        AssignLabel(image, i, j);
                        currentLabel++;
                    }
                }
            }

            return labels;
        }

        private void AssignLabel(int[][] image, int row, int col)
        {
            int height = image.Length;
            int width = image[0].Length;

            if (row < 0 || row >= height || col < 0 || col >= width)
                return;

            if (image[row][col] == 1 && labels[row][col] == 0)
            {
                labels[row][col] = currentLabel;

                AssignLabel(image, row - 1, col); // North
                AssignLabel(image, row, col + 1); // East
                AssignLabel(image, row + 1, col); // South
                AssignLabel(image, row, col - 1); // West
            }
        }
    }

    public partial class DIPSample : Form
    {
        int rgb_or_gray;
        public Brightness Bright;
        public histogram hg;
        public Contrast contrast;
        public Zoom_In_and_Out zoom;
        public Degree deg;
        public Filter filter;
        public DIPSample()
        {
            InitializeComponent();
            Bright = new Brightness();
            Bright.Sending += Sending;
            hg = new histogram();
            hg.Sending += Sending;
            contrast = new Contrast();
            contrast.Sending += Sending;
            zoom = new Zoom_In_and_Out();
            zoom.Sending += send_float;
            deg= new Degree();
            //deg.Sending += send_double;
            filter =new Filter();
            filter.Sending += Sending;
        }
        [DllImport("image.dll")]
        unsafe public static extern void img_handle(byte[] path);
        [DllImport("../../../../Debug/dip_proc.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void encode(int *f0,int w,int h,int *g0);
        [DllImport("../../../../Debug/dip_proc.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void encode1(int* f0, int w, int h, int* g0);
        [DllImport("OpenCVRelease.dll", CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void autoFocus(string path);
        Bitmap NpBitmap;
        float t;
        double d;
        int w, h, c;
        private void DIPSample_Load(object sender, EventArgs e)
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
			this.stStripLabel.Text = "";
        }
        private void Sending(Bitmap img)
        {
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = img;
            childForm.Show();
        }
        private void send_float(float t1)
        {
            this.t= t1;
        }
        private void send_double(float d1)
        {
            this.d = d1;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oFileDlg.CheckFileExists = true;
            oFileDlg.CheckPathExists = true;
            oFileDlg.Title = "Open File - DIP Sample";
            oFileDlg.ValidateNames = true;
            oFileDlg.Filter = "bmp files (*.bmp)|*.bmp";
            oFileDlg.FileName = "";

            if (oFileDlg.ShowDialog() == DialogResult.OK)
            {
                MSForm childForm = new MSForm();
                childForm.MdiParent = this;
                childForm.pf1 = stStripLabel;
                NpBitmap = bmp_read(oFileDlg);
                childForm.pBitmap = NpBitmap;
                //childForm.Name = oFileDlg.FileName;
                w = NpBitmap.Width;
                h = NpBitmap.Height;
                childForm.Show();
            }
        }
        private Bitmap bmp_read(OpenFileDialog oFileDlg)
        {
            Bitmap pBitmap;
            string fileloc = oFileDlg.FileName;
            pBitmap = new Bitmap(fileloc);
            w = pBitmap.Width;
            h = pBitmap.Height;
            return pBitmap;
        }

        private int[] bmp2array(Bitmap myBitmap, ref int ByteDepth, ref PixelFormat pixelFormat, ref ColorPalette palette) //grayscale
        {
                BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, myBitmap.Width, myBitmap.Height),
                                               ImageLockMode.ReadWrite,
                                               myBitmap.PixelFormat);
                pixelFormat = myBitmap.PixelFormat;
                palette = myBitmap.Palette;
                ByteDepth = (int)(byteArray.Stride / myBitmap.Width);
                int[] ImgData = new int[myBitmap.Width * myBitmap.Height * ByteDepth];
                int ByteOfSkip = byteArray.Stride - byteArray.Width * ByteDepth;
                if(ByteDepth==1)
                {
                    rgb_or_gray = 0;
                }
                else if(ByteDepth==3)
                {
                    rgb_or_gray = 1;
                }
                unsafe
                {
                    byte* imgPtr = (byte*)(byteArray.Scan0);
                    for (int y = 0; y < byteArray.Height; y++)
                    {
                        for (int x = 0; x < byteArray.Width; x++)
                        {
                            for (int c = 0; c < ByteDepth; c++)
                            {
                                ImgData[(x + byteArray.Height * y) * ByteDepth + c] = (int)*(imgPtr);
                                imgPtr += (int)(byteArray.Stride / myBitmap.Width) / ByteDepth;
                            }
                        }
                        imgPtr += ByteOfSkip;
                    }
                }
                myBitmap.UnlockBits(byteArray);
                return ImgData;
        }

        private static Bitmap array2bmp(int[] ImgData, int ByteDepth, PixelFormat pixelFormat, ColorPalette palette)
        {
            int Width = (int)Math.Sqrt(ImgData.GetLength(0) / ByteDepth);
            int Height = (int)Math.Sqrt(ImgData.GetLength(0) / ByteDepth);
            Bitmap myBitmap = new Bitmap(Width, Height, pixelFormat);
            BitmapData byteArray = myBitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                           ImageLockMode.WriteOnly,
                                           pixelFormat);
            try
            {
                myBitmap.Palette = palette;
            }
            catch
            {

            }
            //Padding bytes的長度
            int ByteOfSkip = byteArray.Stride - myBitmap.Width * ByteDepth;
            unsafe
            {                                   // 指標取出影像資料
                byte* imgPtr = (byte*)byteArray.Scan0;
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
                    {
                        for (int c = 0; c < ByteDepth; c++)
                        {
                            *imgPtr = (byte)ImgData[(x + Height * y) * ByteDepth + c];       //B
                            imgPtr += 1;
                        }
                    }
                    imgPtr += ByteOfSkip; // 跳過Padding bytes
                }
            }
            myBitmap.UnlockBits(byteArray);
            return myBitmap;
        }

        private void bitPlaneToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void b0ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k]%2)*255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void b1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k] / 2 % 2) * 255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void b2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k] / 4 % 2) * 255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void b3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k] / 8  % 2) * 255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void b4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k] / 16 % 2) * 255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void b5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k] /32 % 2) * 255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void b6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k] /64 % 2) * 255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }

        private void b7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    c = ByteDepth;
                    g = new int[w * h * c];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            int i, j, k = 0;
                            for (j = 0; j < h; j++)
                            {
                                for (i = 0; i < c * w; i++)
                                {
                                    g0[k] = (f0[k] /128 % 2) * 255;
                                    k++;
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = NpBitmap;
            childForm.Show();
        }


        private void rGBtoGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    g = new int[3 * w * h]; //RGB
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            for (int i = 0; i < w * h * 3; i += 3)
                            {
                                double gray = f0[i] * 0.299 + f0[i + 1] * 0.587 + f0[i + 2] * 0.114;
                                g[i] = Convert.ToInt16(gray);
                                g[i + 1] = Convert.ToInt16(gray);
                                g[i + 2] = Convert.ToInt16(gray);
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = new Bitmap(NpBitmap);
            childForm.Show();
        }

        private void brightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Bright.IsDisposed)
            {
                Bright = new Brightness();
                Bright.Sending += Sending;
                Bright.MdiParent = this;
                Bright.pictureBox1.Image = NpBitmap;
                Bright.Show();
            }
            else
            {
                Bright.pictureBox1.Image = NpBitmap;
                Bright.MdiParent = this;
                Bright.Show();
            }
        }
        
        private void stStripLabel_Click(object sender, EventArgs e)
        {

        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (hg.IsDisposed)
            {
                hg = new histogram();
                hg.Sending += Sending;
                hg.MdiParent = this;
                hg.pictureBox1.Image = NpBitmap;
                hg.Show();
            }
            else
            {
                hg.pictureBox1.Image = NpBitmap;
                hg.MdiParent = this;
                hg.Show();
            }
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (contrast.IsDisposed)
            {
                contrast = new Contrast();
                contrast.Sending += Sending;
                contrast.MdiParent = this;
                contrast.pictureBox1.Image = NpBitmap;
                contrast.Show();
            }
            else
            {
                contrast.pictureBox1.Image = NpBitmap;
                contrast.MdiParent = this;
                contrast.Show();
            }
        }
        public int getOPTThresholding(Bitmap grayImage, int[] histogram)
        {   //計算類內方差之和最小；
            //獲取按 pixel 個數統計直方圖
            int pixelNumb = grayImage.Height * grayImage.Width;
            int threshold;
            //儲存差異集合
            double[] variances = new double[256];
            //T爲0-255，爲 Threshold 擬定值.
            for (int T = 0; T < histogram.Length; T++)
            {
                //兩區域 pixel 個數，區域 pixel 值總和，區域平均數,區域權重;
                double n1 = 0, n2 = 0;
                double total1 = 0, total2 = 0;
                double aver1 = 0, aver2 = 0;
                double w1 = 0, w2 = 0;
                //區域 1,2 方差
                double ft1 = 0, ft2 = 0;

                for (int i = 0; i < T; i++)
                {
                    n1 += histogram[i];
                    total1 += histogram[i] * i;
                }
                for (int j = T; j < variances.Length; j++)
                {
                    n2 += histogram[j];
                    total2 += histogram[j] * j;
                }
                w1 = n1 / pixelNumb;
                w2 = n2 / pixelNumb;
                //防止個數為 0，出錯；
                aver1 = (n1 == 0) ? 0 : (total1 / n1);
                aver2 = (n2 == 0) ? 0 : (total2 / n2);
                for (int i = 0; i < T; i++)
                {
                    ft1 += (Math.Pow((i - aver1), 2) * histogram[i]);
                }
                for (int j = T; j < 256; j++)
                {
                    ft2 += (Math.Pow((j - aver2), 2) * histogram[j]);
                }
                ft1 = (n1 == 0) ? 0 : (ft1 / n1);
                ft2 = (n2 == 0) ? 0 : (ft2 / n2);
                variances[T] = w1 * ft1 + w2 * ft2;
            }
            double min = variances[0];
            threshold = 0;
            for (int i = 1; i < variances.Length; i++)
            {
                if (variances[i] < min)
                {
                    min = variances[i];
                    threshold = i;
                }
            }
            return threshold;
        }
        private void otsusMethodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g = new int[w * h *3];
            int[] histogram = new int[256];
            int total = w * h;
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    if (rgb_or_gray==1)
                    {
                        unsafe
                        {
                            fixed (int* f0 = f) fixed (int* g0 = g)
                            {
                                for (int i = 0; i < w * h *3; i +=3)
                                {
                                    double gray = f0[i] * 0.299 + f0[i+1] * 0.587 + f0[i+2] * 0.114;
                                    g[i] = Convert.ToInt16(gray);
                                    g[i+1] = Convert.ToInt16(gray);
                                    g[i+2] = Convert.ToInt16(gray);
                                    histogram[Convert.ToInt16(gray)]++;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < w * h; i++)
                        {
                            histogram[f[i]]++;
                        }
                        g = f;
                    }
                    int t=getOPTThresholding(cF.pBitmap, histogram);
                    unsafe
                    {
                         for (int i = 0; i < w * h *(1+rgb_or_gray*2); i += (1 + rgb_or_gray * 2))
                         {
                             if (g[i]>t)
                             {
                                 g[i] = 255;
                                if (rgb_or_gray == 1)
                                {
                                    g[i + 1] = 255;
                                    g[i + 2] = 255;
                                }
                            }
                             else
                             {
                                 g[i] = 0;
                                if (rgb_or_gray == 1)
                                {
                                    g[i + 1] = 0;
                                    g[i + 2] = 0;
                                }
                            }
                         }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }
            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = new Bitmap(NpBitmap);
            childForm.Show();
        }

        private void zoomInZoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private TaskCompletionSource<float> tcs;
        private TaskCompletionSource<double> tcs_d;
        private Task<float> WaitForChildFormEvent()
        {
            tcs = new TaskCompletionSource<float>();
            zoom.Sending += send_float => {
                tcs.SetResult(0f);
            };
            return tcs.Task;
        }
        private Task<double> WaitForChildFormEvent_d()
        {
            tcs_d = new TaskCompletionSource<double>();
            deg.Sending += send_double => {
                tcs_d.SetResult(0);
            };
            return tcs_d.Task;
        }
        private async void linearInterpolationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    // 讀取原始圖片
                    Bitmap originalImage = new Bitmap(cF.pBitmap);
                    // 設定縮放比例
                    if (zoom.IsDisposed)
                    {
                        zoom = new Zoom_In_and_Out();
                        zoom.MdiParent = this;
                        zoom.Show();
                    }
                    else
                    {
                        zoom.MdiParent = this;
                        zoom.Show();
                    }
                    await WaitForChildFormEvent();
                    t = (float)Convert.ToDouble(zoom.textBox1.Text);
                    // 計算縮放後的圖片尺寸
                    int newWidth = (int)(originalImage.Width * t);
                    int newHeight = (int)(originalImage.Height * t);

                    // 建立新的圖片物件
                    Bitmap resizedImage = new Bitmap(newWidth, newHeight);

                    // 進行線性插值處理
                    using (Graphics graphics = Graphics.FromImage(resizedImage))
                    {
                        graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                        graphics.DrawImage(originalImage, new Rectangle(0, 0, newWidth, newHeight),
                                           new Rectangle(0, 0, originalImage.Width, originalImage.Height),
                                           GraphicsUnit.Pixel);
                    }
                    MSForm childForm = new MSForm();
                    childForm.MdiParent = this;
                    childForm.pf1 = stStripLabel;
                    childForm.pBitmap = new Bitmap(resizedImage);
                    childForm.Show();
                }
            }
        }

        private async void nearestInterpolationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    // 讀取原始圖片
                    Bitmap originalImage = new Bitmap(cF.pBitmap);
                    // 設定縮放比例
                    if (zoom.IsDisposed)
                    {
                        zoom = new Zoom_In_and_Out();
                        zoom.MdiParent = this;
                        zoom.Show();
                    }
                    else
                    {
                        zoom.MdiParent = this;
                        zoom.Show();
                    }
                    await WaitForChildFormEvent();
                    t = (float)Convert.ToDouble(zoom.textBox1.Text);
                    // 計算縮放後的圖片尺寸
                    int newWidth = (int)(originalImage.Width * t);
                    int newHeight = (int)(originalImage.Height * t);

                    // 建立新的圖片物件
                    Bitmap resizedImage = new Bitmap(newWidth, newHeight);

                    // 進行鄰近插值處理
                    for (int y = 0; y < newHeight; y++)
                    {
                        for (int x = 0; x < newWidth; x++)
                        {
                            int srcX = (int)(x / t);
                            int srcY = (int)(y / t);
                            Color pixel = originalImage.GetPixel(srcX, srcY);
                            resizedImage.SetPixel(x, y, pixel);
                        }
                    }
                    MSForm childForm = new MSForm();
                    childForm.MdiParent = this;
                    childForm.pf1 = stStripLabel;
                    childForm.pBitmap = new Bitmap(resizedImage);
                    childForm.Show();
                }
            }
        }
        public Bitmap rotareImageOri(Bitmap Image, double degree)
        {
            int Wo = Image.Width;
            int Lo = Image.Height;
            double Wt = (int)(Math.Abs(Wo * Math.Cos(Math.PI * (degree / 180))) + Math.Abs(Lo * Math.Sin(Math.PI * (degree / 180))) + 0.5);
            double Lt = (int)(Math.Abs(Wo * Math.Sin(Math.PI * (degree / 180))) + Math.Abs(Lo * Math.Cos(Math.PI * (degree / 180))) + 0.5);
            Bitmap rotateImageData = new Bitmap((int)Wt, (int)Lt, PixelFormat.Format24bppRgb);
            for (int y1 = 0; y1 < Lt; y1++)
            {
                for (int x1 = 0; x1 < Wt; x1++)
                {
                    double x = x1 * Math.Cos(Math.PI * (degree / 180)) + y1 * Math.Sin(Math.PI * (degree / 180)) - (Wt - 1) / 2.0 * Math.Cos(Math.PI * (degree / 180)) - (Lt - 1) / 2.0 * Math.Sin(Math.PI * (degree / 180)) + (Wo - 1) / 2.0;
                    double y = -x1 * Math.Sin(Math.PI * (degree / 180)) + y1 * Math.Cos(Math.PI * (degree / 180)) + (Wt - 1) / 2.0 * Math.Sin(Math.PI * (degree / 180)) - (Lt - 1) / 2.0 * Math.Cos(Math.PI * (degree / 180)) + (Lo - 1) / 2.0;
                    if (-0.001 <= x & x <= (Wo - 1) & -0.001 <= y & y <= (Lo - 1))
                    {
                        Color RGB = new Color();

                        int a1 = (int)x;
                        int b1 = (int)y;
                        int a2 = (int)Math.Ceiling(x);
                        int b2 = (int)y;
                        int a3 = (int)x;
                        int b3 = (int)Math.Ceiling(y);
                        int a4 = (int)Math.Ceiling(x);
                        int b4 = (int)Math.Ceiling(y);

                        double xa13 = x - a1;
                        double xa24 = a2 - x;
                        double yb12 = y - b1;
                        double yb34 = b3 - y;

                        if (xa13 != 0 & xa24 != 0 & yb12 != 0 & yb34 != 0) 
                        { 
                            byte R1 = Image.GetPixel(a1, b1).R;
                            byte G1 = Image.GetPixel(a1, b1).G;
                            byte B1 = Image.GetPixel(a1, b1).B;
                            byte R2 = Image.GetPixel(a2, b2).R;
                            byte G2 = Image.GetPixel(a2, b2).G;
                            byte B2 = Image.GetPixel(a2, b2).B;
                            byte R3 = Image.GetPixel(a3, b3).R;
                            byte G3 = Image.GetPixel(a3, b3).G;
                            byte B3 = Image.GetPixel(a3, b3).B;
                            byte R4 = Image.GetPixel(a4, b4).R;
                            byte G4 = Image.GetPixel(a4, b4).G;
                            byte B4 = Image.GetPixel(a4, b4).B;

                            byte R = (byte)((R1 * xa24 + R2 * xa13) * yb34 + (R3 * xa24 + R4 * xa13) * yb12);
                            byte G = (byte)((G1 * xa24 + G2 * xa13) * yb34 + (G3 * xa24 + G4 * xa13) * yb12);
                            byte B = (byte)((B1 * xa24 + B2 * xa13) * yb34 + (B3 * xa24 + B4 * xa13) * yb12);

                            RGB = Color.FromArgb(R, G, B);
                        }
                        else
                        {//对应回原图是整数坐标,直接取Pixel。
                            RGB = Image.GetPixel(a1, b1);
                        }
                        rotateImageData.SetPixel(x1, y1, RGB);
                    }
                }
            }
            return rotateImageData;
        }
        private async void rotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    if (deg.IsDisposed)
                    {
                        deg = new Degree();
                        deg.MdiParent = this;
                        deg.Show();
                    }
                    else
                    {
                        deg.MdiParent = this;
                        deg.Show();
                    }
                    await WaitForChildFormEvent_d();
                    double dd = Convert.ToDouble(deg.textBox1.Text);
                    Bitmap image = rotareImageOri(cF.pBitmap, dd);
                    MSForm childForm = new MSForm();
                    childForm.MdiParent = this;
                    childForm.pf1 = stStripLabel;
                    childForm.pBitmap = new Bitmap(image);
                    childForm.Show();
                }
            }
        }

        private void filterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void avergeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filter.IsDisposed)
            {
                filter = new Filter();
                filter.Sending += Sending;
                filter.MdiParent = this;
                filter._case = "avg";
                filter.pictureBox1.Image = NpBitmap;
                filter.Show();
            }
            else
            {
                filter.pictureBox1.Image = NpBitmap;
                filter._case = "avg";
                filter.MdiParent = this;
                filter.Show();
            }
        }

        private void gaussianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filter.IsDisposed)
            {
                filter = new Filter();
                filter.Sending += Sending;
                filter.MdiParent = this;
                filter._case = "guss";
                filter.pictureBox1.Image = NpBitmap;
                filter.Show();
            }
            else
            {
                filter.pictureBox1.Image = NpBitmap;
                filter._case = "guss";
                filter.MdiParent = this;
                filter.Show();
            }
        }

        private void horizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filter.IsDisposed)
            {
                filter = new Filter();
                filter.Sending += Sending;
                filter._case = "prew_hori";
                filter.MdiParent = this;
                filter.pictureBox1.Image = NpBitmap;
                filter.Show();
            }
            else
            {
                filter.pictureBox1.Image = NpBitmap;
                filter._case = "prew_hori";
                filter.MdiParent = this;
                filter.Show();
            }
        }

        private void verticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filter.IsDisposed)
            {
                filter = new Filter();
                filter.Sending += Sending;
                filter._case = "prew_vert";
                filter.MdiParent = this;
                filter.pictureBox1.Image = NpBitmap;
                filter.Show();
            }
            else
            {
                filter.pictureBox1.Image = NpBitmap;
                filter._case = "prew_vert";
                filter.MdiParent = this;
                filter.Show();
            }
        }

        private void horizontalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (filter.IsDisposed)
            {
                filter = new Filter();
                filter.Sending += Sending;
                filter._case = "sobel_hori";
                filter.MdiParent = this;
                filter.pictureBox1.Image = NpBitmap;
                filter.Show();
            }
            else
            {
                filter.pictureBox1.Image = NpBitmap;
                filter._case = "sobel_hori";
                filter.MdiParent = this;
                filter.Show();
            }
        }

        private void verticalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (filter.IsDisposed)
            {
                filter = new Filter();
                filter.Sending += Sending;
                filter._case = "sobel_vert";
                filter.MdiParent = this;
                filter.pictureBox1.Image = NpBitmap;
                filter.Show();
            }
            else
            {
                filter.pictureBox1.Image = NpBitmap;
                filter._case = "sobel_vert";
                filter.MdiParent = this;
                filter.Show();
            }
        }

        private void mediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filter.IsDisposed)
            {
                filter = new Filter();
                filter.Sending += Sending;
                filter._case = "media";
                filter.MdiParent = this;
                filter.pictureBox1.Image = NpBitmap;
                filter.Show();
            }
            else
            {
                filter.pictureBox1.Image = NpBitmap;
                filter._case = "media";
                filter.MdiParent = this;
                filter.Show();
            }
        }

        private void autofocusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            oFileDlg.CheckFileExists = true;
            oFileDlg.CheckPathExists = true;
            oFileDlg.Title = "Open File - DIP Sample";
            oFileDlg.ValidateNames = true;
            oFileDlg.Filter = "mp4 files (*.mp4)|*.mp4";

            oFileDlg.FileName = "";

            if (oFileDlg.ShowDialog() == DialogResult.OK)
            {
                autoFocus(oFileDlg.FileName);
            }
        }

        private void connectedComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void connectivityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    Bitmap originalImage = new Bitmap(cF.pBitmap);
                    ConnectedComponentLabeling ccl = new ConnectedComponentLabeling();
                    int[][] labeledImage = ccl.LabelComponents(image);
                    double dd = Convert.ToDouble(deg.textBox1.Text);
                    Bitmap image = rotareImageOri(cF.pBitmap, dd);
                    MSForm childForm = new MSForm();
                    childForm.MdiParent = this;
                    childForm.pf1 = stStripLabel;
                    childForm.pBitmap = new Bitmap(originalImage);
                    childForm.Show();
                }
            }
        }

        private void connectivityToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    Bitmap originalImage = new Bitmap(cF.pBitmap);
                    MSForm childForm = new MSForm();
                    childForm.MdiParent = this;
                    childForm.pf1 = stStripLabel;
                    childForm.pBitmap = new Bitmap(originalImage);
                    childForm.Show();
                }
            }
        }

        private void inverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] f;
            int[] g;

            foreach (MSForm cF in MdiChildren)
            {
                if (cF.Focused)
                {
                    int ByteDepth = 0;
                    PixelFormat pixelFormat = new PixelFormat();
                    ColorPalette palette = null;
                    f = bmp2array(cF.pBitmap, ref ByteDepth, ref pixelFormat, ref palette);
                    g = new int[(1+rgb_or_gray*2) * w * h];
                    unsafe
                    {
                        fixed (int* f0 = f) fixed (int* g0 = g)
                        {
                            for (int i = 0; i < w * h * (1 + rgb_or_gray * 2); i += (1 + rgb_or_gray * 2))
                            {
                                g[i] = Math.Abs(255 - f0[i]);
                                if(rgb_or_gray==1)
                                {
                                    g[i + 1] = Math.Abs(255 - f0[i + 1]);
                                    g[i + 2] = Math.Abs(255 - f0[i + 2]);
                                }
                            }
                        }
                    }
                    NpBitmap = array2bmp(g, ByteDepth, pixelFormat, palette);
                    break;
                }
            }

            MSForm childForm = new MSForm();
            childForm.MdiParent = this;
            childForm.pf1 = stStripLabel;
            childForm.pBitmap = new Bitmap(NpBitmap);
            childForm.Show();
        }


        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
