using Microsoft.Xna.Framework.Input;
using SeraphimEngine.Input.Enumerations;

namespace SeraphimEngine.Input
{
    /// <summary>
    /// Class GameActionInputMapping.
    /// </summary>
    public class GameActionInputMapping : IGameActionInputMapping
    {
        #region Properties

        /// <summary>
        /// Gets the action.
        /// </summary>
        /// <value>The action.</value>
        public GameAction GameActionEvent { get; }

        /// <summary>
        /// Gets the actionMapping key.
        /// </summary>
        /// <value>The actionMapping key.</value>
        public Keys[] ActionKeys { get; }

        /// <summary>
        /// Gets the actionMapping button.
        /// </summary>
        /// <value>The actionMapping button.</value>
        public Buttons[] ActionButtons { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GameActionInputMapping"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="actionKey">The action key.</param>
        /// <param name="actionButton">The action button.</param>
        public GameActionInputMapping(GameAction action, Keys actionKey, Buttons actionButton)
        {
            GameActionEvent = action;
            ActionButtons = new [] {actionButton};
            ActionKeys = new [] { actionKey };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameActionInputMapping"/> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="actionKeys">The action keys.</param>
        /// <param name="actionButtonses">The action buttonses.</param>
        public GameActionInputMapping(GameAction action, Keys[] actionKeys, Buttons[] actionButtonses)
        {
            GameActionEvent = action;
            ActionButtons = actionButtonses;
            ActionKeys = actionKeys;
        }

        #endregion
    }
}