using Makai.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Drawing
{
    public class Line3D
    {
        private VertexPositionColor[] verts = new VertexPositionColor[2];

        public Line3D(Color color, Vector3 pointA, Vector3 pointB)
        {
            verts[0].Position = pointA;
            verts[1].Position = pointB;
            SetColor(color);
        }

        private void SetColor(Color color)
        {
            verts[0].Color = color;
            verts[1].Color = color;
        }

        public void Draw(Camera3D cam)
        {
            cam.DrawColor();
            Common.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, 1);
        }
        public void Draw(Color color, Camera3D cam)
        {
            SetColor(color);
            cam.DrawColor();
            Common.Graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, verts, 0, 1);
        }
    }
}