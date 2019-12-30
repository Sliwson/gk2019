using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _3D
{
    class Camera
    {
        private readonly float n = 1;
        private readonly float f = 100;

        public Matrix4x4 GetCameraMatrix(int width, int height, float fov)
        {
            float e = 1f / (float)Math.Tan(fov / 180 * Math.PI / 2);
            float aspect = (float)width / (float)height;

            return new Matrix4x4(e, 0, 0, 0,
                0, e / aspect, 0, 0,
                0, 0, -(f + n) / (f - n), (2 * f * n) / (f - n),
                0, 0, -1, 0);
        }
    }
}
