using System;
using Makai.Helpers;
using Microsoft.Xna.Framework;
namespace Strata.Planets
{
    class GeodesicSphere
    {
        private int element;
        private Vector3[] vertices;
        private int[] indices;

        public int Frequency { get; private set; }

        public GeodesicSphere(int frequency)
        {
            Frequency = frequency;
            Geodesize();
        }

        private void Geodesize()
        {
            SetVertices();
            SetIndices();
        }

        #region Vertices
        private Vector3[] BaseVertices
        {
            get
            {
                float φ = 1.61803398875f;
                float small = φ - 1;
                Vector3[] verts = new Vector3[12];
                verts[0] = new Vector3(-small, 1, 0);
                verts[1] = new Vector3(0, small, 1);
                verts[2] = new Vector3(small, 1, 0);
                verts[3] = new Vector3(0, small, -1);
                verts[4] = new Vector3(-1, 0, -small);
                verts[5] = new Vector3(-1, 0, small);
                verts[6] = new Vector3(0, -small, 1);
                verts[7] = new Vector3(1, 0, small);
                verts[8] = new Vector3(1, 0, -small);
                verts[9] = new Vector3(0, -small, -1);
                verts[10] = new Vector3(-small, -1, 0);
                verts[11] = new Vector3(small, -1, 0);
                return verts;
            }
        }
        private Vector3 BaseTop { get { return BaseVertices[0]; } }     // Row 1 of Base Vertices
        private Vector3[] BaseMiddleTop
        {
            get
            {
                Vector3[] row = new Vector3[6];
                row[0] = BaseVertices[1];
                row[1] = BaseVertices[2];
                row[2] = BaseVertices[3];
                row[3] = BaseVertices[4];
                row[4] = BaseVertices[5];
                row[5] = BaseVertices[1];
                return row;
            }
        }                              // Row 2 of Base Vertices
        private Vector3[] BaseMiddleBottom
        {
            get
            {
                Vector3[] row = new Vector3[6];
                row[0] = BaseVertices[6];
                row[1] = BaseVertices[7];
                row[2] = BaseVertices[8];
                row[3] = BaseVertices[9];
                row[4] = BaseVertices[10];
                row[5] = BaseVertices[6];
                return row;
            }
        }                           // Row 3 of Base Vertices
        private Vector3 BaseBottom { get { return BaseVertices[11]; } } // Row 4 of Base Vertices

        private int TotalVertices
        {
            get
            {
                int cornerVertices = TotalBaseVertices;
                int edgeVertices = TotalBaseEdges * (Frequency - 1);
                int faceVertices = TotalBaseFaces * MathLib.SumOfIntegers(Frequency - 2);
                return cornerVertices + edgeVertices + faceVertices;
            }
        }
        private int TotalBaseVertices { get { return 12; } }
        private int TotalBaseEdges { get { return 30; } }
        private int TotalBaseFaces { get { return 20; } }

