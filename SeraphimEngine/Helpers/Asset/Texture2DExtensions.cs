using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Definitions;

namespace SeraphimEngine.Helpers.Asset
{
    /// <summary>
    /// Class Texture2DExtensions.
    /// </summary>
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Gets the matrix.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <returns>Matrix3&lt;RectangleF&gt;.</returns>
        public static Matrix3<RectangleF> GetMatrix(this Texture2D texture)
        {
            Matrix3<RectangleF> textureMatrix = new Matrix3<RectangleF>();

            float width = (float)texture.Width / 3;
            float height = (float)texture.Height / 3;

            for (int col = 0; col < 3; col++)
                for (int row = 0; row < 3; row++)
                    textureMatrix[row, col] = new RectangleF(col * width, row * height, width, height);

            return textureMatrix;
        }

        /// <summary>
        /// Gets the dimensions.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 GetDimensions(this Texture2D texture)
        {
            return new Vector2(texture.Width, texture.Height);
        }
    }
}
