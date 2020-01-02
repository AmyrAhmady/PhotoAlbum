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
        List<Photo> Album = new List<Photo>();

        public main()
        {
            InitializeComponent();

            Database = new Database(this);
            Database.Load();
        }

        public void AddToControls(Control value)
        {
            Controls.Add(value);
        }

        private void UpdatePictureList()
        {
            ClearAlbum();
            foreach (KeyValuePair<string, string> entry in Images)
            {
                AddToAlbum(entry);
            }
            RenderAlbum(Album.Count);
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

        public void ClearAlbum()
        {
            foreach (Photo pic in Album)
            {
                Controls.Remove(pic.GetPicBox());
            }
                
            Album.Clear();
        }

        public void AddToAlbum(KeyValuePair<string, string> item)
        {
            Photo tempPhoto = new Photo(item, this);
            Album.Add(tempPhoto);
        }

        public void RenderAlbum(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int row = i / 5;
                int column = i % 5;
                Album[i].SetPosition(10 + (column * 100), 40 + (row * 100));
            }
        }

        public void ShowFullScreenPhoto(Image image)
        {
            Transparent BGForm = new Transparent();
            FSPhoto PhotoForm = new FSPhoto(BGForm);
            BGForm.SetMainPhotoForm(PhotoForm);
            PhotoForm.Open(image);
        }
    }
}
