using Strata.Planets;
using Makai.Camera;
using Makai.Drawing;
using Microsoft.Xna.Framework;
namespace Strata.HUD
{
    class NeighborHighlight
    {
        private Planet planet;
        private TriangleColor3D[] triangles;

        private Color selectedColor = Color.Ivory;
        private Color leftColor = Color.Yellow;
        private Color rightColor = Color.Red;
        private Color centerColor = Color.DeepSkyBlue;

        public NeighborHighlight(Planet planet, int tile)
        {
            this.planet = planet;
            Update(tile);
        }

        public void Draw(Camera3D cam)
        {
            for (int i = 0; i < triangles.Length; i++)
            {
                try
                {
                    triangles[i].Draw(cam);
                }
                catch { }
            }
        }
        public void Update(int tile)
        {
            triangles = new TriangleColor3D[4];
            try
            {
                triangles[0] = planet.GetTopTriangle(tile, selectedColor);
            }
            catch { }
            try
            {
                triangles[1] = planet.GetTopTriangle(planet.GetLeftTile(tile), leftColor);
            }
            catch { }
            try
            {
                triangles[2] = planet.GetTopTriangle(planet.GetRightTile(tile), rightColor);
            }
            catch { }
            try
            {
                triangles[3] = planet.GetTopTriangle(planet.GetCenterTile(tile), centerColor);
            }
            catch { }
        }
    }
}