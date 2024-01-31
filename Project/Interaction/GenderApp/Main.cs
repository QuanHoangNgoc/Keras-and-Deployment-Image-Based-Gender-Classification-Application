using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenderApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            return; 
        }

        public void showImage(string fullFilePath)
        {
            try
            {
                /// load image 
                Image image = Image.FromFile(fullFilePath);
                /// resize image 
                image = new Bitmap(image, pictureBoxImage.Width, pictureBoxImage.Height); 
                /// display image 
                pictureBoxImage.Image = image;
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Image not found", "Warning"); 
                MessageBox.Show(ex.Message, "Error");
                return;
            }
        }

        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            string fullFilePath = ImageProvider.Instance.loadImage(); 
            showImage(fullFilePath);

            string B64mes = ImageProvider.Instance.ConvertImageToBase64String(pictureBoxImage.Image);
            /// optional 
            string url_connect = "http://192.168.1.11:6789/model_predict"; 

            string respond = ServerProvider.Instance.sendPOST(url_connect, "imageB64", B64mes); 

            textBoxPredict.Text = respond;
            return; 
        }
    }
}
