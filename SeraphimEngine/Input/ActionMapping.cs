using Microsoft.Xna.Framework.Input;

namespace SeraphimEngine.Input
{
    /// <summary>
    /// Class ActionMapping.
    /// </summary>
    public class ActionMapping : IActionMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionMapping" /> class.
        /// </summary>
        /// <param name="actionKey">The actionMapping key.</param>
        /// <param name="actionButton">The actionMapping button.</param>
        /// <param name="event">The event.</param>
        public ActionMapping(Keys actionKey, Buttons actionButton, InputAction @event)
        {
            ActionKey = actionKey;
            ActionButton = actionButton;
            Event = @event;
        }

        public InputAction Event { get; private set; }

        /// <summary>
        /// Gets the actionMapping key.
        /// </summary>
        /// <value>The actionMapping key.</value>
        public Keys ActionKey { get; private set; }

        /// <summary>
        /// Gets the actionMapping button.
        /// </summary>
        /// <value>The actionMapping button.</value>
        public Buttons ActionButton { get; private set; } 
    }
}
