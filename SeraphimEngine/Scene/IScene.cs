using System;
using MonoGame.Extended;
using SeraphimEngine.Gui;
using SeraphimEngine.Gui.Menu;

namespace SeraphimEngine.Scene
{
    /// <summary>
    /// Interface IScene
    /// </summary>
    public interface IScene : IUpdate, IDraw
    {
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
        void RegisterMenu(IMenuGui menu);

        /// <summary>
        /// Unloads the menu.
        /// </summary>
        /// <param name="menuId">The menu identifier.</param>
        void UnloadMenu(string menuId);
    }
}