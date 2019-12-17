using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Album.Core;

namespace Album
{
    public partial class main : Form
    {
        Database Database;
        Dictionary<string, string> Images = new Dictionary<string, string>();

        public main()
        {
            InitializeComponent();
            Database = new Database(this);
            Database.Load();
        }

        private void UpdatePictureList()
        {
            pictureList.Items.Clear();
            foreach (KeyValuePair<string, string> entry in Images)
            {
                pictureList.Items.Add(entry.Key);
            }
        }

        public void UpdateImages(Dictionary<string, string> list)
        {
            Images.Clear();
            Images = list;
            UpdatePictureList();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.ShowDialog();
                byte[] imageArray = File.ReadAllBytes(dialog.FileName);
                string imgName = Path.GetFileName(dialog.FileName);
                string imgBase64 = Convert.ToBase64String(imageArray);
                if (!Images.ContainsKey(imgName))
                    Images[imgName] = imgBase64;
                else
                    throw new InvalidOperationException("duplicate image");

                Database.Add(imgName, imgBase64);
            }
            catch
            {
                MessageBox.Show("Couldn't load image");
            }

            UpdatePictureList();
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                Images.Remove(pictureList.SelectedItem.ToString());
            }
            catch
            {
                MessageBox.Show("Couldn't remove this picture");
            }
            Database.Remove(pictureList.SelectedItem.ToString());
            UpdatePictureList();
            
            foreach (KeyValuePair<string, string> entry in Images)
            {
                pictureBox.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(entry.Value)));
                break;
            }
        }

        private void pictureList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox.Image = Image.FromStream(new MemoryStream(Convert.FromBase64String(Images[pictureList.SelectedItem.ToString()])));
        }
    }
}
