using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3D
{
    public partial class Form1 : Form
    {
        private readonly Camera mainCamera = new Camera();
        private readonly Tetrahedron tetrahedron = new Tetrahedron(0.2f);

        private readonly Matrix4x4 mView = new Matrix4x4(
                    -0.16f, 0.98f, 0, 0,
                    -0.16f, -0.03f, 0.98f, 0,
                    0.986f, 0.16f, 0.16f, -3.041f,
                    0, 0, 0, 1);

        private Matrix4x4 mModel = Matrix4x4.Identity;

        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            var graphics = e.Graphics;
            var brush = new SolidBrush(Color.Black);
            var pen = new Pen(brush);

            int w = canvas.Width;
            int h = canvas.Height;

            var cameraMatrix = mainCamera.GetCameraMatrix(canvas.Width, canvas.Height, 45);

            var vertices = tetrahedron.GetVertices();
            var edges = tetrahedron.GetEdges();

            var points = new List<Point>();
            foreach (var vertex in vertices)
            {
                var m = Matrix4x4.Multiply(Matrix4x4.Multiply(mModel, mView), cameraMatrix);
                var p = Vector3.Transform(vertex, m);
                var scaled = new Point((int)((p.X + 1) * 0.5 * w), (int)((p.Y + 1) * 0.5 * h));
                points.Add(scaled);

                graphics.FillEllipse(brush, scaled.X - 2, scaled.Y - 2, 4, 4);
            }

            foreach (var edge in edges)
            {
                graphics.DrawLine(pen, points[edge.Item1], points[edge.Item2]);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            canvas.Invalidate();
        }
    }
}