        private void SetVertices()
        {
            // Vertices along Right Edge of Base Triangle not added
            // Because duplicates will be along Left Edge of next Base Triangle
            vertices = new Vector3[TotalVertices];
            element = 0;
            SetVerticesInTopTriangles();
            SetVerticesInMiddleTriangles();
            SetVerticesInBottomTriangles();
        }
        private void SetVerticesInTopTriangles()    // Vertices in Top Base Triangles but not bottom row
        {
            vertices[element] = Vector3.Normalize(BaseVertices[0]);  // vertices[0] = BaseVertices[0]. Top Vertex shared by Top Base Triangles
            element++;

            for (int row = 1; row < Frequency; row++)
            {
                for (int baseTriangle = 0; baseTriangle < 5; baseTriangle++)
                {
                    Vector3 vectorLeft = MiddleVertex(row, Frequency, BaseTop, BaseMiddleTop[baseTriangle]);
                    Vector3 vectorRight = MiddleVertex(row, Frequency, BaseTop, BaseMiddleTop[baseTriangle + 1]);
                    for (int column = 0; column < row; column++)
                    {
                        Vector3 vectorMiddle = MiddleVertex(column, row, vectorLeft, vectorRight);
                        vertices[element] = Vector3.Normalize(vectorMiddle);
                        element++;
                    }
                }
            }
        }
        private void SetVerticesInMiddleTriangles()
        {
            int rowDown = Frequency;    // Row from perspective of Down Pointing Triangles
            for (int rowUp = 0; rowUp <= Frequency; rowUp++)  // Row from perspective of Up Pointing Triangles
            {
                for (int baseTriangle = 0; baseTriangle < 5; baseTriangle++)
                {
                    Vector3 left;
                    Vector3 right;
                    // Set Up Pointing Base Triangle
                    left = MiddleVertex(rowUp, Frequency, BaseMiddleTop[baseTriangle], BaseMiddleBottom[baseTriangle]);
                    right = MiddleVertex(rowUp, Frequency, BaseMiddleTop[baseTriangle], BaseMiddleBottom[baseTriangle + 1]);
                    for (int column = 0; column < rowUp; column++)
                    {
                        Vector3 vectorMiddle = MiddleVertex(column, rowUp, left, right);
                        vertices[element] = Vector3.Normalize(vectorMiddle);
                        element++;
                    }

                    // Set Down Pointing Base Triangle
                    left = MiddleVertex(rowDown, Frequency, BaseMiddleBottom[baseTriangle + 1], BaseMiddleTop[baseTriangle]);
                    right = MiddleVertex(rowDown, Frequency, BaseMiddleBottom[baseTriangle + 1], BaseMiddleTop[baseTriangle + 1]);
                    for (int column = 0; column < rowDown; column++)
                    {
                        Vector3 vectorMiddle = MiddleVertex(column, rowDown, left, right);
                        vertices[element] = Vector3.Normalize(vectorMiddle);
                        element++;
                    }
                }
                rowDown--;
            }
        }
        private void SetVerticesInBottomTriangles() // Vertices in Bottom Base Triangles but not top row
        {
            for (int row = Frequency - 1; row > 0; row--)
            {
                for (int baseTriangle = 0; baseTriangle < 5; baseTriangle++)
                {
                    Vector3 vectorLeft = MiddleVertex(row, Frequency, BaseBottom, BaseMiddleBottom[baseTriangle]);
                    Vector3 vectorRight = MiddleVertex(row, Frequency, BaseBottom, BaseMiddleBottom[baseTriangle + 1]);
                    // Set Middle Vertices
                    for (int column = 0; column < row; column++)
                    {
                        Vector3 vectorMiddle = MiddleVertex(column, row, vectorLeft, vectorRight);
                        vertices[element] = Vector3.Normalize(vectorMiddle);
                        element++;
                    }
                }
            }
            vertices[element] = Vector3.Normalize(BaseVertices[11]);    // vertices[TotalVertices - 1] = BaseVertices[BaseVertices.Length - 1]. Bottom Vertex shared by Bottom Base Triangles
        }
        private Vector3 MiddleVertex(int x, int divisions, Vector3 topVertex, Vector3 bottomVertex)
        {
            float xDiff = (bottomVertex.X - topVertex.X) / divisions;
            float yDiff = (bottomVertex.Y - topVertex.Y) / divisions;
            float zDiff = (bottomVertex.Z - topVertex.Z) / divisions;
            Vector3 vectorDiff = new Vector3(xDiff, yDiff, zDiff);
            Vector3 vector = topVertex;
            for (int i = 0; i < x; i++)
            {
                vector += vectorDiff;
            }
            return vector;
        }
        #endregion

        #region Indices
        private int TotalIndices { get { return TotalBaseFaces * FacesPerBaseFace * 3; } }
        private int FacesPerBaseFace { get { return (int)Math.Pow(Frequency, 2); } }

