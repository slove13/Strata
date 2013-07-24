using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Makai.Input;
namespace Makai.Graphics
{
    class DisplayKeys : InputKeys, IDisplayInput
    {
        private Keys exit = Keys.Escape;

        public bool Exit()
        {
            return Press(exit);
        }                  //Exit program
    }
}