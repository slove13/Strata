using Makai.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Drawing
{
    public class TriangleColor3D
    {
        private VertexPositionColor[] vertices = new VertexPositionColor[3];
        private int[] indices = new int[3];

        public Vector3 TopVertex { get { return vertices[0].Position; } }
        public Vector3 LeftVertex { get { return vertices[1].Position; } }
        public Vector3 RightVertex { get { return vertices[2].Position; } }

        public TriangleColor3D(Vector3 top, Vector3 left, Vector3 right, Color color)
        {
            vertices[0].Position = top;
            vertices[1].Position = left;
            vertices[2].Position = right;
            SetColor(color);
            SetIndices();
        }

        private void SetColor(Color color)
        {
            vertices[0].Color = color;
            vertices[1].Color = color;
            vertices[2].Color = color;
        }
        private void SetIndices()
        {
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
        }

        public void Draw(Camera3D cam)
        {
            cam.DrawColor();
            DrawPrimitives();
        }
        private void DrawPrimitives()
        {
            Common.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, 3, indices, 0, 1);  
        }
    }
}