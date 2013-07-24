using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Drawing
{
    public class Line2D
    {
        private Color color;
        private float thickness;
        private Vector2 pointA;
        private Vector2 pointB;

        private LineOrigin origin;

        public Line2D(Color color, float thickness, Vector2 pointA, Vector2 pointB)
        {
            this.color = color;
            this.thickness = thickness;
            this.pointA = pointA;
            this.pointB = pointB;
            origin = LineOrigin.Center;
        }
        public Line2D(Color color, float thickness, Vector2 pointA, Vector2 pointB, LineOrigin origin)
        {
            this.color = color;
            this.thickness = thickness;
            this.pointA = pointA;
            this.pointB = pointB;
            this.origin = origin;
        }
        public void Draw()
        {
            Vector2 tangent = pointB - pointA;
            float rotation = (float)Math.Atan2(tangent.Y, tangent.X);
            Vector2 scale = new Vector2(tangent.Length(), thickness);
            Common.Batch.Draw(Common.Pixel, pointA, null, color, rotation, GetOrigin(), scale, SpriteEffects.None, 0f);
        }
        private Vector2 GetOrigin()
        {
            if (origin == LineOrigin.TopLeft)
            {
                return Vector2.Zero;
            }
            if (origin == LineOrigin.TopRight)
            {
                return new Vector2(Common.Pixel.Width, 0);
            }
            if (origin == LineOrigin.BottomLeft)
            {
                return new Vector2(0, Common.Pixel.Height);
            }
            if (origin == LineOrigin.BottomRight)
            {
                return new Vector2(Common.Pixel.Width, Common.Pixel.Height);
            }
            else    // Center
            {
                return new Vector2(Common.Pixel.Width / 2, Common.Pixel.Height / 2);
            }
        }
    }
}
public enum LineOrigin { Center, TopLeft, TopRight, BottomLeft, BottomRight }