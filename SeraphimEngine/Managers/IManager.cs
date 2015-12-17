using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine.Managers {
    /// <summary>
    /// Interface IManager
    /// </summary>
    public interface IManager {
        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        bool IsInitialized { get; }

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        void Initialize(ContentManager content, GraphicsDevice graphics);
    }
}