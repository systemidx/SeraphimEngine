using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

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
        Rectangle SourceRectangle { get; }

        /// <summary>
        /// Gets the destination rectangle.
        /// </summary>
        /// <value>The destination rectangle.</value>
        Rectangle DestinationRectangle { get; }
    }
}