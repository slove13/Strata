using System;
using System.Collections.Generic;
using Strata.Planets.Wilds.Animals;
using Strata.Planets.Wilds.Plants;
using Makai;
using Makai.Camera;
using Makai.Content;
using Makai.Drawing;
using Makai.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Strata.Planets
{
    abstract class Planet
    {
        private GeodesicSphere shape;

        public int TotalTiles { get { return shape.TotalTiles; } }
        public int Frequency { get { return shape.Frequency; } }

        public Planet(int frequency)
        {
            shape = new GeodesicSphere(frequency);
            layers = new Layer[TotalTiles][];

            SetLayers();
            SetMeshes();
        }   

        #region Drawing
        public void Draw(Camera3D cam)
        {
            DrawLayers(cam);
            DrawWilds(cam);
        }
        public void Draw(Camera3D cam, Vector3 lightDirection, float lightIntensity)
        {
            DrawLayers(cam, lightDirection, lightIntensity);
            DrawWilds(cam);
        }
        private void DrawLayers(Camera3D cam)
        {
            foreach (TriangleColorMesh mesh in sideMeshes)
            {
                if (mesh != null)
                {
                    mesh.Draw(cam);
                }
            }
            foreach (TriangleTextureMesh mesh in topMeshes)
            {
                if (mesh != null)
                {
                    mesh.Draw(cam);
                }
            }
            wireMesh.Draw(cam);
        }
        private void DrawLayers(Camera3D cam, Vector3 lightDirection, float lightIntensity)
        {
            foreach (TriangleColorMesh mesh in sideMeshes)
            {
                if (mesh != null)
                {
                    mesh.Draw(cam);
                }
            }
            foreach (TriangleTextureMesh mesh in topMeshes)
            {
                if (mesh != null)
                {
                    mesh.Draw(cam, lightDirection, lightIntensity);
                }
            }
            wireMesh.Draw(cam);
        }
        private void DrawWilds(Camera3D cam)
        {
            foreach (Plant plant in plants)
            {
                plant.Draw(cam);
            }
            foreach (Animal animal in animals)
            {
                animal.Draw(cam);
            }
        }
        #endregion

        #region Tiles
        public int TotalRows { get { return shape.TotalRows; } }
        public int ColumnsInRow(int row)
        {
            return shape.ColumnsInRow(row);
        }
        public int GetLeftTile(int tile)
        {
            return shape.GetLeftTile(tile);
        }
        public int GetRightTile(int tile)
        {
            return shape.GetRightTile(tile);
        }
        public int GetCenterTile(int tile)
        {
            return shape.GetCenterTile(tile);
        }
        protected bool TilePointsUp(int tile)
        {
            return shape.TilePointsUp(tile);
        }
        #endregion

        #region Layers
        private Layer[][] layers;   // [tile] [depth]
        private float layerHeight = 0.075f;
        public int MinLayers { get { return 1; } }
        public abstract int MaxLayers { get; }
        public static int TotalLayerTypes { get { return Enum.GetNames(typeof(Layer)).Length; } }

        private List<Plant> plants = new List<Plant>();
        private List<Animal> animals = new List<Animal>();

        protected abstract void SetLayers();
        protected void AddLayer(Layer layer, int tile)
        {
            if (layers[tile] == null)
            {
                layers[tile] = new Layer[1];
                layers[tile][0] = layer;
            }
            else if (layers[tile].Length < MaxLayers)
            {
                Layer[] temp = layers[tile];

                layers[tile] = new Layer[temp.Length + 1];
                for (int depth = 0; depth < temp.Length; depth++)
                {
                    layers[tile][depth] = temp[depth];
                }
                layers[tile][temp.Length] = layer;
            }
        }
        protected void AddLayer(Layer layer, int row, int column)
        {
            AddLayer(layer, GetTileIndex(row, column));
        }
        protected void AddPlant(Plant plant)
        {
            plants.Add(plant);
        }
        protected void AddAnimal(Animal animal)
        {
            animals.Add(animal);
        }

        public int GetTileIndex(int row, int column)
        {
            return shape.GetTileIndex(row, column);
        }
        public int GetTotalLayersInTile(int tile)
        {
            return layers[tile].Length;
        }

        public int GetTopLayerIndex(int tile)
        {
            return GetTotalLayersInTile(tile) - 1;
        }
        public Layer GetTopLayer(int tile)
        {
            return layers[tile][GetTopLayerIndex(tile)];
        }
        public Vector3 GetTopLayerCenter(int tile)
        {
            Vector3[] vertices = GetTopVertices(tile, GetTopLayerIndex(tile));
            return MathLib.Center(vertices[0], vertices[1], vertices[2]);
        }
        public Vector3 GetTopLayerNormal(int tile)
        {
            Vector3[] vertices = GetTopVertices(tile, GetTopLayerIndex(tile));
            return MathLib.NormalizedNormal(vertices[0], vertices[1], vertices[2]);
        }
        public Color GetLayerColor(Layer layer)
        {
            if (layer == Layer.Lava)
            {
                return lavaColor;
            }
            if (layer == Layer.Rock)
            {
                return rockColor;
            }
            if (layer == Layer.Dirt)
            {
                return dirtColor;
            }
            if (layer == Layer.Sand)
            {
                return sandColor;
            }
            if (layer == Layer.Water)
            {
                return waterColor;
            }
            else return Color.Transparent;
        }
        public Color GetLayerColor(int layerTypeIndex)
        {
            Layer layer = (Layer)layerTypeIndex;
            return GetLayerColor(layer);
        }
        public Color GetLayerColor(int tile, int depth)
        {
            Layer layer = layers[tile][depth];
            return GetLayerColor(layer);
        }
        public TriangleColor3D GetTopTriangle(int tile, Color color)
        {
            Vector3[] vertices = GetTopVertices(tile, GetTopLayerIndex(tile));
            return new TriangleColor3D(vertices[0], vertices[1], vertices[2], color);
        }
        #endregion

        #region Meshes
        private TriangleTextureMesh[] topMeshes;
        private TriangleColorMesh[] sideMeshes;
        private LineMesh wireMesh;

        private Texture2D lavaTop = Load.Texture("Images\\Layers\\Lava");
        private Texture2D rockTop = Load.Texture("Images\\Layers\\Rock");
        private Texture2D dirtTop = Load.Texture("Images\\Layers\\Dirt");
        private Texture2D sandTop = Load.Texture("Images\\Layers\\Sand");
        private Texture2D waterTop = Load.Texture("Images\\Layers\\Water");

        private static int opacity = 235;  // 0 is total transparency. 255 is total opacity
        private Color lavaColor = new Color(255, 121, 57, opacity);
        private Color rockColor = new Color(100, 88, 90, opacity);
        private Color dirtColor = new Color(56, 49, 47, opacity);
        private Color sandColor = new Color(214, 197, 132, opacity);
        private Color waterColor = new Color(105, 210, 231, opacity);

        private void SetMeshes()
        {
            topMeshes = new TriangleTextureMesh[TotalLayerTypes];
            List<Vector3>[] topVertices = new List<Vector3>[TotalLayerTypes];
            List<int>[] topIndices = new List<int>[TotalLayerTypes];
            int[] topOffset = new int[TotalLayerTypes];

            sideMeshes = new TriangleColorMesh[TotalLayerTypes];
            List<Vector3>[] sideVertices = new List<Vector3>[TotalLayerTypes];
            List<int>[] sideIndices = new List<int>[TotalLayerTypes];
            int[] sideOffset = new int[TotalLayerTypes];

            List<Vector3> wireVertices = new List<Vector3>();
            List<int> wireIndices = new List<int>();
            int wireOffset = 0;
            Color wireColor = Color.LemonChiffon;

            for (int type = 0; type < TotalLayerTypes; type++)
            {
                topVertices[type] = new List<Vector3>();
                topIndices[type] = new List<int>();
                sideVertices[type] = new List<Vector3>();
                sideIndices[type] = new List<int>();
            }

            for (int tile = 0; tile < TotalTiles; tile++)
            {
                for (int depth = 0; depth < layers[tile].Length; depth++)
                {
                    for (int type = 0; type < TotalLayerTypes; type++)
                    {
                        if (layers[tile][depth] == (Layer)type)
                        {
                            // Set Top Triangle
                            topVertices[type].AddRange(GetTopVertices(tile, depth));
                            topIndices[type].AddRange(GetTopIndices(topOffset[type]));
                            topOffset[type] += GetTopVertices(tile, depth).Length;

                            // Set Side Squares
                            sideVertices[type].AddRange(GetSideVertices(tile, depth));
                            sideIndices[type].AddRange(GetSideIndices(sideOffset[type]));
                            sideOffset[type] += GetSideVertices(tile, depth).Length;

                            // Set Layer Dividing Line
                            wireVertices.AddRange(GetTopVertices(tile, depth));
                            wireIndices.AddRange(GetWireIndices(wireOffset));
                            wireOffset += GetTopVertices(tile, depth).Length;
                        }
                    }
                }
            }

            for (int type = 0; type < TotalLayerTypes; type++)
            {
                if (topVertices[type].ToArray().Length > 0)
                {
                    topMeshes[type] = new TriangleTextureMesh(topVertices[type].ToArray(), topIndices[type].ToArray(), GetTopTexture(type));
                    sideMeshes[type] = new TriangleColorMesh(sideVertices[type].ToArray(), sideIndices[type].ToArray(), GetLayerColor(type));
                }
            }
            wireMesh = new LineMesh(wireVertices.ToArray(), wireIndices.ToArray(), wireColor);
        }
        private Texture2D GetTopTexture(int layerTypeIndex)
        {
            Layer layer = (Layer)layerTypeIndex;
            if (layer == Layer.Lava)
            {
                return lavaTop;
            }
            if (layer == Layer.Rock)
            {
                return rockTop;
            }
            if (layer == Layer.Dirt)
            {
                return dirtTop;
            }
            if (layer == Layer.Sand)
            {
                return sandTop;
            }
            if (layer == Layer.Water)
            {
                return waterTop;
            }
            else return Common.Pixel;
        }
        private Vector3[] GetTopVertices(int tile, int depth)
        {
            Vector3[] vertices = new Vector3[3];
            vertices[0] = shape.GetTopVertex(tile) + shape.GetVertexNormal(shape.GetTopVertex(tile)) * layerHeight * depth;    // High Top
            vertices[1] = shape.GetLeftVertex(tile) + shape.GetVertexNormal(shape.GetLeftVertex(tile)) * layerHeight * depth;   // High Left
            vertices[2] = shape.GetRightVertex(tile) + shape.GetVertexNormal(shape.GetRightVertex(tile)) * layerHeight * depth;   // High Right
            return vertices;
        }
        private Vector3[] GetSideVertices(int tile, int depth)
        {
            Vector3[] vertices = new Vector3[6];
            vertices[0] = shape.GetTopVertex(tile) + shape.GetVertexNormal(shape.GetTopVertex(tile)) * layerHeight * depth;    // High Top
            vertices[1] = shape.GetLeftVertex(tile) + shape.GetVertexNormal(shape.GetLeftVertex(tile)) * layerHeight * depth;   // High Left
            vertices[2] = shape.GetRightVertex(tile) + shape.GetVertexNormal(shape.GetRightVertex(tile)) * layerHeight * depth;   // High Right
            vertices[3] = shape.GetTopVertex(tile) + shape.GetVertexNormal(shape.GetTopVertex(tile)) * layerHeight * (depth - 1);    // Low Top
            vertices[4] = shape.GetLeftVertex(tile) + shape.GetVertexNormal(shape.GetLeftVertex(tile)) * layerHeight * (depth - 1);     // Low Left
            vertices[5] = shape.GetRightVertex(tile) + shape.GetVertexNormal(shape.GetRightVertex(tile)) * layerHeight * (depth - 1);      // Low Right
            return vertices;
        }
        private int[] GetTopIndices(int offset)
        {
            int[] indices = new int[3];

            // Top Triangle
            indices[0] = 0 + offset; // High Top
            indices[1] = 1 + offset; // High Left
            indices[2] = 2 + offset; // High Right

            return indices;
        }
        private int[] GetSideIndices(int offset)
        {
            int[] indices = new int[18];

            // Side Square Left
            indices[0] = 0 + offset; // High Top
            indices[1] = 3 + offset; // Low Top
            indices[2] = 1 + offset; // High Left
            indices[3] = 4 + offset; // Low Left
            indices[4] = 1 + offset; // High Left
            indices[5] = 3 + offset; // Low Top

            // Side Square Right
            indices[6] = 2 + offset; // High Right
            indices[7] = 5 + offset;  // Low Right
            indices[8] = 0 + offset;  // High Top
            indices[9] = 3 + offset;  // Low Top
            indices[10] = 0 + offset;  // High Top
            indices[11] = 5 + offset;  // Low Right

            // Side Square Bottom
            indices[12] = 1 + offset;    // High Left
            indices[13] = 4 + offset;    // Low Left
            indices[14] = 2 + offset;    // High Right
            indices[15] = 5 + offset;    // Low Right
            indices[16] = 2 + offset;    // High Right
            indices[17] = 4 + offset;    // Low Left

            return indices;
        }
        private int[] GetWireIndices(int offset)
        {
            int[] outlines = new int[6];
            // Top Triangle
            outlines[0] = 0 + offset;
            outlines[1] = 1 + offset;

            outlines[2] = 0 + offset;
            outlines[3] = 2 + offset;

            outlines[4] = 1 + offset;
            outlines[5] = 2 + offset;

            return outlines;
        }
        #endregion
    }
}
public enum Layer { Lava, Rock, Dirt, Sand, Water }