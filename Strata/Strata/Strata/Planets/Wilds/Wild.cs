using Makai.Camera;
using Makai.Drawing;
using Microsoft.Xna.Framework.Graphics;
namespace Strata.Planets.Wilds
{
    abstract class Wild
    {
        protected Planet Planet { get; private set; }
        protected int Tile { get; private set; }

        protected abstract Texture2D Texture { get; }
        protected abstract float Size { get; }
        protected float Width { get { return Size/Planet.Frequency; } }
        protected float Height { get { return Texture.Width / Texture.Height * Size / Planet.Frequency; } }

        public Wild(Planet planet, int tile)
        {
            Planet = planet;
            Tile = tile;
        }

        public void Draw(Camera3D cam)
        {
            GetRectangle(cam).Draw(cam);
        }

        protected abstract RectangleTexture3D GetRectangle(Camera3D cam);
    }
}