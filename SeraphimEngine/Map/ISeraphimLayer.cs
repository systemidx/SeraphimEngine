using System.Collections.Generic;
using MonoGame.Extended;

namespace SeraphimEngine.Map
{
    /// <summary>
    /// Interface ISeraphimLayer
    /// </summary>
    public interface ISeraphimLayer : IUpdate, IDraw
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ISeraphimLayer"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        bool Visible { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ISeraphimLayer"/> is passable.
        /// </summary>
        /// <value><c>true</c> if passable; otherwise, <c>false</c>.</value>
        bool Passable { get; }

        /// <summary>
        /// Gets the tiles.
        /// </summary>
        /// <value>The tiles.</value>
        List<ISeraphimTile> Tiles { get; }
    }
}