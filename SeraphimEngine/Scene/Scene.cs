using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Script;

namespace SeraphimEngine.Scene
{
    /// <summary>
    /// Class Scene.
    /// </summary>
    public abstract class Scene : IScene
    {
        #region Member Variables
        
        /// <summary>
        /// The graphics device
        /// </summary>
        protected readonly GraphicsDevice Graphics;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the scene camera.
        /// </summary>
        /// <value>The scene camera.</value>
        public Camera2D SceneCamera { get; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Scene"/> class.
        /// </summary>
        /// <param name="graphics">The graphics.</param>
        /// <param name="viewport">The viewport.</param>
        protected Scene(GraphicsDevice graphics, ViewportAdapter viewport)
        {
            SceneCamera = new Camera2D(viewport);
            Graphics = graphics;
        }

        #endregion

        #region Abstract Methods / Game Flow Methods

        /// <summary>
        /// Loads this instance.
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Unloads this instance.
        /// </summary>
        public abstract void Unload();

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Draw(GameTime gameTime);

        #endregion
    }
}