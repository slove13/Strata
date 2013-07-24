using Makai.Content;
using Makai.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Makai
{
    public class Common
    {
        public static Game Game { get; set; }
        public static GraphicsDeviceManager Graphics { get; set; }
        public static GraphicsDevice GraphicsDevice { get { return Graphics.GraphicsDevice; } }
        public static SpriteBatch Batch { get; set; }
        public static Texture2D Pixel { get { Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1); pixel.SetData(new[] { Color.White }); return pixel; } }

        public Common(Game game)
        {
            Game = game; 
            Game.Content.RootDirectory = "Content";
            Graphics = new GraphicsDeviceManager(game);
            Game.Run();
        }
    }
}