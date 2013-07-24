using Makai.Input;
using Microsoft.Xna.Framework.Input;
namespace Makai.Camera
{
    class Camera3DKeys:InputKeys,ICamera3DInput
    {
        //private Keys up = Keys.NumPad7;
        //private Keys down = Keys.NumPad9;
        //private Keys left = Keys.NumPad4;
        //private Keys right = Keys.NumPad6;
        //private Keys zoomIn = Keys.NumPad8;
        //private Keys zoomOut = Keys.NumPad5;
        private Keys up = Keys.E;
        private Keys down = Keys.Q;
        private Keys left = Keys.A;
        private Keys right = Keys.D;
        private Keys zoomIn = Keys.W;
        private Keys zoomOut = Keys.S;

        public float Zoom()
        {
            //return MouseWheel;
            if (Hold(zoomIn) && !Hold(zoomOut))
            {
                return -1;
            }
            if (Hold(zoomOut) && !Hold(zoomIn))
            {
                return 1;
            }
            return 0;
        }
        public float RotateX()
        {
            if (Hold(right) && !Hold(left))
            {
                return 1;
            }
            if (Hold(left) && !Hold(right))
            {
                return -1;
            }
            return 0;
        }
        public float RotateY()
        {
            if (Hold(up) && !Hold(down))
            {
                return 1;
            }
            if (Hold(down) && !Hold(up))
            {
                return -1;
            }
            return 0;
        }
    }
}