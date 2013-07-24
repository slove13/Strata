using System;
using Makai.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Camera
{
    //Based on XNA Camera 2D by David Amador
    //www.david-amador.com/2009/10/xna-camera-2d-with-zoom-and-rotation/
    public class Camera2D
    {
        private Camera2DKeys input = new Camera2DKeys();

        private Vector2 pan = Vector2.Zero;
        private float zoom = 1;
        private float rotation = 0;

        public Matrix Transform { get { return Matrix.CreateTranslation(new Vector3(-(Display.Width / 2) - pan.X, -(Display.Height / 2) - pan.Y, 0)) *
                    Matrix.CreateScale(new Vector3((zoom * zoom * zoom), (zoom * zoom * zoom), 0)) *
                    Matrix.CreateRotationZ(rotation) *
                    Matrix.CreateTranslation(new Vector3(Display.Width / 2, Display.Height / 2, 0)); } }

        protected float zoomSpeed = 0.025f;
        protected float panSpeed = 15;
        protected float rotateSpeed = 0.025f;

        public void Update()
        {
            input.Update();
            UpdateZoom();
            UpdatePan();
            UpdateRotation();
            CheckReset();
        }
        private void UpdateZoom()
        {
            zoom += zoomSpeed * input.Zoom();
        }
        private void UpdatePan()
        {
            pan += panSpeed * input.Pan() / (float)Math.Pow(zoom, 3);
        }
        private void UpdateRotation()
        {
            rotation += rotateSpeed * input.Rotation();
        }
        private void CheckReset()
        {
            if (input.ResetCamera())
            {
                zoom = 1;
                rotation = 0;
                pan = Vector2.Zero;
            }
        }

        public void Begin()
        {
            Common.Batch.Begin(0, null, SamplerState.PointClamp, null, null, null, Transform);
            UpdateBackground();
        }
        public void BeginHUD()
        {
            Common.Batch.Begin(0, null, SamplerState.PointClamp, null, null, null);
        }
        private void UpdateBackground()
        {
            Common.Game.GraphicsDevice.Clear(Display.Background);
        }

        public void Draw(Texture2D texture, Color color, Vector2 location, float rotation)
        {
            Common.Batch.Draw(texture, location, null, color, rotation, GetOrigin(texture), 1, SpriteEffects.None, 0);
        }
        public void Draw(Texture2D texture, Color color, Vector2 location, float rotation, Vector2 origin, float scale)
        {
            Common.Batch.Draw(texture, location, null, color, rotation, origin, scale, SpriteEffects.None, 0);
        }
        private Vector2 GetOrigin(Texture2D texture)
        {
            return new Vector2(texture.Width / 2, texture.Height / 2);
        }
        
        public void End()
        {
            Common.Batch.End();
            DepthBufferBugFix();
        }
        private void DepthBufferBugFix()
        {
            Common.Game.GraphicsDevice.DepthStencilState = new DepthStencilState() { DepthBufferEnable = true };
        }
    }
}