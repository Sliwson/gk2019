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
        private Hierarchy hierarchyController;

        public Form1()
        {
            InitializeComponent();

            polygonManager = new PolygonManager(canvas, ChangeCursor, ChangeStatusStrip);

            var relationCreator = new RelationCreator(errorLabel, (selectedEdge1, selectedEdge2));
            relationCreator.InitEvents(remove1, remove2, addEqualButton, addPerpendicularButton);            

            hierarchyController = new Hierarchy(hierarchy, polygonManager, relationCreator);
            hierarchyController.Update();

            polygonManager.OnStructureChanged += hierarchyController.HandleHierarchyChange;
            polygonManager.Update();

            addEqualButton.Click += UpdateAll;
            addPerpendicularButton.Click += UpdateAll;

            polygonManager.GetPolygons().Add(Polygon.GetBigSample());
            Algorithm.LineWeight = 1;
            UpdateAll(this, null);
        }

        private void ChangeCursor(Cursor cursor)
        {
            this.Cursor = cursor;
        }

        private void ChangeStatusStrip(string text)
        {
            statusLabel.Text = text;
        }

        private void UpdateAll(object sender, EventArgs a)
        {
            hierarchyController.Update();
            polygonManager.Update();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var value = lineSizeSlider.Value;
            lineSizeLabel.Text = $"Line size: {value}";
            Algorithm.LineWeight = value;
            UpdateAll(this, null);
        }
    }
}
