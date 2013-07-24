using Makai.Drawing;
using Makai.Graphics;
using Microsoft.Xna.Framework;
using Strata.Planets;
namespace Strata.HUD
{
    class DepthMeter
    {
        private Planet planet;
        private RectangleColor2D[] rectangles;

        private int layerWidth = Display.Height / 6;
        private int layerHeight = Display.Height / 16;
        private int borderThickness = (int)Display.Height / 160;
        private Color borderColor = Color.Black;

        public DepthMeter(Planet planet, int tile)
        {
            this.planet = planet;
            Update(tile);
        }

        public void Draw()
        {
            for (int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i].Draw();
            }
        }

        public void Update(int tile)
        {
            rectangles = new RectangleColor2D[GetTotalVisibleLayers(tile) + MaxLayers + 3];
            for (int depth = 0; depth <= MaxLayers; depth++)
            {
                // Layers
                if (depth < GetTotalVisibleLayers(tile))
                {
                    rectangles[depth] = new RectangleColor2D(
                        planet.GetLayerColor(tile, depth),
                        new Vector2(Location.X + borderThickness, Location.Y + (MaxLayers - 1 - depth) * (layerHeight + borderThickness) + borderThickness),
                        layerWidth,
                        layerHeight);
                }

                // Dividers
                rectangles[GetTotalVisibleLayers(tile) + depth] = new RectangleColor2D(
                    borderColor,
                    new Vector2(Location.X + borderThickness, Location.Y + (MaxLayers - depth) * (layerHeight + borderThickness)),
                    layerWidth,
                    borderThickness);
            }
            // Left Border
            rectangles[GetTotalVisibleLayers(tile) + MaxLayers + 1] = new RectangleColor2D(
                borderColor,
                new Vector2(Location.X, Location.Y),
                borderThickness,
                (layerHeight + borderThickness) * MaxLayers + borderThickness);

            // Right Border
            rectangles[GetTotalVisibleLayers(tile) + MaxLayers + 2] = new RectangleColor2D(
              borderColor,
              new Vector2(Location.X + layerWidth + borderThickness, Location.Y),
              borderThickness,
              (layerHeight + borderThickness) * MaxLayers + borderThickness);
        }
        private int GetTotalVisibleLayers(int tile)
        {
            return planet.GetTotalLayersInTile(tile);
        }
        private int MaxLayers { get { return planet.MaxLayers; } }
        private Vector2 Location
        {
            get
            {
                return new Vector2(
                    Display.Width - (2 * borderThickness + layerWidth),
                    Display.Height / 2 - ((MaxLayers + 1) * borderThickness + layerHeight * MaxLayers) / 2);
            }
        }   // Location of Depth Meter
    }
}