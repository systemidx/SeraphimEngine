using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine.Managers
{
    /// <summary>
    /// Class Manager.
    /// </summary>
    /// <typeparam name="TManagerType">The type of the manager type.</typeparam>
    public abstract class Manager<TManagerType> : IManager where TManagerType : new()
    {
        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public abstract bool IsInitialized { get; protected set; }

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public abstract void Initialize(ContentManager content, GraphicsDevice graphics);

        /// <summary>
        /// The thread safe singleton instance
        /// </summary>
        public static readonly TManagerType Instance = new Lazy<TManagerType>(() => new TManagerType()).Value;
    }
}