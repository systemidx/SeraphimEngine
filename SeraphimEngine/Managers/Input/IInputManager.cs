using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using SeraphimEngine.Input;

namespace SeraphimEngine.Managers.Input
{
    /// <summary>
    /// Interface IInputManager
    /// </summary>
    public interface IInputManager : IUpdate
    {
        ActionInputMethod InputMethod { get; }

        /// <summary>
        /// Determines whether a specified key is held down.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if [is key down] [the specified key]; otherwise, <c>false</c>.</returns>
        bool IsKeyDown(Keys key);

        /// <summary>
        /// Determines whether [is key held] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if [is key held] [the specified key]; otherwise, <c>false</c>.</returns>
        bool IsKeyHeld(Keys key);

        /// <summary>
        /// Determines whether [is action down] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if [is action down] [the specified action]; otherwise, <c>false</c>.</returns>
        bool IsActionDown(IInputAction action);

        /// <summary>
        /// Determines whether [is action held] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if [is action held] [the specified action]; otherwise, <c>false</c>.</returns>
        bool IsActionHeld(IInputAction action);
    }
}