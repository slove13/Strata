using Makai.Camera;
using Makai.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace Makai.Drawing
{
    public class TriangleTexture3D
    {
        private VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[3];
        private int[] indices = new int[3];
        private Texture2D texture;

        public Vector3 TopVertex { get { return vertices[0].Position; } }
        public Vector3 LeftVertex { get { return vertices[1].Position; } }
        public Vector3 RightVertex { get { return vertices[2].Position; } }

        public TriangleTexture3D(Texture2D texture, Vector3 top, Vector3 left, Vector3 right)
        {
            this.texture = texture;
            vertices[0].Position = top;
            vertices[1].Position = left;
            vertices[2].Position = right;
            SetTextureCoordinates();
            SetNormals();
            SetIndices();
        }

        private void SetTextureCoordinates()
        {
            Random random = new Random();
            Vector2 topLeft = new Vector2(0, 0);
            Vector2 topRight = new Vector2(1, 0);
            Vector2 bottomLeft = new Vector2(0, 1);
            Vector2 bottomRight = new Vector2(1, 1);

            Vector2[] corners = new Vector2[4];
            corners[0] = topLeft;
            corners[1] = topRight;
            corners[2] = bottomLeft;
            corners[3] = bottomRight;

            RandomHelper.Shuffle(corners);
            this.vertices[0].TextureCoordinate = corners[0];
            this.vertices[1].TextureCoordinate = corners[1];
            this.vertices[2].TextureCoordinate = corners[2];
        }
        private void SetNormals()
        {
            Vector3 normal = MathLib.NormalizedNormal(vertices[0].Position, vertices[1].Position, vertices[2].Position);
            vertices[0].Normal = normal;
            vertices[1].Normal = normal;
            vertices[2].Normal = normal;
        }
        private void SetIndices()
        {
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
        }

        public void Draw(Camera3D cam)
        {
            cam.DrawTexture(texture);
            DrawPrimitives();
        }
        public void Draw(Camera3D cam, Vector3 lightDirection, float lightIntensity)
        {
            cam.DrawTexture(texture, lightDirection, lightIntensity);
            DrawPrimitives();
        }
        private void DrawPrimitives()
        {
            Common.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices, 0, 3, indices, 0, 1);
        }
    }
}