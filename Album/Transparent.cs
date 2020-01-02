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
    public partial class Transparent : Form
    {
        FSPhoto FSForm;
        public Transparent()
        {
            InitializeComponent();
            this.Location = new Point(0, 0);
            WindowState = FormWindowState.Maximized;
            ShowInTaskbar = false;
        }

        public void SetMainPhotoForm(FSPhoto value)
        {
            FSForm = value;
        }
    }
}
