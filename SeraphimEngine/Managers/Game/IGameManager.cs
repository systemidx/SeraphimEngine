using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine.Managers.Game
{
    /// <summary>
    /// Interface IGameManager
    /// </summary>
    public interface IGameManager
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether [should exit].
        /// </summary>
        /// <value><c>true</c> if [should exit]; otherwise, <c>false</c>.</value>
        bool ShouldExit { get; }

        /// <summary>
        /// Gets the sprite batch.
        /// </summary>
        /// <value>The sprite batch.</value>
        SpriteBatch SpriteBatch { get; }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Starts the drawing.
        /// </summary>
        void StartDrawing();

        /// <summary>
        /// Stops the drawing.
        /// </summary>
        void StopDrawing();

        /// <summary>
        /// Checks to see if the game should exit. If given a boolean parameter, the game will exit if true.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        void Exit();

        #endregion

        #region Methods

        /// <summary>
        /// Gets the game variable.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>TGameVarOutputType.</returns>
        dynamic GetGameVariable(string key);

        /// <summary>
        /// Sets the game variable.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        void SetGameVariable(string key, object value);

        #endregion
    }
}