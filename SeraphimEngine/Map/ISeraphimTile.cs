using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace SeraphimEngine.Map
{
    /// <summary>
    /// Interface ISeraphimTile
    /// </summary>
    public interface ISeraphimTile : IDraw, IUpdate
    {
        /// <summary>
        /// Gets the animation interval.
        /// </summary>
        /// <value>The animation interval.</value>
        int AnimationInterval { get; set; }

        /// <summary>
        /// Gets the index of the animation frame.
        /// </summary>
        /// <value>The index of the animation frame.</value>
        int AnimationFrameIndex { get; }

        /// <summary>
        /// Gets the size of the tile.
        /// </summary>
        /// <value>The size of the tile.</value>
        Point TileSize { get; set; }

        /// <summary>
        /// Gets the collision position.
        /// </summary>
        /// <value>The collision position.</value>
        Vector2 CollisionOrigin { get; set; }

        /// <summary>
        /// Gets the collision dimensions.
        /// </summary>
        /// <value>The collision dimensions.</value>
        Vector2 CollisionDimensions { get; set; }

        /// <summary>
        /// Gets the frames.
        /// </summary>
        /// <value>The frames.</value>
        List<ISeraphimTileFrame> Frames { get; set; }

        /// <summary>
        /// Determines whether the specified source is colliding.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source is colliding; otherwise, <c>false</c>.</returns>
        bool IsColliding(Rectangle source);
    }
}