using Makai.Camera;
using Makai.Graphics;
using Strata.HUD;
using Strata.Planets;
namespace Strata.Tests
{
    class PlanetTest
    {
        private PlanetKeys input = new PlanetKeys();
        private Planet planet;
        private DepthMeter depthMeter;
        private NeighborHighlight neighborHighlight;
        private int frequency = 1;  // Max possible is 228 because of primitives per draw call limit. Split draw calls if more is needed
        private int row;
        private int column;

        private float lightIntensity = 0.9f;

        private int Tile { get { return planet.GetTileIndex(row, column); } }

        public PlanetTest()
        {
            MakePlanet();
        }

        public void DrawHUD()
        {
            depthMeter.Draw();
        }
        public void DrawScene(Camera3D cam)
        {
            planet.Draw(cam, cam.View.Forward, lightIntensity);
            neighborHighlight.Draw(cam);
        }

        public void Update()
        {
            UpdateInput();
        }
        private void UpdateInput()
        {
            // Planet
            if (input.VUp())
            {
                frequency++;
                MakePlanet();
                UpdateMeters();
            }
            if (input.VDown())
            {
                if (frequency > 1)
                {
                    frequency--;
                    MakePlanet();
                    UpdateMeters();
                }
            }

            // Tile
            if (input.RowDown())
            {
                if (row == planet.TotalRows - 1)
                {
                    row = 0;
                }
                else
                {
                    row++;
                }
                column = 0;
                UpdateMeters();

            }
            if (input.RowUp())
            {
                if (row == 0)
                {
                    row = planet.TotalRows - 1;
                }
                else
                {
                    row--;
                }
                column = 0;
                UpdateMeters();
            }
            if (input.ColumnLeft())
            {
                if (column == 0)
                {
                    column = planet.ColumnsInRow(row) - 1;
                }
                else
                {
                    column--;
                }
                UpdateMeters();
            }
            if (input.ColumnRight())
            {
                if (column == planet.ColumnsInRow(row) - 1)
                {
                    column = 0;
                }
                else
                {
                    column++;
                }
                UpdateMeters();
            }

            input.Update();
        }

        private void UpdateMeters()
        {
            depthMeter.Update(Tile);
            neighborHighlight.Update(Tile);
        }

        private void MakePlanet()
        {
            planet = new Earth(frequency);
            row = 0;
            column = 0;
            SetMeters();
        }
        private void SetMeters()
        {
            neighborHighlight = new NeighborHighlight(planet, Tile);
            depthMeter = new DepthMeter(planet, Tile);
        }
        public void Print()
        {
            Display.WindowTitle = frequency + "V ICOSAHEDRON" + " - " + planet.TotalTiles + " TILES - "+ "ROW: " + row + " - " + "COLUMN: " + column + " - " + "TILE: " + Tile;
        }
    }
}