﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private PolygonManager polygonManager;
        public Form1()
        {
            InitializeComponent();

            polygonManager = new PolygonManager(canvas);
            polygonManager.InitSample();
            
            var treeHierarchy = new Hierarchy(hierarchy, polygonManager);
            treeHierarchy.Update();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            polygonManager.Draw(graphics);
        }
    }
}
