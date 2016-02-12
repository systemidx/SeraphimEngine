using Microsoft.Xna.Framework;
using SeraphimEngine.Definitions;

namespace SeraphimEngine.Helpers.Definitions
{
    /// <summary>
    /// Class RectangleExtensions.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// To the rectangle f.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>RectangleF.</returns>
        public static RectangleF ToRectangleF(this Rectangle rectangle)
        {
            return new RectangleF(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}
