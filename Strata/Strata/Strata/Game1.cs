using Makai;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Strata
{
    public class Game1 : Game
    {
        static void Main(string[] args)
        {
            Game1 game = new Game1();
            Common common = new Common(game);
        }
        protected override void Initialize()
        {
            Common.Batch = new SpriteBatch(Common.Graphics.GraphicsDevice);
            TestData.Initialize();
            base.Initialize();
        }
        protected override void Draw(GameTime time)
        {
            TestData.Draw();
            base.Draw(time);
        }
        protected override void Update(GameTime time)
        {
            TestData.Update();
            base.Update(time);
        }
    }
}