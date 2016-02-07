using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.Map
{
    /// <summary>
    /// Class SeraphimLayer.
    /// </summary>
    public class SeraphimLayer : ISeraphimLayer
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether this <see cref="ISeraphimLayer" /> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ISeraphimLayer" /> is passable.
        /// </summary>
        /// <value><c>true</c> if passable; otherwise, <c>false</c>.</value>
        public bool Passable { get; }

        /// <summary>
        /// Gets the tiles.
        /// </summary>
        /// <value>The tiles.</value>
        public List<ISeraphimTile> Tiles { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SeraphimLayer" /> class.
        /// </summary>
        /// <param name="visible">if set to <c>true</c> [visible].</param>
        /// <param name="passable">if set to <c>true</c> [passable].</param>
        /// <param name="tiles">The tiles.</param>
        public SeraphimLayer(bool visible, bool passable, List<ISeraphimTile> tiles)
        {
            Visible = visible;
            Passable = passable;
            Tiles = tiles;
        }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(GameTime gameTime)
        {
            if (!Visible)
                return;

            foreach (ISeraphimTile tile in Tiles)
                tile.Update(gameTime);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Draw(GameTime gameTime)
        {
            if (!Visible)
                return;
            
            for (int i = 0; i < Tiles.Count; ++i)
                Tiles[i].Draw(gameTime);
        }

        #endregion
    }
}
