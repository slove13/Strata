using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Makai.Input
{
    public class InputKeys : IInputDevice<Keys>
    {
        private KeyboardState NowKey { get { return Keyboard.GetState(); } }
        private KeyboardState oldKey;

        private MouseState NowMouse { get { return Mouse.GetState(); } }
        private MouseState oldMouse;

        public Vector2 MousePosition { get { return new Vector2(NowMouse.X, NowMouse.Y); } }
        public float MouseWheel { get { return Mouse.GetState().ScrollWheelValue; } }  

        public virtual void Update()
        {
            oldKey = NowKey;
            oldMouse = NowMouse;
        }       //Updates last input   
        public bool Press(Keys key)
        {
            return NowKey.IsKeyDown(key) && oldKey.IsKeyUp(key);
        }
        public bool Hold(Keys key)
        {
            return NowKey.IsKeyDown(key);
        }

        //Yoda style (fix)
        public bool LeftClick()
        {
            return (ButtonState.Pressed == NowMouse.LeftButton && ButtonState.Released == oldMouse.LeftButton);
        }
        public bool LeftClickHold()
        {
            return (ButtonState.Pressed == NowMouse.LeftButton);
        }
        public bool LeftClickRelease()
        {
            return (ButtonState.Released == NowMouse.LeftButton && ButtonState.Pressed == oldMouse.LeftButton);
        }
    }
}