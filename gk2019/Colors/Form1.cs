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

        }

        private void separateButton_Click(object sender, EventArgs e)
        {

        }

        private void grayscaleButton_Click(object sender, EventArgs e)
        {

        }

        private void colorRepresentation_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
