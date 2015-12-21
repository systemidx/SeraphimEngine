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
        /// <summary>
        /// Gets the input method.
        /// </summary>
        /// <value>The input method.</value>
        ActionInputMethod InputMethod { get; }

        /// <summary>
        /// Determines whether [is actionMapping down] [the specified actionMapping].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if [is actionMapping down] [the specified actionMapping]; otherwise, <c>false</c>.</returns>
        bool IsActionDown(InputAction action);

        /// <summary>
        /// Determines whether [is actionMapping held] [the specified actionMapping].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if [is actionMapping held] [the specified actionMapping]; otherwise, <c>false</c>.</returns>
        bool IsActionHeld(InputAction action);
    }
}