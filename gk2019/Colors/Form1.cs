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

            inputBitmap = new BitmapWrapper(Properties.Resources.Flower);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            var result = BitmapWrapper.GetFromFileDialog();
            if (result != null)
            {
                inputBitmap = result;
                inputPicturebox.BackgroundImage = inputBitmap.GetUnderlyingBitmap();
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

            Func<Color, Color> identity = (Color c) => { return c; };
            var size = inputBitmap.GetSize();
            outputBitmaps = new BitmapWrapper[3];

            for (int i = 0; i < 3; i++)
                outputBitmaps[i] = new BitmapWrapper(size.Width, size.Height);

            if (variables.ColorRepresentation == ColorRepresentation.HSV)
                Transforms.RgbToHsv(inputBitmap, outputBitmaps[0], outputBitmaps[1], outputBitmaps[2]);

            output1.BackgroundImage = outputBitmaps[0].ToBitmap();
            output2.BackgroundImage = outputBitmaps[1].ToBitmap();
            output3.BackgroundImage = outputBitmaps[2].ToBitmap();

            var outLabels = variables.GetRepresentationLabels();
            out1box.Text = outLabels[0];
            out2box.Text = outLabels[1];
            out3box.Text = outLabels[2];
        }

        private void grayscaleButton_Click(object sender, EventArgs e)
        {

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
    }
}
