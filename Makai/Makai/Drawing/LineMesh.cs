using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Makai.Camera;
namespace Makai.Drawing
{
    public class LineMesh
    {        
        private VertexPositionColor[] vertices;
        private int[] indices;
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;

        public int TotalPrimitives { get { return indices.Length / 2; } }

        public LineMesh(Vector3[] vertices, int[] indices, Color color)
        {
            SetVertices(vertices, color);
            SetIndices(indices);
            SetBuffers();
        }

        private void SetVertices(Vector3[] vertices, Color color)
        {
            this.vertices = new VertexPositionColor[vertices.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                this.vertices[i].Position = vertices[i];
                this.vertices[i].Color = color;
            }
        }
        private void SetIndices(int[] indices)
        {
            this.indices = indices;
        }
        private void SetBuffers()
        {
            vertexBuffer = new VertexBuffer(Common.GraphicsDevice, VertexPositionColor.VertexDeclaration, vertices.Length, BufferUsage.WriteOnly);
            vertexBuffer.SetData(vertices);
            indexBuffer = new IndexBuffer(Common.GraphicsDevice, typeof(int), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }
        public void Draw(Camera3D cam)
        {
            cam.DrawColor();
            Common.GraphicsDevice.Indices = indexBuffer;
            Common.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            Common.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.LineList, 0, 0, vertices.Length, 0, indices.Length / 2);
        }
    }
}