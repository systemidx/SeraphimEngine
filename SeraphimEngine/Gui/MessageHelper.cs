using Microsoft.Xna.Framework;
using SeraphimEngine.Definitions;

namespace SeraphimEngine.Gui
{
    /// <summary>
    /// Class MessageHelper.
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// Creates the container destination matrix.
        /// </summary>
        /// <param name="startPosition">The start position.</param>
        /// <param name="textureDimensions">The texture dimensions.</param>
        /// <param name="textDimensions">The text dimensions.</param>
        /// <returns>Matrix3&lt;RectangleF&gt;.</returns>
        public static Matrix3<RectangleF> CreateContainerDestinationMatrix(Vector2 startPosition, Vector2 textureDimensions, Vector3 textDimensions)
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            Matrix3<RectangleF> matrix = new Matrix3<RectangleF>();
            
            Vector2 cornerDimensions = textureDimensions;
            Vector2 lengthDimensions = new Vector2(textDimensions.X, textureDimensions.Y);
            Vector2 heightDimensions = new Vector2(textureDimensions.X, textDimensions.Y / textDimensions.Z);

            matrix[0, 0] = new RectangleF(startPosition.X, startPosition.Y, cornerDimensions);
            matrix[0, 1] = new RectangleF(matrix[0, 0].Right, matrix[0, 0].Top, lengthDimensions);
            matrix[0, 2] = new RectangleF(matrix[0, 1].Right, matrix[0, 0].Top, cornerDimensions);

            matrix[1, 0] = new RectangleF(startPosition.X, matrix[0, 0].Bottom, heightDimensions);
            matrix[1, 1] = new RectangleF(matrix[0, 1].Left, matrix[0, 1].Bottom, lengthDimensions.X, heightDimensions.Y);
            matrix[1, 2] = new RectangleF(matrix[0, 2].Left, matrix[0, 2].Bottom, heightDimensions);

            matrix[2, 0] = new RectangleF(startPosition.X, matrix[1, 0].Bottom, cornerDimensions);
            matrix[2, 1] = new RectangleF(matrix[1, 1].Left, matrix[1, 1].Bottom, lengthDimensions);
            matrix[2, 2] = new RectangleF(matrix[1, 2].Left, matrix[1, 2].Bottom, cornerDimensions);

            return matrix;
        }
    }
}
