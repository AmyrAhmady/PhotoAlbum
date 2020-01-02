using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Album.Core
{
    class Photo
    {
        private string imgData;
        private string imgName;
        private PictureBox picBoxControl;
        main App;

        public Photo(KeyValuePair<string, string> item, main app)
        {
            imgName = item.Key;
            imgData = item.Value;
            App = app;

            picBoxControl = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)(picBoxControl)).BeginInit();
            picBoxControl.Location = new Point(-100, -100);
            picBoxControl.Name = item.Key;
            picBoxControl.Size = new Size(90, 90);
            picBoxControl.TabIndex = 3;
            picBoxControl.TabStop = false;
            picBoxControl.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(item.Value)));
            picBoxControl.SizeMode = PictureBoxSizeMode.StretchImage;
            picBoxControl.Click += new EventHandler(this.OnClick);
            App.AddToControls(picBoxControl);
            ((System.ComponentModel.ISupportInitialize)(picBoxControl)).EndInit();
        }

        public void SetPosition(int x, int y)
        {
            picBoxControl.Location = new Point(x, y);
        }

        public PictureBox GetPicBox()
        {
            return picBoxControl;
        }

        public void OnClick(object sender, EventArgs e)
        {
            App.ShowFullScreenPhoto(((PictureBox)sender).Image);
        }
    }
}
