using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace SeraphimEngine.Map
{
    public interface ISeraphimMap : IDraw, IUpdate
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        Vector2 Size { get; }

        /// <summary>
        /// Gets the layers.
        /// </summary>
        /// <value>The layers.</value>
        List<ISeraphimLayer> Layers { get; }

        /// <summary>
        /// Moves the map by a magnitude.
        /// </summary>
        /// <param name="magnitude">The magnitude.</param>
        void MoveBy(Vector2 magnitude);
    }
}