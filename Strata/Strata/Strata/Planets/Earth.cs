using System;
using Strata.Planets.Wilds.Animals;
using Strata.Planets.Wilds.Plants;
namespace Strata.Planets
{
    class Earth : Planet
    {
        public override int MaxLayers { get { return 5; } }
        private Random rand = new Random();

        public Earth(int frequency)
            : base(frequency)
        {
        }

        protected override void SetLayers()
        {
            //AddExampleLayers();
            SetMantle();
        }

        private void SetMantle()
        {
            for (int tile = 0; tile< TotalTiles; tile++)
            {
                AddLayer(Layer.Lava, tile);
            }
        }
        private void AddExampleLayers()
        {
            for (int row = 0; row < TotalRows; row++)
            {
                for (int column = 0; column < ColumnsInRow(row); column++)
                {
                    for (int i = 0; i < rand.Next(MinLayers, MaxLayers + 1); i++)
                    {
                        Layer layer = (Layer)rand.Next(TotalLayerTypes);
                        AddLayer(layer, row, column);
                    }
                    AddExampleWilds(GetTileIndex(row, column));
                }
            }
        }
        private void AddExampleWilds(int tile)
        {
            int plantTypes = 2;
            int pigFrequency = 10;

            if (GetTopLayer(tile) == Layer.Dirt)
            {
                int plant = rand.Next(plantTypes);
                if (plant == 0)
                {
                    AddPlant(new Grass(this, tile));
                    int pig = rand.Next(pigFrequency);
                    if (pig == 0)
                    {
                        AddAnimal(new Pig(this, tile));
                    }
                }
                if (plant == 1)
                {
                    AddPlant(new Tree(this, tile));
                }
            }
        }
    }
}