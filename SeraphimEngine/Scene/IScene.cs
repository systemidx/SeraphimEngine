using System;
using MonoGame.Extended;

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
    }
}