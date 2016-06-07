using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.IO;

namespace PictureCapture
{

    public partial class Form1 : Form
    {
        //private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;
        BlobCounter blobCounter = new BlobCounter();
        ColorDialog colorD = new ColorDialog();
        Color color = Color.Black;
        private float hue;
        System.Drawing.Image thePicture;
        public string pictureName; 
        //public bool IsTaken = false;
        
        public System.Drawing.Image ThePicture
        {
            get { return thePicture; }
            set { thePicture = value; }
        }
        public Form1(string filemane)
        {
           pictureName = filemane;
            //IsTaken = istaken;
            InitializeComponent();
           
        }
       
        
        //VideoCaptureDevice videoSource;

        private void getCamList()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                //comboBox1.Items.Clear();
                if (videoDevices.Count == 0) { 
                    throw new ApplicationException();
                }
                else
                {
                    videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
                    videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                    CloseVideoSource();
                    //videoSource.DesiredFrameSize = new Size(160, 120);
                    //videoSource.DesiredFrameRate = 10;
                    videoSource.Start();
                    label2.Text = "устройство работает...";
                    //start.Text = "&Capture";
                    timer1.Enabled = true;
                    //DeviceExist = true;
                }
                
             

            }
            catch (ApplicationException)
            {
                //DeviceExist = false;
                label2.Text = "Нет устройства захвата в системе";
                //comboBox1.Items.Add("No capture device on your system");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            getCamList();
        
        }

        void videoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Cast the frame as Bitmap object and don't forget to use ".Clone()" otherwise
            //you'll probably get access violation exceptions
            pictureBox1.BackgroundImage = (Bitmap)eventArgs.Frame.Clone();
        }

       

        private void btnStart_Click(object sender, EventArgs e)
        {
           

        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
         
              
            var filll = new Mirror(false, true);
            filll.ApplyInPlace(img);
            Graphics g1 = Graphics.FromImage(img);
            Pen pen1 = new Pen(Color.FromArgb(160, 255, 160), 2);
            Pen pen2 = new Pen(Color.Red,2);
           
            //g1.DrawLine(pen1, img.Width, img.Height / 2, 0, img.Height / 2);
            //g1.DrawLine(pen2, 290, 190, 350, 190);
            //Graphics surface;
            //surface = this.CreateGraphics();
            Brush brush = new SolidBrush(Color.FromArgb(100, 255, 128, 255));
            Point[] points = { new Point(305, 190), new Point(335, 190), new Point(319, 160) };
            g1.FillPolygon(brush, points);

            Pen blackPen = new Pen(Color.White, 3);
            blackPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(240, 80, 160, 200);
            g1.DrawLine(blackPen, img.Width / 2, 0, img.Width / 2, img.Width);
            // Draw ellipse to screen.
            g1.DrawEllipse(blackPen, rect);
            Rectangle ellip = new Rectangle(300,200, 40, 20);
            g1.DrawEllipse(blackPen, ellip);
            //g1.DrawRectangle(pen2, 220, 80, 200, 150);
            //g1.DrawPolygon(pen2, 310, 80, 20, 150);
            //g1.DrawRectangle(pen2, 220, 170, 200, 20);
            g1.DrawLine(blackPen, 220, 160, 420, 160);
            //g1.DrawLine(pen2, 300, 220, 340, 220);
            g1.Dispose();
            pictureBox1.Image = img;
           
            Crop filt = new Crop(new Rectangle(180, 80, 280, 240));
            //// apply the filter
            Bitmap img2 = (Bitmap)eventArgs.Frame.Clone();
            Bitmap newImage = filt.Apply(img2);
            filll.ApplyInPlace(newImage);
            pictureBox2.Image  = newImage;
        }
          
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }
        //close the device safely
        private void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }

        //get total received frame at 1 second tick
       

        //prevent sudden close while device is running
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseVideoSource();
            Application.Exit();
            //this.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCamList();
            
            //if (start.Text == "&Capture" && videoSource.IsRunning)
            //{
            //    timer1.Enabled = false;
            //    CloseVideoSource();
            //    label2.Text = "Device stopped.";
            //    //start.Text = "&Start Camera";
            //}
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
           
            //do processing here
            //pictureBox1.Image = img;
            label2.Text = "устройство работает... " + videoSource.FramesReceived.ToString() + " FPS";
          
        }
       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            colorD.ShowDialog();
            color = (Color)colorD.Color;
            hue = color.GetHue();

        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                //OnOff = false;
                timer1.Enabled = false;
                CloseVideoSource();
                label2.Text = "устройство остановлено.";
                //start.Text = "&Start Camera";
                //Bitmap varBmp1 = new Bitmap(pictureBox2.Image);
               
               
                
                //if (MessageBox.Show("Do you want to save?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                    //string mySavedName = ;
                Bitmap varBmp = new Bitmap(pictureBox2.Image);
                Bitmap newBitmap = new Bitmap(varBmp);
                //MessageBox.Show(mySavedName);

                //varBmp.Save(@"C:\\" + pictureName + ".png", ImageFormat.Png);
                    varBmp.Save(@pictureName, ImageFormat.Jpeg);
                    //Now Dispose to free the memory
                    varBmp.Dispose();
                    varBmp = null;
                
                //this.Close();
                //}
                //else
                //{
                
                Preview pre = new Preview(pictureName);
               // thePicture = System.Drawing.Image.FromFile("C:\\aaa.png");

                pre.ShowDialog();
                getCamList();
                //}
                //;
                
                
                //SaveFileDialog dialog = new SaveFileDialog();
                //if (dialog.ShowDialog() == DialogResult.OK)
                //{
                //    varBmp.Save("myfile.png", ImageFormat.Png);
                //}


            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseVideoSource();
            Application.Exit();
            //this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                CloseVideoSource();
                Application.Exit();
                //this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
