using Makai.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Strata.Planets.Wilds.Plants
{
    class Tree : Plant
    {
        private Texture2D _texture = Load.Texture("Images\\Wilds\\Plants\\Tree");

        protected override Texture2D Texture { get { return _texture; } }
        protected override float Size { get { return 0.7f; } }

        public Tree(Planet plant, int tile)
            : base(plant, tile)
        {
        }
    }
}