using System;
using MonoGame.Extended;
using SeraphimEngine.Scene.Gui;

namespace SeraphimEngine.Scene
{
    /// <summary>
    /// Interface IScene
    /// </summary>
    public interface IScene : IUpdate, IDraw
    {
        /// <summary>
        /// Gets the scene camera.
        /// </summary>
        /// <value>The scene camera.</value>
        Camera2D SceneCamera { get; }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        void Load();

        /// <summary>
        /// Unloads this instance.
        /// </summary>
        void Unload();

        /// <summary>
        /// Registers the menu.
        /// </summary>
        /// <param name="menu">The menu.</param>
        void RegisterMenu(IMenu menu);

        /// <summary>
        /// Unloads the menu.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        void UnloadMenu(string menuId);
    }
}