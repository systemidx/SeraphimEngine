using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine.Managers {
    public abstract class Manager<TManagerType> : IManager where TManagerType : new() {
        public abstract bool IsInitialized { get; protected set; }
        public abstract void Initialize(ContentManager content, GraphicsDevice graphics);

        /// <summary>
        /// The instance
        /// </summary>
        public static readonly TManagerType Instance = new Lazy<TManagerType>(() => new TManagerType()).Value;
    }
}