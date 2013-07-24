using Makai.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace Makai.Camera
{
    class Camera2DKeys : InputKeys, ICamera2DInput
    {
        private Keys panUp = Keys.NumPad8;
        private Keys panDown = Keys.NumPad5;
        private Keys panLeft = Keys.NumPad4;
        private Keys panRight = Keys.NumPad6;
        private Keys zoomIn = Keys.Add;
        private Keys zoomOut = Keys.NumPad0;
        private Keys spinLeft = Keys.NumPad7;
        private Keys spinRight = Keys.NumPad9;
        private Keys follow = Keys.OemBackslash;
        private Keys reset = Keys.Multiply;

        public float Zoom()
        {
            if (Hold(zoomIn) && !Hold(zoomOut))
            {
                return 1;
            }
            if (Hold(zoomOut) && !Hold(zoomIn))
            {
                return -1;
            }
            return 0;
        }
        public Vector2 Pan()
        {
            float x = 0;
            if (Hold(panRight) && !Hold(panLeft))
            {
                x = 1;
            }
            if (Hold(panLeft) && !Hold(panRight))
            {
                x = -1;
            }
            float y = 0;
            if (Hold(panUp) && !Hold(panDown))
            {
                y = 1;
            }
            if (Hold(panDown) && !Hold(panUp))
            {
                y = -1;
            }
            if (x != 0 && y != 0)
            {
                x = 0.5f;
                y = 0.5f;
            }
            return new Vector2(x, y);
        }
        public float Rotation()
        {
            if (Hold(spinRight)&&!Hold(spinLeft))
            {
                return 1;
            }
            if (Hold(spinLeft) && !Hold(spinRight))
            {
                return -1;
            }
            return 0;
        }

        public bool ToggleFollow()
        {
            return Press(follow);
        }              //Press to toggle Follow Camera
        public bool ResetCamera()
        {
            return Press(reset);
        }              //Press to reset Camera settings
    }
}