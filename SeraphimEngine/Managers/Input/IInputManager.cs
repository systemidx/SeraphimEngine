using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace SeraphimEngine.Managers.Input
{
    /// <summary>
    /// Interface IInputManager
    /// </summary>
    public interface IInputManager : IUpdate
    {
        /// <summary>
        /// Determines whether a specified key is held down.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if [is key down] [the specified key]; otherwise, <c>false</c>.</returns>
        bool IsKeyDown(Keys key);
    }
}