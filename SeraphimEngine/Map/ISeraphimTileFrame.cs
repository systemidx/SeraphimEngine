using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SeraphimEngine.Definitions;

namespace SeraphimEngine.Map
{
    public interface ISeraphimTileFrame : IDraw
    {
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <value>The texture.</value>
        Texture2D SourceTexture { get; }

        /// <summary>
        /// Gets the source rectangle.
        /// </summary>
        /// <value>The source rectangle.</value>
        RectangleF SourceRectangle { get; }

        /// <summary>
        /// Gets the destination rectangle.
        /// </summary>
        /// <value>The destination rectangle.</value>
        RectangleF DestinationRectangle { get; }
    }
}