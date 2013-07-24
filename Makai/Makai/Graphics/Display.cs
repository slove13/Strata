using Microsoft.Xna.Framework;
namespace Makai.Graphics
{
    public static class Display
    {
        private static DisplayKeys input = new DisplayKeys();

        public static Color Background { get; set; }

        public static int Width { get { return Common.Graphics.PreferredBackBufferWidth; } set { Common.Graphics.PreferredBackBufferWidth = value; ApplyChanges(); } }
        public static int Height { get { return Common.Graphics.PreferredBackBufferHeight; } set { Common.Graphics.PreferredBackBufferHeight = value; ApplyChanges(); } }
        public static Vector2 Center { get { return new Vector2(Width / 2, Height / 2); } }
        public static string WindowTitle { get { return Common.Game.Window.Title; } set { Common.Game.Window.Title = value; ApplyChanges(); } }

        public static bool MouseVisible { set { Common.Game.IsMouseVisible = value; ApplyChanges(); } }
        public static bool UnlockFrameRate { set { Common.Graphics.SynchronizeWithVerticalRetrace = !value; Common.Game.IsFixedTimeStep = !value; } }
        public static bool MultiSampling { set { Common.Graphics.PreferMultiSampling = value; ApplyChanges(); } }

        public static void Update()
        {
            CheckExit(); 
            input.Update();
        }                //Updates System actions
        public static void CheckExit()
        {
            if (input.Exit())
            {
                Common.Game.Exit();
            }
        }                  //Checks if program should exit
        private static void ApplyChanges()
        {
            Common.Graphics.ApplyChanges();
        }

        public static void SetResolution(DisplayResolution resolution)
        {
            if (resolution == DisplayResolution.Full_640x480)
            {
                Width = 640;
                Height = 480;
            }
            if (resolution == DisplayResolution.Full_1024x768)
            {
                Width = 1024;
                Height = 768;
            }
            if (resolution == DisplayResolution.Full_1152x864)
            {
                Width = 1152;
                Height = 864;
            }
            if (resolution == DisplayResolution.Full_1280x960)
            {
                Width = 1280;
                Height = 960;
            }

            if (resolution == DisplayResolution.Wide_1280x720)
            {
                Width = 1280;
                Height = 720;
            }
            if (resolution == DisplayResolution.Wide_1600x900)
            {
                Width = 1600;
                Height = 900;
            }
            if (resolution == DisplayResolution.Wide_1920x1080)
            {
                Width = 1920;
                Height = 1080;
            }
        }
    }
}
public enum DisplayResolution { Full_640x480, Full_1024x768, Full_1152x864, Full_1280x960, Wide_1280x720, Wide_1600x900, Wide_1920x1080 }