using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.Structure;

namespace CameraCapture
{
    public partial class Screenshot : Form
    {
        private Image<Bgr, byte> image = null;
        private string fileName = string.Empty;
        public Screenshot(Image<Bgr, byte> image)

        {
            this.image = image;
            InitializeComponent();
        }

        private void Screenshot_Load(object sender, EventArgs e)
        {
            fileName=$"WCSS_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Second}_{DateTime.Now.Year}.jpg";
            pictureBox1.Image = image.ToBitmap();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image.Save(fileName, ImageFormat.Jpeg);
                if (File.Exists(fileName))
                {
                    Close();
                }
                else
                {
                    throw new Exception("File was not save");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
