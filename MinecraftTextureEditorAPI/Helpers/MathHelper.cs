using System;

namespace MinecraftTextureEditorAPI.Helpers
{
    public static class MathHelper
    {
        /// <summary>
        /// Constrains a value within min and max
        /// </summary>
        /// <typeparam name="T">The type of the value</typeparam>
        /// <param name="val">The value</param>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximium value</param>
        /// <returns>T</returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}