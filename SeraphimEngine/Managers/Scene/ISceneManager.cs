using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Scene;

namespace SeraphimEngine.Managers.Scene {
    /// <summary>
    /// Interface ISceneManager
    /// </summary>
    public interface ISceneManager : IManager, IUpdate, IDraw {

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
        /// Switches the scene.
        /// </summary>
        /// <param name="sceneId">The scene identifier.</param>
        void SwitchScene(string sceneId);
    }
}