using Microsoft.Xna.Framework.Input;

namespace SeraphimEngine.Input
{
    /// <summary>
    /// Interface IInputAction
    /// </summary>
    public interface IInputAction
    {
        /// <summary>
        /// Gets the action key.
        /// </summary>
        /// <value>The action key.</value>
        Keys ActionKey { get; }

        /// <summary>
        /// Gets the action button.
        /// </summary>
        /// <value>The action button.</value>
        Buttons ActionButton { get; } 
    }
}
