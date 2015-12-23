using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Scene.Gui;

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

        /// <summary>
        /// The menu container
        /// </summary>
        private readonly List<IMenu> _menus = new List<IMenu>();

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
        public virtual void Update(GameTime gameTime)
        {
            //We alter this collection during the enumeration, so we cannot use a foreach.
            // ReSharper disable ForCanBeConvertedToForeach
            for (int i = 0; i < _menus.Count; ++i)
                _menus[i].Update(gameTime);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Draw(GameTime gameTime)
        {
            foreach (IMenu menu in _menus)
                menu.Draw(gameTime);
        }

        #endregion

        #region GUI Methods

        /// <summary>
        /// Registers the menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        public void RegisterMenu([NotNull]IMenu menu)
        {
            if (_menus.Any(x => string.Equals(x.Id, menu.Id, StringComparison.InvariantCultureIgnoreCase)))
                return;

            menu.Initialize();

            _menus.Add(menu);
        }

        /// <summary>
        /// Unloads the menu.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        public void UnloadMenu(string menuId)
        {
            int idx = _menus.FindIndex(x => string.Equals(x.Id, menuId, StringComparison.InvariantCultureIgnoreCase));
            if (idx == -1)
                return;

            _menus.RemoveAt(idx);
        }

        #endregion
    }
}