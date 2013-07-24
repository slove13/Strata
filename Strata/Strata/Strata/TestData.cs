using Strata.Tests;
using Makai.Camera;
using Makai.Graphics;
using Microsoft.Xna.Framework;
namespace Strata
{
    static class TestData
    {
        private static Camera2D cam2D;
        private static Camera3D cam3D;
        private static PlanetTest test;

        public static void Initialize()
        {
            Display.SetResolution(DisplayResolution.Full_1280x960);
            Display.MouseVisible = true;
            Display.UnlockFrameRate = false;
            Display.MultiSampling = true;
            Display.Background = Color.Tomato;

            cam2D = new Camera2D();
            cam3D = new Camera3D();

            test = new PlanetTest();
        }
        public static void Draw()
        {
            // Draw Background
            cam2D.Begin();
            cam2D.End();

            // Draw Scene
            test.DrawScene(cam3D);
            
            // Draw HUD elements
            cam2D.BeginHUD();
            test.DrawHUD();
            cam2D.End();
        }
        public static void Update()
        {
            Display.Update();

            cam2D.Update();
            cam3D.Update();

            test.Update();
            test.Print();
        }
    }
}