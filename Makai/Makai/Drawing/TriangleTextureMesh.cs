using System;
using Makai.Camera;
using Makai.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Drawing
{
    public class TriangleTextureMesh
    {
        private VertexPositionNormalTexture[] vertices;
        private int[] indices;
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;

        private Texture2D texture;

        public int TotalPrimitives { get { return indices.Length / 3; } }

        public TriangleTextureMesh(Vector3[] vertices, int[] indices, Texture2D texture)
        {
            SetIndices(indices);
            SetVertices(vertices);
            SetBuffers();
            this.texture = texture;
        }

        private void SetVertices(Vector3[] vertices)
        {
            this.vertices = new VertexPositionNormalTexture[vertices.Length];
            SetPositions(vertices);
            SetNormals(vertices);
            SetTextureCoordinates(vertices);
        }
        private void SetPositions(Vector3[] vertices)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                this.vertices[i].Position = vertices[i];
            }
        }
        private void SetNormals(Vector3[] vertices)
        {
            for (int i = 0; i < vertices.Length; i += 3)
            {
                Vector3 normal = MathLib.NormalizedNormal(vertices[indices[i]], vertices[indices[i + 1]], vertices[indices[i + 2]]);
                this.vertices[i].Normal = normal;
                this.vertices[i + 1].Normal = normal;
                this.vertices[i + 2].Normal = normal;
            }

        }

        private void SetTextureCoordinates(Vector3[] vertices)
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

            for (int i = 0; i < vertices.Length; i += 3)
            {
                RandomHelper.Shuffle(corners);
                this.vertices[i].TextureCoordinate = corners[0];
                this.vertices[i+1].TextureCoordinate = corners[1];
                this.vertices[i+2].TextureCoordinate = corners[2];
            }
        }

        private void SetIndices(int[] indices)
        {
            this.indices = indices;
        }

        private void SetBuffers()
        {
            vertexBuffer = new VertexBuffer(Common.GraphicsDevice, VertexPositionNormalTexture.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);
            indexBuffer = new IndexBuffer(Common.GraphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
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
            Common.GraphicsDevice.Indices = indexBuffer;
            Common.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            Common.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Length, 0, indices.Length / 3);
        }
    }
}