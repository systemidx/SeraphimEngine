using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Exceptions;

namespace SeraphimEngine.Managers.Scene {

    /// <summary>
    /// Class SceneManager.
    /// </summary>
    public class SceneManager : ISceneManager {

        #region Private Members
        /// <summary>
        /// The graphics device
        /// </summary>
        private GraphicsDevice _graphics;

        /// <summary>
        /// The content manager
        /// </summary>
        private ContentManager _content;
        #endregion

        #region Properties

        /// <summary>
        /// Gets the viewport adapter.
        /// </summary>
        /// <value>The viewport adapter.</value>
        public ViewportAdapter ViewportAdapter { get; private set; }

        /// <summary>
        /// Gets the current scene.
        /// </summary>
        /// <value>The current scene.</value>
        public IScene CurrentScene { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public bool IsInitialized { get; private set; } = false;

        #endregion

        #region Private Constructor + Singleton Instance
        /// <summary>
        /// Prevents a default instance of the <see cref="SceneManager"/> class from being created.
        /// </summary>
        private SceneManager() {}

        /// <summary>
        /// The instance
        /// </summary>
        public static readonly SceneManager Instance = new Lazy<SceneManager>(() => new SceneManager()).Value;
        #endregion

        public void Initialize(ContentManager content, GraphicsDevice graphics) {
            _content = content;
            _graphics = graphics;
            ViewportAdapter = new ScalingViewportAdapter(_graphics, 800, 600);

            IsInitialized = true;
        }

        public void SwitchScene(string sceneId) {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            CurrentScene?.Exit();
            CurrentScene = LoadScene();
            CurrentScene.Enter();
        }

        private IScene LoadScene() {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            return new SeraphimEngine.Scene(_graphics, ViewportAdapter);
        }

        public void Update(GameTime gameTime) {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            if (CurrentScene == null)
                throw new SceneInitializationException();

            CurrentScene.Update(gameTime);
        }

        public void Draw(GameTime gameTime) {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            if (CurrentScene == null)
                throw new SceneInitializationException();

            CurrentScene.Draw(gameTime);
        }
    }
}