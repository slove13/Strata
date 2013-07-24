using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
namespace Makai.Content
{
    public static class Load
    {
        public static Texture2D Texture(string path)
        {
            return Common.Game.Content.Load<Texture2D>(path);
        }
        public static SoundEffect Sound(string path)
        {
            return Common.Game.Content.Load<SoundEffect>(path);
        }
        public static Effect Effect(string path)
        {
            return Common.Game.Content.Load<Effect>(path);
        }
    }
}