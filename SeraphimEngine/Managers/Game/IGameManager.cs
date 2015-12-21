using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace SeraphimEngine.Managers.Game
{
    public interface IGameManager
    {
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
    }
}