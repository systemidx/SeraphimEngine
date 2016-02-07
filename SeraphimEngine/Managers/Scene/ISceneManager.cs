using System;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Scene;
using Camera2D = SeraphimEngine.Camera.Camera2D;

namespace SeraphimEngine.Managers.Scene
{
    /// <summary>
    /// Interface ISceneManager
    /// </summary>
    public interface ISceneManager : IManager, IUpdate, IDraw
    {
        /// <summary>
        /// Gets the viewport adapter.
        /// </summary>
        /// <value>The viewport adapter.</value>
        ViewportAdapter ViewportAdapter { get; }

        /// <summary>
        /// Gets the current scene.
        /// </summary>
        /// <value>The current scene.</value>
        IScene CurrentScene { get; }

        /// <summary>
        /// Gets the camera.
        /// </summary>
        /// <value>The camera.</value>
        Camera2D Camera { get; }

        /// <summary>
        /// Switches the scene.
        /// </summary>
        /// <param name="sceneType">Type of the scene.</param>
        void SwitchScene(Type sceneType);
    }
}