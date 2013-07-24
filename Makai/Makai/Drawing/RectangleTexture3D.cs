using Makai.Camera;
using Makai.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Drawing
{
    public class RectangleTexture3D
    {
        private VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];
        private int[] indices = new int[6];
        private Texture2D texture;

        public Vector3 TopLeft { get { return vertices[0].Position; } }
        public Vector3 TopRight { get { return vertices[1].Position; } }
        public Vector3 BottomLeft { get { return vertices[2].Position; } }
        public Vector3 BottomRight { get { return vertices[3].Position; } }

        public RectangleTexture3D(Texture2D texture, Vector3 topLeft, Vector3 topRight, Vector3 bottomLeft, Vector3 bottomRight)
        {
            this.texture = texture;
            vertices[0].Position = topLeft;
            vertices[1].Position = topRight;
            vertices[2].Position = bottomLeft;
            vertices[3].Position = bottomRight;
            SetTextureCoordinates();
            SetNormals();
            SetIndices();
        }

        private void SetTextureCoordinates()
        {
            vertices[0].TextureCoordinate = new Vector2(0, 0);
            vertices[1].TextureCoordinate = new Vector2(1, 0);
            vertices[2].TextureCoordinate = new Vector2(0, 1);
            vertices[3].TextureCoordinate = new Vector2(1, 1);
        }
        private void SetNormals()
        {
            Vector3 normal = MathLib.NormalizedNormal(vertices[2].Position, vertices[3].Position, vertices[0].Position);

            vertices[0].Normal = normal;
            vertices[1].Normal = normal;
            vertices[2].Normal = normal;
            vertices[3].Normal = normal;
        }
        private void SetIndices()
        {
            indices[0] = 2;
            indices[1] = 3;
            indices[2] = 0;
            indices[3] = 1;
            indices[4] = 0;
            indices[5] = 3;
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
            Common.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices, 0, 4, indices, 0, 2);
        }
    }
}