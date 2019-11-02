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

       private void mSlider_ValueChanged(object sender, EventArgs e)
       {
            mTextbox.Text = mSlider.Value.ToString();
       }

        private void kdSlider_ValueChanged(object sender, EventArgs e)
        {
            var value = kdSlider.Value / 100.0;
            kdTextbox.Text = value.ToString();
        }

        private void ksSlider_ValueChanged(object sender, EventArgs e)
        {
            var value = ksSlider.Value / 100.0;
            ksTextbox.Text = value.ToString();
        }
    }
}
