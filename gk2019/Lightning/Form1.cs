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
        private Grid grid;
        private Drawer drawer;
        public Form1()
        {
            InitializeComponent();
            grid = new Grid(5, 5, canvas);
            drawer = new Drawer();
        }
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            for (int y = 0; y < grid.GetSize().Height; y++)
            {
                for (int x = 0; x < 2 * grid.GetSize().Width; x++)
                {
                    drawer.FillPolygon(grid.GetTriangleVertices(x, y), e.Graphics);
                }
            }
            grid.Paint(e.Graphics);
        }
    }
}

