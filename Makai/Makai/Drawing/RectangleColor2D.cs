using Microsoft.Xna.Framework;
namespace Makai.Drawing
{
    public class RectangleColor2D
    {
        private Color color;
        private Vector2 location;
        private int width;
        private int height;

        private Rectangle Rectangle { get { return new Rectangle((int)location.X, (int)location.Y, width, height); } }

        public RectangleColor2D(Color color, Vector2 location, int width, int height)
        {
            this.color = color;
            this.location = location;
            this.width = width;
            this.height = height;
        }

        public void Draw()
        {
            Common.Batch.Draw(Common.Pixel, Rectangle, color);
        }
    }
}