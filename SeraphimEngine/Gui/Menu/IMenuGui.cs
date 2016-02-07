using MonoGame.Extended;

namespace SeraphimEngine.Gui.Menu
{
    /// <summary>
    /// Interface IMenuGui
    /// </summary>
    public interface IMenuGui : IDraw, IUpdate
    {
        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        string Id { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        bool IsVisible { get; set; }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();
    }
}