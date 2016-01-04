using Microsoft.Xna.Framework.Input;

namespace SeraphimEngine.Input
{
    /// <summary>
    /// Interface IActionMapping
    /// </summary>
    public interface IActionMapping
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        InputAction Event { get; }
        
        /// <summary>
        /// Gets the actionMapping key.
        /// </summary>
        /// <value>The actionMapping key.</value>
        Keys ActionKey { get; }

        /// <summary>
        /// Gets the actionMapping button.
        /// </summary>
        /// <value>The actionMapping button.</value>
        Buttons ActionButton { get; } 
    }
}
