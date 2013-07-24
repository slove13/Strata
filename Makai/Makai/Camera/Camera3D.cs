using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Camera
{
    public class Camera3D
    {
        #region Drawing
        private BasicEffect effect = new BasicEffect(Common.Game.GraphicsDevice);

        public void DrawTextureDefaultLighting(Texture2D texture)
        {
            DisableColor();
            DefaultLighting();
            PrepTexture(texture);
            Draw();
        }
        public void DrawColor()
        {
            DisableLighting();
            DisableTextures();
            EnableColor();
            Draw();
        }
        public void DrawTexture(Texture2D texture)
        {
            DisableColor();
            DisableLighting();
            PrepTexture(texture);
            Draw();
        }
        public void DrawTexture(Texture2D texture, Vector3 lightDirection, float lightIntensity)
        {
            DisableColor();
            PrepTexture(texture);
            PrepLighting(lightDirection, lightIntensity);
            Draw();
        }

        private void Draw()
        {
            NoCull();
            PrepCamera();
            PrepEffects();
        }
        private void NoCull()
        {
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            Common.Game.GraphicsDevice.RasterizerState = rs;
        }
        private void PrepCamera()
        {
            effect.View = View;
            effect.Projection = Projection;
            effect.World = World;
        }
        private void PrepEffects()
        {
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
            }
        }

        private void PrepTexture(Texture2D texture)
        {
            effect.TextureEnabled = true;
            effect.Texture = texture;
        }
        private void PrepLighting(Vector3 direction, float intensity)
        {
            effect.LightingEnabled = true;
            Vector3 lightDirection = direction * intensity;
            effect.DirectionalLight0.Direction = lightDirection;
            effect.DirectionalLight1.Direction = lightDirection;
            effect.DirectionalLight2.Direction = lightDirection;
        }
        private void DefaultLighting()
        {
            effect.EnableDefaultLighting();
        }

        private void EnableColor()
        {
            effect.VertexColorEnabled = true;
        }
        private void DisableColor()
        {
            effect.VertexColorEnabled = false;
        }
        private void DisableLighting()
        {
            effect.LightingEnabled = false;
        }
        private void DisableTextures()
        {
            effect.TextureEnabled = false;
        }
        #endregion

        private Camera3DKeys input = new Camera3DKeys();

        private float rotX = 0; // Positive X faces Right
        private float rotY = 0; // Positive Y faces Up
        private float rotZ = 0; // Positive Z faces Towards viewer

        private Vector2 rotVel = Vector2.Zero;
        private float rotMax = 0.04f;
        private float rotAccel = 0.005f;
        private float rotDecel = 0.001f;

        private float zoomVel = 0; 
        private float zoomMax = 0.01f;
        private float zoomAccel = 0.0005f;
        private float zoomDecel = 0.00025f;

        private float fieldOfView = MathHelper.PiOver4;
        private float minFOV = 0;
        private float maxFOV = MathHelper.Pi;

        public Matrix World { get { return Matrix.CreateFromYawPitchRoll(rotX, rotY, rotZ); } }
        public Matrix View { get { return Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), Vector3.UnitY); } }
        public Matrix Projection { get { return Matrix.CreatePerspectiveFieldOfView(fieldOfView, Common.Game.GraphicsDevice.Viewport.AspectRatio, 0.01f, 100f); } }

        public void Update()
        {
            Zoom();
            Rotate();
            input.Update();
        }
        private void Zoom()
        {
            zoomVel += zoomAccel * input.Zoom();
            if (input.Zoom() == 0)
            {
                if (zoomVel > 0)
                {
                    if (zoomVel - zoomDecel < 0)
                    {
                        zoomVel = 0;
                    }
                    else
                    {
                        zoomVel -= zoomDecel;
                    }
                }
                if (zoomVel < 0)
                {
                    if (zoomVel + zoomDecel > 0)
                    {
                        zoomVel = 0;
                    }
                    else
                    {
                        zoomVel += zoomDecel;
                    }
                }
            }
            if (zoomVel > zoomMax)
            {
                zoomVel = zoomMax;
            }
            if (zoomVel < -zoomMax)
            {
                zoomVel = -zoomMax;
            }
            if (zoomVel > 0)
            {
                if (fieldOfView + zoomVel < maxFOV)
                {
                    fieldOfView += zoomVel;
                }
            }
            if (zoomVel < 0)
            {
                if (fieldOfView + zoomVel > minFOV)
                {
                    fieldOfView += zoomVel;
                }
            }
        }
        private void Rotate()
        {
            rotVel.X += rotAccel * input.RotateX();
            rotVel.Y += rotAccel * input.RotateY();
            if (input.RotateX() == 0)
            {
                if (rotVel.X > 0)
                {
                    if (rotVel.X - rotDecel < 0)
                    {
                        rotVel.X = 0;
                    }
                    else
                    {
                        rotVel.X -= rotDecel;
                    }
                }
                if (rotVel.X < 0)
                {
                    if (rotVel.X + rotDecel > 0)
                    {
                        rotVel.X = 0;
                    }
                    else
                    {
                        rotVel.X += rotDecel;
                    }
                }
            }
            if (input.RotateY() == 0)
            {
                if (rotVel.Y > 0)
                {
                    if (rotVel.Y - rotDecel < 0)
                    {
                        rotVel.Y = 0;
                    }
                    else
                    {
                        rotVel.Y -= rotDecel;
                    }
                }
                if (rotVel.Y < 0)
                {
                    if (rotVel.Y + rotDecel > 0)
                    {
                        rotVel.Y = 0;
                    }
                    else
                    {
                        rotVel.Y += rotDecel;
                    }
                }
            }

            if (rotVel.X > rotMax)
            {
                rotVel.X = rotMax;
            }
            if (rotVel.X < -rotMax)
            {
                rotVel.X = -rotMax;
            }
            if (rotVel.Y > rotMax)
            {
                rotVel.Y = rotMax;
            }
            if (rotVel.Y < -rotMax)
            {
                rotVel.Y = -rotMax;
            }
            rotX += rotVel.X;
            rotY += rotVel.Y;

        }
    }
}