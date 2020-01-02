using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Album
{
    public partial class FSPhoto : Form
    {
        Form BGForm;
        public FSPhoto(Form background)
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
            WindowState = FormWindowState.Maximized;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            picBox.BackColor = Color.Transparent;
            BGForm = background;
        }

        public void Open(Image image)
        {
            BGForm.Show();
            picBox.Image = image;
            Show();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FSPhoto_FormClosed(object sender, FormClosedEventArgs e)
        {
            BGForm.Close();
        }

        protected override void OnPaintBackground(PaintEventArgs e) { /* Ignore */ }
    }
}
