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
        public Form1()
        {
            InitializeComponent();

            var polygonManager = new PolygonManager(canvas, ChangeCursor, ChangeStatusStrip);

            var relationCreator = new RelationCreator(errorLabel, (selectedEdge1, selectedEdge2));
            relationCreator.InitEvents(remove1, remove2, addEqualButton, addPerpendicularButton);            

            var treeHierarchy = new Hierarchy(hierarchy, polygonManager, relationCreator);
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
