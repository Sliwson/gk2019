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
            {
                var color = objectColorDialog.Color;
                objectColorPictureBox.BackColor = color;
                Variables.ObjectColor.ObjectColor = color;
            }
        }

        private void lightColorPicturebox_Click(object sender, EventArgs e)
        {
            if (lightColorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = lightColorDialog.Color;
                lightColorPictureBox.BackColor = lightColorDialog.Color;
                Variables.Light.LightColor = color;
            }
        }

       private void mSlider_ValueChanged(object sender, EventArgs e)
       {
            mTextbox.Text = mSlider.Value.ToString();
            Variables.Coefficients.M = mSlider.Value;
       }

        private void kdSlider_ValueChanged(object sender, EventArgs e)
        {
            var value = kdSlider.Value / 100.0f;
            kdTextbox.Text = value.ToString();
            Variables.Coefficients.Kd = value;
        }

        private void ksSlider_ValueChanged(object sender, EventArgs e)
        {
            var value = ksSlider.Value / 100.0f;
            ksTextbox.Text = value.ToString();
            Variables.Coefficients.Ks = value;
        }
    }
}

