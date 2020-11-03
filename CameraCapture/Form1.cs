using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

using DirectShowLib;

namespace CameraCapture
{
    public partial class Form1 : Form
    {
        private VideoCapture capture= null;
        private DsDevice[] webcams=null;
        private int selectedCameraID = 0;
        //private OpenFileDialog ofd = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void playToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (webcams.Length == 0)
                {
                    throw new Exception("No camera found");
                }
                else if (comboBox1.SelectedItem==null )
                {
                    throw new Exception("Select camera");
                }
                else if (capture!=null)
                {
                    capture.Start();
                }
                else
                {
                    capture = new VideoCapture(selectedCameraID);
                    capture.ImageGrabbed += Capture_ImageGrabbed;
                    capture.Start();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Capture_ImageGrabbed(object sender, EventArgs e)
        {
            try
            {
                
                    Mat m = new Mat();
                    capture.Retrieve(m);
                    pictureBox1.Image = m.ToImage<Bgr, Byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal).ToBitmap();
               
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture!=null)
                {
                    capture.Pause();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture != null)
                {
                    capture.Pause();
                    capture.Dispose();
                    capture = null;
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                    selectedCameraID = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webcams = DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);

            for(int i=0; i<webcams.Length; i++)
            {
                comboBox1.Items.Add(webcams[i].Name);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCameraID = comboBox1.SelectedIndex;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //screenshot
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (capture != null)
                {
                    Mat m = new Mat();
                    capture.Retrieve(m);

                    Screenshot screenShot = new Screenshot(m.ToImage<Bgr, Byte>().Flip(Emgu.CV.CvEnum.FlipType.Horizontal));
                    screenShot.Show();
                }
                    
            }   
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
