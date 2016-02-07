using System;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.ContentPipeline
{
    /// <summary>
    /// Class StringExtensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Creates the vector2 from coordinate string.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 CreateVector2FromCoordinateString(this string coordinates)
        {
            int idx = coordinates.IndexOf(" ");
            if (idx == -1)
                return Vector2.Zero;

            int x = Convert.ToInt32(coordinates.Substring(0, idx));
            int y = Convert.ToInt32(coordinates.Substring(idx + 2, coordinates.Length - idx - 2));

            return new Vector2(x, y);
        }
    }
}
