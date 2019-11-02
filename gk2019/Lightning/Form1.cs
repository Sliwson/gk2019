using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightning
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void objectColorPictureBox_Click(object sender, EventArgs e)
        {
            if (objectColorDialog.ShowDialog() == DialogResult.OK)
                objectColorPictureBox.BackColor = objectColorDialog.Color;
        }

        private void lightColorPicturebox_Click(object sender, EventArgs e)
        {
            if (lightColorDialog.ShowDialog() == DialogResult.OK)
                lightColorPictureBox.BackColor = lightColorDialog.Color;
        }
    }
}
