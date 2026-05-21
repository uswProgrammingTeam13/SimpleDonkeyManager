using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SimpleDonkeyManager.controlutils
{
    public partial class ImageViewer : UserControl
    {
        private PictureBox picBox;

        public ImageViewer()
        {
            InitializeComponent();

            picBox = new PictureBox();
            picBox.Dock = DockStyle.Fill;
            picBox.SizeMode = PictureBoxSizeMode.Zoom;
            this.Controls.Add(picBox);
        }

        public void DisplayImage(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                if (picBox.Image != null)
                {
                    var oldImage = picBox.Image;
                    picBox.Image = null;
                    oldImage.Dispose();
                }

                using (var stream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    picBox.Image = Image.FromStream(stream);
                }
            }
        }
    }
}
