using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Makai.Input
{
    public class InputPad : IInputDevice<Buttons>
    {
        public PlayerIndex PlayerNumber { get; set; }

        private GamePadState NowPad { get { return GamePad.GetState(PlayerNumber); } }
        private GamePadState oldPad;

        public Vector2 LeftStick { get { return GamePad.GetState(PlayerNumber).ThumbSticks.Right; } }
        public Vector2 RightStick { get { return GamePad.GetState(PlayerNumber).ThumbSticks.Left; } }
        public float LeftTrigger { get { return GamePad.GetState(PlayerNumber).Triggers.Left; } }
        public float RightTrigger { get { return GamePad.GetState(PlayerNumber).Triggers.Right; } }


        public InputPad(PlayerIndex playerNumber)
        {
            PlayerNumber = playerNumber;
        }

        public void Update()
        {
            oldPad = NowPad;
        }       //Updates last input
        public bool Press(Buttons button)
        {
            return NowPad.IsButtonDown(button) & oldPad.IsButtonUp(button);
        }
        public bool Hold(Buttons button)
        {
            return NowPad.IsButtonDown(button);
        }
    }
}