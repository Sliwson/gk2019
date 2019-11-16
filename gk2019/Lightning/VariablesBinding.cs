using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lightning
{
    public partial class Form1 : Form 
    {
        private void objectColorPictureBox_Click(object sender, EventArgs e)
        {
            if (objectColorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = objectColorDialog.Color;
                objectColorPictureBox.BackColor = color;
                Variables.ObjectColor.ObjectColor = color;
                canvas.Invalidate();
            }
        }

        private void lightColorPicturebox_Click(object sender, EventArgs e)
        {
            if (lightColorDialog.ShowDialog() == DialogResult.OK)
            {
                var color = lightColorDialog.Color;
                lightColorPictureBox.BackColor = lightColorDialog.Color;
                Variables.Light.LightColor = color;
                canvas.Invalidate();
            }
        }

       private void mSlider_ValueChanged(object sender, EventArgs e)
       {
            mTextbox.Text = mSlider.Value.ToString();
            Variables.Coefficients.M = mSlider.Value;
            canvas.Invalidate();
       }

        private void kdSlider_ValueChanged(object sender, EventArgs e)
        {
            var value = kdSlider.Value / 100.0f;
            kdTextbox.Text = value.ToString();
            Variables.Coefficients.Kd = value;
            canvas.Invalidate();
        }

        private void ksSlider_ValueChanged(object sender, EventArgs e)
        {
            var value = ksSlider.Value / 100.0f;
            ksTextbox.Text = value.ToString();
            Variables.Coefficients.Ks = value;
            canvas.Invalidate();
        }

        private void constColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            Variables.ObjectColor.IsConst = constColorRadio.Checked;
            canvas.Invalidate();
        }

        private void constNormalRadio_CheckedChanged(object sender, EventArgs e)
        {
            Variables.NormalVectors.IsConst = constNormalRadio.Checked;
            canvas.Invalidate();
        }

        private void preciseFillColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (preciseFillColorRadio.Checked)
            {
                Variables.ColorMode = FillColorMode.Precise;
                canvas.Invalidate();
            }
        }

        private void interpolatedFillColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (interpolatedFillColorRadio.Checked)
            {
                Variables.ColorMode = FillColorMode.Interpolated;
                canvas.Invalidate();
            }
        }

        private void hybridFillColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (hybridFillColorRadio.Checked)
            {
                Variables.ColorMode = FillColorMode.Hybrid;
                canvas.Invalidate();
            }
        }

        private void coefficientsRandomRadio_CheckedChanged(object sender, EventArgs e)
        {
            Variables.Coefficients.IsRandom = coefficientsRandomRadio.Checked;
            canvas.Invalidate();
        }

        private void constLightRadio_CheckedChanged(object sender, EventArgs e)
        {
            Variables.Light.IsConst = constLightRadio.Checked;
            canvas.Invalidate();
        }
        private void saveSizeButton_Click(object sender, EventArgs e)
        {
            var xString = xTextBox.Text;
            var yString = yTextBox.Text;

            try
            {
                var x = int.Parse(xString);
                var y = int.Parse(yString);

                if (x < 1 || x > 100 || y < 1 || y > 100)
                    throw new Exception();

                grid.Resize(x, y);
            }
            catch
            {
                MessageBox.Show("Values must be in [1, 100] range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
