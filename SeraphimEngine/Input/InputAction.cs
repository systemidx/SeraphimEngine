using Microsoft.Xna.Framework.Input;

namespace SeraphimEngine.Input
{
    /// <summary>
    /// Class InputAction.
    /// </summary>
    public class InputAction : IInputAction
    {
        /// <summary>
        /// Gets the action key.
        /// </summary>
        /// <value>The action key.</value>
        public Keys ActionKey { get; }

        /// <summary>
        /// Gets the action button.
        /// </summary>
        /// <value>The action button.</value>
        public Buttons ActionButton { get; } 
    }
}
