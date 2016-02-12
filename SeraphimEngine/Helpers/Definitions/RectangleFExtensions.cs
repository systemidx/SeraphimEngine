using System;
using Microsoft.Xna.Framework;
using SeraphimEngine.Definitions;

namespace SeraphimEngine.Helpers.Definitions
{
    /// <summary>
    /// Class RectangleFExtensions.
    /// </summary>
    public static class RectangleFExtensions
    {
        /// <summary>
        /// To the rectangle.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle ToRectangle(this RectangleF rectangle)
        {
            return new Rectangle(Convert.ToInt32(rectangle.X), Convert.ToInt32(rectangle.Y), Convert.ToInt32(rectangle.Width), Convert.ToInt32(rectangle.Height));
        }
    }
}
