namespace DIP
{
    partial class DIPSample
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stStripLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBtoGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitPlaneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b6ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.b7ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inverseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.histogramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avergeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gaussianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prewetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sobelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.verticalToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mediaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otsusMethodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomInZoomOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linearInterpolationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nearestInterpolationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autofocusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectedComponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.connectivityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectivityToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stStripLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 325);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(657, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stStripLabel
            // 
            this.stStripLabel.Name = "stStripLabel";
            this.stStripLabel.Size = new System.Drawing.Size(118, 17);
            this.stStripLabel.Text = "toolStripStatusLabel1";
            this.stStripLabel.Click += new System.EventHandler(this.stStripLabel_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.iPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(657, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // iPToolStripMenuItem
            // 
            this.iPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBtoGrayToolStripMenuItem,
            this.bitPlaneToolStripMenuItem,
            this.inverseToolStripMenuItem,
            this.brightnessToolStripMenuItem,
            this.contrastToolStripMenuItem,
            this.histogramToolStripMenuItem,
            this.filterToolStripMenuItem,
            this.otsusMethodToolStripMenuItem,
            this.zoomInZoomOutToolStripMenuItem,
            this.rotateToolStripMenuItem,
            this.autofocusToolStripMenuItem,
            this.connectedComponentToolStripMenuItem});
            this.iPToolStripMenuItem.Name = "iPToolStripMenuItem";
            this.iPToolStripMenuItem.Size = new System.Drawing.Size(29, 20);
            this.iPToolStripMenuItem.Text = "&IP";
            // 
            // rGBtoGrayToolStripMenuItem
            // 
            this.rGBtoGrayToolStripMenuItem.Name = "rGBtoGrayToolStripMenuItem";
            this.rGBtoGrayToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.rGBtoGrayToolStripMenuItem.Text = "RGBtoGray";
            this.rGBtoGrayToolStripMenuItem.Click += new System.EventHandler(this.rGBtoGrayToolStripMenuItem_Click);
            // 
            // bitPlaneToolStripMenuItem
            // 
            this.bitPlaneToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.b0ToolStripMenuItem,
            this.b1ToolStripMenuItem,
            this.b2ToolStripMenuItem,
            this.b3ToolStripMenuItem,
            this.b4ToolStripMenuItem,
            this.b5ToolStripMenuItem,
            this.b6ToolStripMenuItem,
            this.b7ToolStripMenuItem});
            this.bitPlaneToolStripMenuItem.Name = "bitPlaneToolStripMenuItem";
            this.bitPlaneToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.bitPlaneToolStripMenuItem.Text = "Bit Plane";
            this.bitPlaneToolStripMenuItem.Click += new System.EventHandler(this.bitPlaneToolStripMenuItem_Click);
            // 
            // b0ToolStripMenuItem
            // 
            this.b0ToolStripMenuItem.Name = "b0ToolStripMenuItem";
            this.b0ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b0ToolStripMenuItem.Text = "B0";
            this.b0ToolStripMenuItem.Click += new System.EventHandler(this.b0ToolStripMenuItem_Click);
            // 
            // b1ToolStripMenuItem
            // 
            this.b1ToolStripMenuItem.Name = "b1ToolStripMenuItem";
            this.b1ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b1ToolStripMenuItem.Text = "B1";
            this.b1ToolStripMenuItem.Click += new System.EventHandler(this.b1ToolStripMenuItem_Click);
            // 
            // b2ToolStripMenuItem
            // 
            this.b2ToolStripMenuItem.Name = "b2ToolStripMenuItem";
            this.b2ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b2ToolStripMenuItem.Text = "B2";
            this.b2ToolStripMenuItem.Click += new System.EventHandler(this.b2ToolStripMenuItem_Click);
            // 
            // b3ToolStripMenuItem
            // 
            this.b3ToolStripMenuItem.Name = "b3ToolStripMenuItem";
            this.b3ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b3ToolStripMenuItem.Text = "B3";
            this.b3ToolStripMenuItem.Click += new System.EventHandler(this.b3ToolStripMenuItem_Click);
            // 
            // b4ToolStripMenuItem
            // 
            this.b4ToolStripMenuItem.Name = "b4ToolStripMenuItem";
            this.b4ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b4ToolStripMenuItem.Text = "B4";
            this.b4ToolStripMenuItem.Click += new System.EventHandler(this.b4ToolStripMenuItem_Click);
            // 
            // b5ToolStripMenuItem
            // 
            this.b5ToolStripMenuItem.Name = "b5ToolStripMenuItem";
            this.b5ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b5ToolStripMenuItem.Text = "B5";
            this.b5ToolStripMenuItem.Click += new System.EventHandler(this.b5ToolStripMenuItem_Click);
            // 
            // b6ToolStripMenuItem
            // 
            this.b6ToolStripMenuItem.Name = "b6ToolStripMenuItem";
            this.b6ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b6ToolStripMenuItem.Text = "B6";
            this.b6ToolStripMenuItem.Click += new System.EventHandler(this.b6ToolStripMenuItem_Click);
            // 
            // b7ToolStripMenuItem
            // 
            this.b7ToolStripMenuItem.Name = "b7ToolStripMenuItem";
            this.b7ToolStripMenuItem.Size = new System.Drawing.Size(87, 22);
            this.b7ToolStripMenuItem.Text = "B7";
            this.b7ToolStripMenuItem.Click += new System.EventHandler(this.b7ToolStripMenuItem_Click);
            // 
            // inverseToolStripMenuItem
            // 
            this.inverseToolStripMenuItem.Name = "inverseToolStripMenuItem";
            this.inverseToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.inverseToolStripMenuItem.Text = "Inverse";
            this.inverseToolStripMenuItem.Click += new System.EventHandler(this.inverseToolStripMenuItem_Click);
            // 
            // brightnessToolStripMenuItem
            // 
            this.brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            this.brightnessToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.brightnessToolStripMenuItem.Text = "Brightness";
            this.brightnessToolStripMenuItem.Click += new System.EventHandler(this.brightnessToolStripMenuItem_Click);
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.contrastToolStripMenuItem.Text = "Contrast";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // histogramToolStripMenuItem
            // 
            this.histogramToolStripMenuItem.Name = "histogramToolStripMenuItem";
            this.histogramToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.histogramToolStripMenuItem.Text = "Histogram";
            this.histogramToolStripMenuItem.Click += new System.EventHandler(this.histogramToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.avergeToolStripMenuItem,
            this.gaussianToolStripMenuItem,
            this.prewetToolStripMenuItem,
            this.sobelToolStripMenuItem,
            this.mediaToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.filterToolStripMenuItem.Text = "Filter";
            this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
            // 
            // avergeToolStripMenuItem
            // 
            this.avergeToolStripMenuItem.Name = "avergeToolStripMenuItem";
            this.avergeToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.avergeToolStripMenuItem.Text = "Average";
            this.avergeToolStripMenuItem.Click += new System.EventHandler(this.avergeToolStripMenuItem_Click);
            // 
            // gaussianToolStripMenuItem
            // 
            this.gaussianToolStripMenuItem.Name = "gaussianToolStripMenuItem";
            this.gaussianToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.gaussianToolStripMenuItem.Text = "Gaussian";
            this.gaussianToolStripMenuItem.Click += new System.EventHandler(this.gaussianToolStripMenuItem_Click);
            // 
            // prewetToolStripMenuItem
            // 
            this.prewetToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem,
            this.verticalToolStripMenuItem});
            this.prewetToolStripMenuItem.Name = "prewetToolStripMenuItem";
            this.prewetToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.prewetToolStripMenuItem.Text = "Prewitt";
            // 
            // horizontalToolStripMenuItem
            // 
            this.horizontalToolStripMenuItem.Name = "horizontalToolStripMenuItem";
            this.horizontalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.horizontalToolStripMenuItem.Text = "Horizontal";
            this.horizontalToolStripMenuItem.Click += new System.EventHandler(this.horizontalToolStripMenuItem_Click);
            // 
            // verticalToolStripMenuItem
            // 
            this.verticalToolStripMenuItem.Name = "verticalToolStripMenuItem";
            this.verticalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.verticalToolStripMenuItem.Text = "Vertical";
            this.verticalToolStripMenuItem.Click += new System.EventHandler(this.verticalToolStripMenuItem_Click);
            // 
            // sobelToolStripMenuItem
            // 
            this.sobelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.horizontalToolStripMenuItem1,
            this.verticalToolStripMenuItem1});
            this.sobelToolStripMenuItem.Name = "sobelToolStripMenuItem";
            this.sobelToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.sobelToolStripMenuItem.Text = "Sobel";
            // 
            // horizontalToolStripMenuItem1
            // 
            this.horizontalToolStripMenuItem1.Name = "horizontalToolStripMenuItem1";
            this.horizontalToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.horizontalToolStripMenuItem1.Text = "Horizontal";
            this.horizontalToolStripMenuItem1.Click += new System.EventHandler(this.horizontalToolStripMenuItem1_Click);
            // 
            // verticalToolStripMenuItem1
            // 
            this.verticalToolStripMenuItem1.Name = "verticalToolStripMenuItem1";
            this.verticalToolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.verticalToolStripMenuItem1.Text = "Vertical";
            this.verticalToolStripMenuItem1.Click += new System.EventHandler(this.verticalToolStripMenuItem1_Click);
            // 
            // mediaToolStripMenuItem
            // 
            this.mediaToolStripMenuItem.Name = "mediaToolStripMenuItem";
            this.mediaToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.mediaToolStripMenuItem.Text = "Media";
            this.mediaToolStripMenuItem.Click += new System.EventHandler(this.mediaToolStripMenuItem_Click);
            // 
            // otsusMethodToolStripMenuItem
            // 
            this.otsusMethodToolStripMenuItem.Name = "otsusMethodToolStripMenuItem";
            this.otsusMethodToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.otsusMethodToolStripMenuItem.Text = "Otsu\'s method";
            this.otsusMethodToolStripMenuItem.Click += new System.EventHandler(this.otsusMethodToolStripMenuItem_Click);
            // 
            // zoomInZoomOutToolStripMenuItem
            // 
            this.zoomInZoomOutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.linearInterpolationToolStripMenuItem,
            this.nearestInterpolationToolStripMenuItem});
            this.zoomInZoomOutToolStripMenuItem.Name = "zoomInZoomOutToolStripMenuItem";
            this.zoomInZoomOutToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.zoomInZoomOutToolStripMenuItem.Text = "Zoom In & Zoom Out";
            this.zoomInZoomOutToolStripMenuItem.Click += new System.EventHandler(this.zoomInZoomOutToolStripMenuItem_Click);
            // 
            // linearInterpolationToolStripMenuItem
            // 
            this.linearInterpolationToolStripMenuItem.Name = "linearInterpolationToolStripMenuItem";
            this.linearInterpolationToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.linearInterpolationToolStripMenuItem.Text = "Linear interpolation";
            this.linearInterpolationToolStripMenuItem.Click += new System.EventHandler(this.linearInterpolationToolStripMenuItem_Click);
            // 
            // nearestInterpolationToolStripMenuItem
            // 
            this.nearestInterpolationToolStripMenuItem.Name = "nearestInterpolationToolStripMenuItem";
            this.nearestInterpolationToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            this.nearestInterpolationToolStripMenuItem.Text = "Nearest Interpolation";
            this.nearestInterpolationToolStripMenuItem.Click += new System.EventHandler(this.nearestInterpolationToolStripMenuItem_Click);
            // 
            // rotateToolStripMenuItem
            // 
            this.rotateToolStripMenuItem.Name = "rotateToolStripMenuItem";
            this.rotateToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.rotateToolStripMenuItem.Text = "Rotate";
            this.rotateToolStripMenuItem.Click += new System.EventHandler(this.rotateToolStripMenuItem_Click);
            // 
            // autofocusToolStripMenuItem
            // 
            this.autofocusToolStripMenuItem.Name = "autofocusToolStripMenuItem";
            this.autofocusToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.autofocusToolStripMenuItem.Text = "Autofocus";
            this.autofocusToolStripMenuItem.Click += new System.EventHandler(this.autofocusToolStripMenuItem_Click);
            // 
            // connectedComponentToolStripMenuItem
            // 
            this.connectedComponentToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectivityToolStripMenuItem,
            this.connectivityToolStripMenuItem1});
            this.connectedComponentToolStripMenuItem.Name = "connectedComponentToolStripMenuItem";
            this.connectedComponentToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.connectedComponentToolStripMenuItem.Text = "Connected Component";
            this.connectedComponentToolStripMenuItem.Click += new System.EventHandler(this.connectedComponentToolStripMenuItem_Click);
            // 
            // oFileDlg
            // 
            this.oFileDlg.FileName = "openFileDialog1";
            // 
            // connectivityToolStripMenuItem
            // 
            this.connectivityToolStripMenuItem.Name = "connectivityToolStripMenuItem";
            this.connectivityToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.connectivityToolStripMenuItem.Text = "4-connectivity";
            this.connectivityToolStripMenuItem.Click += new System.EventHandler(this.connectivityToolStripMenuItem_Click);
            // 
            // connectivityToolStripMenuItem1
            // 
            this.connectivityToolStripMenuItem1.Name = "connectivityToolStripMenuItem1";
            this.connectivityToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.connectivityToolStripMenuItem1.Text = "8-connectivity";
            this.connectivityToolStripMenuItem1.Click += new System.EventHandler(this.connectivityToolStripMenuItem1_Click);
            // 
            // DIPSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 347);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DIPSample";
            this.Text = "DIPSample";
            this.Load += new System.EventHandler(this.DIPSample_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel stStripLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iPToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog oFileDlg;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem rGBtoGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitPlaneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b0ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b5ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b6ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem b7ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inverseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem histogramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otsusMethodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomInZoomOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linearInterpolationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nearestInterpolationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avergeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gaussianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prewetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sobelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem horizontalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem verticalToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mediaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autofocusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectedComponentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectivityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectivityToolStripMenuItem1;
    }
}