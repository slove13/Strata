using System;
using Microsoft.Xna.Framework;
namespace Makai.Helpers
{
    public static class RandomHelper
    {
        private static Random rand = new Random();

        /// <summary>
        /// Generates a random color between (0, 0, 0) and (255, 255, 255)
        /// </summary>
        public static Color RandomColor { get { return new Color(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255)); } }

        /// <summary>
        /// Randomly sorts array using Fisher-Yates shuffle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void Shuffle<T>(T[] array)
        {
            int length = array.Length;
            while (length > 1)
            {
                length--;
                int element = rand.Next(length + 1);
                T value = array[element];
                array[element] = array[length];
                array[length] = value;
            }
        }
    }
}