using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colors
{
    public partial class Form1 : Form
    {
        private BitmapWrapper inputBitmap;
        private BitmapWrapper[] outputBitmaps;
        private Variables variables;

        public Form1()
        {
            InitializeComponent();

            variables = new Variables(ColorRepresentation.HSV);
            colorRepresentation.SelectedIndex = 0;
            predefinedIlumi.SelectedIndex = 5;
            predefinedProfileCombo.SelectedIndex = 0;

            inputBitmap = new BitmapWrapper(Properties.Resources.Flower);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            var result = BitmapWrapper.GetFromFileDialog();
            if (result != null)
            {
                inputBitmap = result;
                inputPicturebox.BackgroundImage = inputBitmap.GetUnderlyingBitmap();
                GC.Collect();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (outputBitmaps == null)
            {
                MessageBox.Show("No output bitmap!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                if (outputBitmaps[i] == null)
                {
                    MessageBox.Show("No input bitmap!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            for (int i = 0; i < 3; i++)
                outputBitmaps[i].SaveToFileDialog($"Save output {i + 1}", $"Bitmap{i+1}");                
        }

        private void separateButton_Click(object sender, EventArgs e)
        {
            if (inputBitmap == null)
            {
                MessageBox.Show("No input bitmap!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var size = inputBitmap.GetSize();
            outputBitmaps = new BitmapWrapper[3];

            for (int i = 0; i < 3; i++)
                outputBitmaps[i] = new BitmapWrapper(size.Width, size.Height);

            if (variables.ColorRepresentation == ColorRepresentation.HSV)
                Transforms.RgbToHsv(inputBitmap, outputBitmaps[0], outputBitmaps[1], outputBitmaps[2]);
            else if (variables.ColorRepresentation == ColorRepresentation.YCbCr)
                Transforms.RgbToYCbCr(inputBitmap, outputBitmaps[0], outputBitmaps[1], outputBitmaps[2]);
            else if (variables.ColorRepresentation == ColorRepresentation.Lab)
            {
                //get lab settings
                var labSettings = new LabSettings
                {
                    RedPrimary = new Chromacity((float)redPrimaryX.Value, (float)redPrimaryY.Value),
                    GreenPrimary = new Chromacity((float)greenPrimaryX.Value, (float)greenPrimaryY.Value),
                    BluePrimary = new Chromacity((float)bluePrimaryX.Value, (float)bluePrimaryY.Value),
                    WhitePoint = new Chromacity((float)whitePointX.Value, (float)whitePointY.Value),
                    Gamma = (float)gamma.Value
                };

                Transforms.RgbToLab(inputBitmap, outputBitmaps[0], outputBitmaps[1], outputBitmaps[2], labSettings);
            }

            output1.BackgroundImage = outputBitmaps[0].ToBitmap();
            output2.BackgroundImage = outputBitmaps[1].ToBitmap();
            output3.BackgroundImage = outputBitmaps[2].ToBitmap();

            var outLabels = variables.GetRepresentationLabels();
            out1box.Text = outLabels[0];
            out2box.Text = outLabels[1];
            out3box.Text = outLabels[2];
            GC.Collect();
        }

        private void grayscaleButton_Click(object sender, EventArgs e)
        {
            var size = inputBitmap.GetSize();
            var outputBitmap = new BitmapWrapper(size.Width, size.Height);

            Transforms.RgbToGrayscale(inputBitmap, outputBitmap);
            inputBitmap = outputBitmap;
            inputPicturebox.BackgroundImage = inputBitmap.ToBitmap();
            GC.Collect();
        }

        private void colorRepresentation_SelectedIndexChanged(object sender, EventArgs e)
        {
            var i = colorRepresentation.SelectedIndex;
            if (i == 0)
                variables.ColorRepresentation = ColorRepresentation.HSV;
            else if (i == 1)
                variables.ColorRepresentation = ColorRepresentation.YCbCr;
            else
                variables.ColorRepresentation = ColorRepresentation.Lab;
        }

        private void predefinedIlumi_SelectedIndexChanged(object sender, EventArgs e)
        {
            var illuminant = (Illuminant)Enum.ToObject(typeof(Illuminant), predefinedIlumi.SelectedIndex);
            var whitePoint = Variables.IlluminantToWhitePoint(illuminant);
            whitePointX.Value = (decimal)whitePoint.X;
            whitePointY.Value = (decimal)whitePoint.Y;
        }

        private void predefinedProfileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var predefinedColorProfile = (ColorProfile)Enum.ToObject(typeof(ColorProfile), predefinedProfileCombo.SelectedIndex);
            var vars = Variables.ColorProfileToLabSettings(predefinedColorProfile);
            predefinedIlumi.SelectedIndex = (int)vars.WhitePoint;

            gamma.Value = (decimal)vars.Gamma;
            redPrimaryX.Value = (decimal)vars.RedPrimary.X;
            redPrimaryY.Value = (decimal)vars.RedPrimary.Y;
            greenPrimaryX.Value = (decimal)vars.GreenPrimary.X;
            greenPrimaryY.Value = (decimal)vars.GreenPrimary.Y;
            bluePrimaryX.Value = (decimal)vars.BluePrimary.X;
            bluePrimaryY.Value = (decimal)vars.BluePrimary.Y;
        }
    }
}