        private void SetIndices()
        {
            element = 0;
            indices = new int[TotalIndices];

            SetIndicesInTopTriangles();
            SetIndicesInMiddleTriangles();
            SetIndicesInBottomTriangles();
        }
        private void SetIndicesInTopTriangles()
        {
            for (int row = 0; row < Frequency; row++)
            {
                for (int baseTriangle = 0; baseTriangle < 5; baseTriangle++)
                {
                    for (int column = 0; column <= row; column++)
                    {
                        if (baseTriangle == 4 && column >= row - 1) // If Triangle is on Right Side of Net and needs  wraparound vertices
                        {
                            if (column == row - 1)
                            {
                                // Up Pointing
                                indices[element] = FirstElementInRowInTopTriangle(row) + baseTriangle * row + column;
                                indices[element + 1] = FirstElementInRowInTopTriangle(row + 1) + baseTriangle * (row + 1) + column;
                                indices[element + 2] = FirstElementInRowInTopTriangle(row + 1) + baseTriangle * (row + 1) + column + 1;

                                // Down Pointing
                                indices[element + 3] = FirstElementInRowInTopTriangle(row + 1) + baseTriangle * (row + 1) + column + 1;
                                indices[element + 4] = FirstElementInRowInTopTriangle(row);
                                indices[element + 5] = FirstElementInRowInTopTriangle(row) + baseTriangle * row + column;
                                element += 6;
                            }
                            if (column == row)  // Right Edge
                            {
                                indices[element] = FirstElementInRowInTopTriangle(row);
                                indices[element + 1] = FirstElementInRowInTopTriangle(row + 1) + baseTriangle * (row + 1) + column;
                                indices[element + 2] = FirstElementInRowInTopTriangle(row + 1);
                                element += 3;
                            }
                        }
                        else
                        {
                            // Up Pointing
                            indices[element] = FirstElementInRowInTopTriangle(row) + baseTriangle * row + column;
                            indices[element + 1] = FirstElementInRowInTopTriangle(row + 1) + baseTriangle * (row + 1) + column;
                            indices[element + 2] = FirstElementInRowInTopTriangle(row + 1) + baseTriangle * (row + 1) + column + 1;
                            element += 3;

                            // Down Pointing
                            if (column < row)
                            {
                                indices[element] = FirstElementInRowInTopTriangle(row + 1) + baseTriangle * (row + 1) + column + 1;
                                indices[element + 1] = FirstElementInRowInTopTriangle(row) + baseTriangle * row + column + 1;
                                indices[element + 2] = FirstElementInRowInTopTriangle(row) + baseTriangle * row + column;
                                element += 3;
                            }
                        }
                    }
                }
            }
        }       // Could simplify code
        private int FirstElementInRowInTopTriangle(int row)
        {
            if (row == 0)
                return 0;
            if (row == 1)
                return 1;
            return 5 * (row - 1) + FirstElementInRowInTopTriangle(row - 1);
        }
        private void SetIndicesInMiddleTriangles()  // Sets Top Row but not Bottom Row
        {
            int start = FirstElementInRowInTopTriangle(Frequency);
            int end = FirstElementInRowInTopTriangle(Frequency) + 5 * (MathLib.SumOfIntegers(Frequency) + MathLib.SumOfIntegers(Frequency - 1));
            for (int i = start; i < end; i++)
            {
                indices[element] = i;
                indices[element + 1] = i + 5 * Frequency;
                int offset = i - start + 1;
                if (offset != 0 && offset % (5 * Frequency) == 0)    // Tile is on Right Edge and needs wraparound vertices
                {
                    indices[element + 2] = i + 1;
                    indices[element + 3] = i + 1;
                    indices[element + 4] = i - 5 * Frequency + 1;
                }
                else
                {
                    indices[element + 2] = i + 5 * Frequency + 1;
                    indices[element + 3] = i + 5 * Frequency + 1;
                    indices[element + 4] = i + 1;
                }
                indices[element + 5] = i;
                element += 6;

            }
        }
        private int FirstElementInRowInBottomTriangle(int row)
        {
            return TotalVertices - 1 - OffsetInBottomTriangle(row);
        }
        private int OffsetInBottomTriangle(int row)
        {
            if (row == 0)
                return 0;
            return 5 * row + OffsetInBottomTriangle(row - 1);
        }
        private void SetIndicesInBottomTriangles()
        {
            for (int row = Frequency; row > 0; row--)
            {
                for (int baseTriangle = 0; baseTriangle < 5; baseTriangle++)
                {
                    for (int column = 0; column < row; column++)
                    {
                        if (baseTriangle == 4 && column >= row - 2) // If Triangle is on Right Side of Net and needs  wraparound vertices
                        {
                            if (column == row - 2)
                            {
                                // Down Pointing
                                indices[element] = FirstElementInRowInBottomTriangle(row - 1) + baseTriangle * (row - 1) + column;
                                indices[element + 1] = FirstElementInRowInBottomTriangle(row) + baseTriangle * row + column + 1;
                                indices[element + 2] = FirstElementInRowInBottomTriangle(row) + baseTriangle * row + column;
                                element += 3;

                                // Up Pointing
                                indices[element] = FirstElementInRowInBottomTriangle(row) + baseTriangle * row + column + 1;
                                indices[element + 1] = FirstElementInRowInBottomTriangle(row - 1) + baseTriangle * (row - 1) + column;
                                indices[element + 2] = FirstElementInRowInBottomTriangle(row - 1);
                                element += 3;
                            }
                            if (column == row - 1)
                            {
                                // Down Pointing
                                indices[element] = FirstElementInRowInBottomTriangle(row - 1);
                                indices[element + 1] = FirstElementInRowInBottomTriangle(row);
                                indices[element + 2] = FirstElementInRowInBottomTriangle(row) + baseTriangle * row + column;
                                element += 3;
                            }

                        }
                        else
                        {
                            // Down Pointing
                            indices[element] = FirstElementInRowInBottomTriangle(row - 1) + baseTriangle * (row - 1) + column;
                            indices[element + 1] = FirstElementInRowInBottomTriangle(row) + baseTriangle * row + column + 1;
                            indices[element + 2] = FirstElementInRowInBottomTriangle(row) + baseTriangle * row + column;
                            element += 3;

                            // Up Pointing
                            if (column < row - 1)
                            {
                                indices[element] = FirstElementInRowInBottomTriangle(row) + baseTriangle * row + column + 1;
                                indices[element + 1] = FirstElementInRowInBottomTriangle(row - 1) + baseTriangle * (row - 1) + column;
                                indices[element + 2] = FirstElementInRowInBottomTriangle(row - 1) + baseTriangle * (row - 1) + column + 1;
                                element += 3;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Layers
        public Vector3 GetVertexNormal(Vector3 position)
        {
            Vector3 normal = position;
            normal.Normalize();
            return normal;
        }   // Normalized position
        public Vector3 GetTopVertex(int tile)
        {
            return vertices[indices[tile * 3]];
        }
        public Vector3 GetLeftVertex(int tile)
        {
            return vertices[indices[tile * 3 + 1]];
        }
        public Vector3 GetRightVertex(int tile)
        {
            return vertices[indices[tile * 3 + 2]];
        }
        #endregion

        #region Tiles
        public int TotalTiles
        {
            get
            {
                // old way: indices.Length / 3; 
                int totalTiles = 0;
                for (int row = 0; row < TotalRows; row++)
                {
                    totalTiles += ColumnsInRow(row);
                }
                return totalTiles;
            }
        }
        public int TotalRows { get { return Frequency * 3; } }
        public int ColumnsInRow(int row)
        {
            if (row < MiddleTopTileRow)
                return (2 * row + 1) * 5;
            if (row > MiddleBottomTileRow)
                return (2 * (BottomTileRow - row) + 1) * 5;
            return Frequency * 10;   // Middle rows all equal length
        }
        private int FirstTileInRow(int row)
        {
            if (row == 0)
                return 0;
            return ColumnsInRow(row - 1) + FirstTileInRow(row - 1);
        }
        private int LastTileInRow(int row)
        {
            return FirstTileInRow(row + 1) - 1;
        }

        private int TopTileRow { get { return 0; } }
        private int MiddleTopTileRow { get { return Frequency; } }      // Top Tile Row in Middle Base Row
        private int MiddleBottomTileRow { get { return Frequency * 2 - 1; } }   // Bottom Tile Row in Middle Base Row
        private int BottomTileRow { get { return Frequency * 3 - 1; } }

        public int GetTileIndex(int row, int column)
        {
            return FirstTileInRow(row) + column;
        }
        public int RowOfTile(int tile)
        {
            int row;
            for (row = 0; row < TotalRows; row++)
            {
                if (tile < FirstTileInRow(row + 1))
                {
                    break;
                }
            }
            return row;
        }
        public int ColumnOfTile(int tile)
        {
            return tile - FirstTileInRow(RowOfTile(tile));
        }

        public int GetLeftTile(int tile)
        {
            int row = RowOfTile(tile);
            if (tile == FirstTileInRow(row))
            {
                return LastTileInRow(row);
            }
            return tile - 1;
        }
        public int GetRightTile(int tile)
        {
            int row = RowOfTile(tile);
            if (tile == LastTileInRow(row))
            {
                return FirstTileInRow(row);
            }
            return tile + 1;
        }
        public int GetCenterTile(int tile)
        {
            #region OLD CODE
            //int row;
            //int column;
            //if (TilePointsUp(tile))
            //{
            //    row = RowOfTile(tile) + 1;
            //    if (ColumnOfTile(tile) == LastTileInRow(RowOfTile(tile)))
            //    {
            //        int baseTriangle;
            //        for (baseTriangle = 0; baseTriangle < 5; baseTriangle++)
            //        {
            //            if (tile < (baseTriangle + 1) * TilesInRow(row) / 5)
            //            {
            //                break;
            //            }
            //        }
            //        int baseColumn = tile - baseTriangle * TilesInRow(row) / 5;
            //        column = baseTriangle * TilesInRow(row + 1) / 5 + baseColumn + 1;
            //    }
            //    else
            //    {
            //        column = ColumnOfTile(tile) + 1;
            //    }
            //}
            //else // Tile Points Down
            //{
            //    row = RowOfTile(tile) - 1;
            //    if (ColumnOfTile(tile) == LastTileInRow(RowOfTile(tile)))
            //    {
            //        int baseTriangle;
            //        for (baseTriangle = 0; baseTriangle < 5; baseTriangle++)
            //        {
            //            if (tile < (baseTriangle + 1) * TilesInRow(row) / 5)
            //            {
            //                break;
            //            }
            //        }
            //        int baseColumn = tile - baseTriangle * TilesInRow(row) / 5;
            //        column = baseTriangle * TilesInRow(row - 1) / 5 + baseColumn - 1;
            //    }
            //    else
            //    {
            //        column = ColumnOfTile(tile) - 1;
            //    }
            //}
            //return GetTileIndex(row, column);
            #endregion
            throw new NotImplementedException();
        }
        public bool TilePointsUp(int tile)
        {
            #region OLD CODE
            //int row = RowOfTile(tile);
            //// Top Base Rows
            //if (row < MiddleTopTileRow)
            //{
            //    int baseTriangle;
            //    for (baseTriangle = 0; baseTriangle < 5; baseTriangle++)
            //    {
            //        int tilesInBaseTriangle = TilesInRow(row) / 5;
            //        if (tile < (baseTriangle + 1) * tilesInBaseTriangle)
            //        {
            //            break;
            //        }
            //    }
            //    return MathLib.IsEven(baseTriangle);
            //}

            //// Bottom Base Rows

            //if (row > MiddleTopTileRow)
            //{
            //    int baseTriangle;
            //    for (baseTriangle = 0; baseTriangle < 5; baseTriangle++)
            //    {
            //        int tilesInBaseTriangle = TilesInRow(row) / 5;
            //        if (tile < (baseTriangle + 1) * tilesInBaseTriangle)
            //        {
            //            break;
            //        }
            //    }
            //    return MathLib.IsOdd(baseTriangle);
            //}

            //// Middle Rows

            //else // (row >= MiddleTopTileRow && row <= MiddleBottomTileRow)
            //{
            //    if (MathLib.IsEven(ColumnOfTile(tile)))
            //    {
            //        return true;
            //    }
            //    return false;
            //}
            #endregion
            throw new NotImplementedException();
        }

        #endregion
    }
}