﻿using System;
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
        public Form1()
        {
            InitializeComponent();
            grid = new Grid(5, 5, canvas);
        }
        
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            grid.Paint(e.Graphics);
        }
   }
}

