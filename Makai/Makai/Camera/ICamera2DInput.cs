using Microsoft.Xna.Framework;
namespace Makai.Camera
{
    interface ICamera2DInput:ICameraInput
    {
        Vector2 Pan();
        float Rotation();
    }
}