using Makai.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Drawing
{
    public class RectangleColor3D
    {
        private VertexPositionColor[] vertices = new VertexPositionColor[4];
        private int[] indices = new int[6];

        public Vector3 TopLeft { get { return vertices[0].Position; } }
        public Vector3 TopRight { get { return vertices[1].Position; } }
        public Vector3 BottomLeft { get { return vertices[2].Position; } }
        public Vector3 BottomRight { get { return vertices[3].Position; } }

        public RectangleColor3D(Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft, Vector3 bottomRight,Color color)
        {
            vertices[0].Position = topLeft;
            vertices[1].Position = topRight;
            vertices[2].Position = bottomLeft;
            vertices[3].Position = bottomRight;
            SetColor(color);
            SetIndices();
        }

        private void SetColor(Color color)
        {
            vertices[0].Color = color;
            vertices[1].Color = color;
            vertices[2].Color = color;
            vertices[3].Color = color;
        }
        private void SetIndices()
        {
            indices[0] = 2;
            indices[1] = 3;
            indices[2] = 0;
            indices[3] = 1;
            indices[4] = 0;
            indices[5] = 3;
        }

        public void Draw(Camera3D cam)
        {
            cam.DrawColor();
            DrawPrimitives();    
        }

        private void DrawPrimitives()
        {
            Common.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, 4, indices, 0, 2);
        }
    }
}