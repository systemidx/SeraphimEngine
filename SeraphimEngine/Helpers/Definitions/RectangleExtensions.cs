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

        /// <summary>
        /// Gets the coordinates.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 GetPosition(this Rectangle rectangle)
        {
            return new Vector2(rectangle.X, rectangle.Y);
        }

        /// <summary>
        /// Gets the dimensions.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 GetDimensions(this Rectangle rectangle)
        {
            return new Vector2(rectangle.Width, rectangle.Height);
        }
    }
}
