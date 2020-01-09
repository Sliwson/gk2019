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
           -1, 0, 0, 0,
           0, 0.707f, 0.707f, 0,
           0, 0.707f, -0.707f, -4.243f,
           0, 0, 0, 1); 


        private float time = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            var cameraMatrix = mainCamera.GetCameraMatrix(canvas.Width, canvas.Height, fov.Value);
            var modelMatrix = GetModelMatrix();

            var m = Matrix4x4.Multiply(Matrix4x4.Multiply(modelMatrix, mView), cameraMatrix);
            var points = TransformVertices(tetrahedron.GetVertices(), m);
            Draw(points, tetrahedron.GetEdges(), e.Graphics);
        }

        private Matrix4x4 GetModelMatrix()
        {
            Matrix4x4 model = Matrix4x4.Identity;
            model.M11 = (float)Math.Cos(time);
            model.M12 = (float)-Math.Sin(time);
            model.M21 = (float)Math.Sin(time);
            model.M22 = (float)Math.Cos(time);
            return model;
        }

        private List<Point> TransformVertices(List<Vector3> vertices, Matrix4x4 transformMatrix)
        {
            var points = new List<Point>();
            
            int w = canvas.Width;
            int h = canvas.Height;

            foreach (var vertex in vertices)
            {
                var p = Vector3.Transform(vertex, transformMatrix);
                var scaled = new Point((int)((p.X + 1) * 0.5 * w), (int)((p.Y + 1) * 0.5 * h));
                points.Add(scaled);
            }

            return points;
        }

        private void Draw(List<Point> vertices, List<(int, int)> edges, Graphics graphics)
        {
            var brush = new SolidBrush(Color.Black);
            var pen = new Pen(brush);
            
            foreach (var vertex in vertices)
            {
                graphics.FillEllipse(brush, vertex.X - 2, vertex.Y - 2, 4, 4);
            }

            foreach (var edge in edges)
            {
                graphics.DrawLine(pen, vertices[edge.Item1], vertices[edge.Item2]);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            canvas.Invalidate();
            time += (float)timer.Interval / 1000f;
        }
    }
}
