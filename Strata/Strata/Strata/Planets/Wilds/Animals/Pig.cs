using Makai.Content;
using Microsoft.Xna.Framework.Graphics;
namespace Strata.Planets.Wilds.Animals
{
    class Pig : Animal
    {
        private Texture2D _texture = Load.Texture("Images\\Wilds\\Animals\\Pig");

        protected override Texture2D Texture { get { return _texture; } }
        protected override float Size { get { return 0.7f; } }

        public Pig(Planet plant, int tile)
            : base(plant, tile)
        {
        }
    }
}