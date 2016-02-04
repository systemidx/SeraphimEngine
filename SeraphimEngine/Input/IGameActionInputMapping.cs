using Microsoft.Xna.Framework.Input;
using SeraphimEngine.Input.Enumerations;

namespace SeraphimEngine.Input
{
    /// <summary>
    /// Interface IGameActionInputMapping
    /// </summary>
    public interface IGameActionInputMapping
    {
        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        GameAction GameActionEvent { get; }
        
        /// <summary>
        /// Gets the actionMapping key.
        /// </summary>
        /// <value>The actionMapping key.</value>
        Keys[] ActionKeys { get; }

        /// <summary>
        /// Gets the actionMapping button.
        /// </summary>
        /// <value>The actionMapping button.</value>
        Buttons[] ActionButtons { get; } 
    }
}
