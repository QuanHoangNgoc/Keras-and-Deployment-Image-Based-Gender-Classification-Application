using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenderApp
{
    public class ImageProvider
    {
        private static ImageProvider instance;
        public static ImageProvider Instance
        {
            get { if (instance == null) instance = new ImageProvider(); return ImageProvider.instance; }
            private set { ImageProvider.instance = value; }
        }

        public string loadImage()
        {
            string fullFilePath = string.Empty;
            OpenFileDialog open = new OpenFileDialog(); /// open file dialog
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK) /// OK 
            {
                try
                {
                    /// get fullFilePath 
                    fullFilePath = open.FileName;
                    MessageBox.Show("Upload 100%");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                    return null; 
                }
            }
            return fullFilePath;
        }

        public string ConvertImageToBase64String(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return Convert.ToBase64String(ms.ToArray());
            }
        } 
    } 
}
