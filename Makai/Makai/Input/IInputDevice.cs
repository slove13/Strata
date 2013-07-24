namespace Makai.Input
{
    interface IInputDevice <T>
    {
        void Update();
        bool Press(T t);
        bool Hold(T t);
    }
}