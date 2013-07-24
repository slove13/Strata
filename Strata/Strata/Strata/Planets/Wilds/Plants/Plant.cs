using Makai.Camera;
using Makai.Drawing;
using Microsoft.Xna.Framework;
namespace Strata.Planets.Wilds.Plants
{
    abstract class Plant : Wild
    {
        public Plant(Planet plant, int tile)
            : base(plant, tile)
        {
        }

        protected override RectangleTexture3D GetRectangle(Camera3D cam)
        {
            Vector3 center = Planet.GetTopLayerCenter(Tile);    // Center of Tile
            Vector3 normal = Height * Planet.GetTopLayerNormal(Tile);    // Surface normal of Tile
            Vector3 view = cam.View.Forward;
            Vector3 offset = (Width / 2) * Vector3.Normalize(Vector3.Cross(center, cam.View.Forward));  // Horizontal offset from center

            Vector3 topLeft = center - offset + normal;
            Vector3 topRight = center + offset + normal;
            Vector3 bottomLeft = center - offset;
            Vector3 bottomRight = center + offset;

            return new RectangleTexture3D(Texture,
                topLeft,
                topRight,
                bottomLeft,
                bottomRight);
        }
    }
}