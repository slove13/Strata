using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Drawing
{
    public class RectangleTexture2D
    {
        private Texture2D texture;
        private Color color = Color.White;
        private Vector2 location;
        private int width;
        private int height;

        private Rectangle Rectangle { get { return new Rectangle((int)location.X, (int)location.Y, width, height); } }

        public RectangleTexture2D(Texture2D texture, Vector2 location)
        {
            this.texture = texture;
            this.location = location;
            width = texture.Width;
            height = texture.Height;
        }
        public RectangleTexture2D(Texture2D texture, Color color, Vector2 location)
        {
            this.texture = texture;
            this.color = color;
            this.location = location;
            width = texture.Width;
            height = texture.Height;
        }

        public RectangleTexture2D(Texture2D texture, Vector2 location, int width, int height)
        {
            this.texture = texture;
            this.location = location;
            this.width = width;
            this.height = height;
        }
        public RectangleTexture2D(Texture2D texture, Color color, Vector2 location, int width, int height)
        {
            this.texture = texture;
            this.color = color;
            this.location = location;
            this.width = width;
            this.height = height;
        }

        public void Draw()
        {
            Common.Batch.Draw(texture, Rectangle, color);
        }
    }
}