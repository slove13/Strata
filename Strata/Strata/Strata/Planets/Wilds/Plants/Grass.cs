using Makai.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Strata.Planets.Wilds.Plants
{
    class Grass : Plant
    {
        private Texture2D _texture = Load.Texture("Images\\Wilds\\Plants\\Grass");

        protected override Texture2D Texture { get { return _texture; } }
        protected override float Size { get { return 0.5f; } }

        public Grass(Planet plant, int tile)
            : base(plant, tile)
        {
        }
    }
}