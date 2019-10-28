using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common;
using Newtonsoft.Json;

namespace Polygons
{
    public partial class Form1 : Form
    {
        private PolygonManager polygonManager;
        private Hierarchy hierarchyController;
        private RelationCreator relationC;

        public Form1()
        {
            InitializeComponent();

            polygonManager = new PolygonManager(canvas, ChangeCursor, ChangeStatusStrip);

            relationC = new RelationCreator(errorLabel, (selectedEdge1, selectedEdge2));
            relationC.InitEvents(remove1, remove2, addEqualButton, addPerpendicularButton);            

            hierarchyController = new Hierarchy(hierarchy, polygonManager, relationC);
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

        private void saveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = dialog.OpenFile())
                    {
                        var bytes = Encoding.UTF8.GetBytes(polygonManager.ToJson());
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Json files (*.json)|*.json|Text files (*.txt)|*.txt";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    using (var stream = dialog.OpenFile())
                    {
                        var streamReader = new StreamReader(stream);
                        string json = streamReader.ReadToEnd();

                        dynamic encoded = JsonConvert.DeserializeObject(json);

                        var count = polygonManager.GetPolygons().Count();

                        hierarchy.Nodes.Clear();
                        hierarchyController = new Hierarchy(hierarchy, polygonManager, relationC);

                        polygonManager.GetPolygons().Clear();
                        polygonManager.LoadJson(encoded);
                        UpdateAll(this, null);
                    }
                }
            }
        }
    }
}
