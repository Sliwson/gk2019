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

        public Form1()
        {
            InitializeComponent();
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

            for (int i = 0; i < 3; i++)
                BitmapWrapper.Transform(inputBitmap, outputBitmaps[i], identity);

            output1.BackgroundImage = outputBitmaps[0].ToBitmap();
            output2.BackgroundImage = outputBitmaps[1].ToBitmap();
            output3.BackgroundImage = outputBitmaps[2].ToBitmap();

            output1.Invalidate();
            output2.Invalidate();
            output3.Invalidate();
        }

        private void grayscaleButton_Click(object sender, EventArgs e)
        {

        }

        private void colorRepresentation_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
