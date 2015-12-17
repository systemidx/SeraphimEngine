using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Exceptions;
using SeraphimEngine.Scene;

namespace SeraphimEngine.Managers.Scene {

    /// <summary>
    /// Class SceneManager.
    /// </summary>
    public class SceneManager : Manager<SceneManager>, ISceneManager {
        
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
        public override bool IsInitialized { get; protected set; } = false;

        #endregion

        #region Override Methods

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics) {
            _content = content;
            _graphics = graphics;
            ViewportAdapter = new BoxingViewportAdapter(_graphics, 1920, 1080);

            IsInitialized = true;
        }

        #endregion

        public void SwitchScene(string sceneId) {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            //CurrentScene?.Exit();
            //CurrentScene?.Script.OnEnter();
            CurrentScene = LoadScene();
            //CurrentScene.Script.OnExit();
        }

        private IScene LoadScene() {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            IScene scene = new CustomScene(_graphics, ViewportAdapter);
            scene.Load();

            return scene;
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