using System;
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

            polygonManager = new PolygonManager(canvas, ChangeCursor, ChangeStatusStrip);

            var treeHierarchy = new Hierarchy(hierarchy, polygonManager);
            treeHierarchy.Update();

            polygonManager.OnStructureChanged += treeHierarchy.HandleHierarchyChange;
            polygonManager.Update();
        }

        private void ChangeCursor(Cursor cursor)
        {
            this.Cursor = cursor;
        }

        private void ChangeStatusStrip(string text)
        {
            statusLabel.Text = text;
        }
    }
}
