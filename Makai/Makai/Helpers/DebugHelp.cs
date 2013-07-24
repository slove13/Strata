namespace Makai.Helpers
{
    public static class DebugHelp
    {
        public static void PrintToFile(string[] data, string path)
        {
            System.IO.File.WriteAllLines(@path, data);
        }
    }
}