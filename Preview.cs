using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureCapture
{
    
    public partial class Preview : Form
    {
        public string fill;
        //public string pictureName;
        public Preview(string fille)
        {
            fill = fille;
            InitializeComponent();
           

        }

       
        private void Preview_Load(object sender, EventArgs e)
        {
           
            //this.ThePicture = frm.ThePicture;
            //pictureBox1.Image = frm.ThePicture;
            //pictureBox1.Image = System.Drawing.Image.FromFile(@"C:\\" + fill + ".png");
          pictureBox1 .Image = System.Drawing.Image.FromFile(@fill);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1 frm = new Form1();
            Environment.Exit(0);
            this.Close();
            Application.Exit();
            this.FormClosed += new FormClosedEventHandler(delegate { Close(); });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            System.IO.File.Delete(@fill);
            
            this.Close();
        }
    }
}
