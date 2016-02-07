using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Camera;
using SeraphimEngine.Exceptions;
using SeraphimEngine.Scene;

namespace SeraphimEngine.Managers.Scene
{
    /// <summary>
    /// Class SceneManager.
    /// </summary>
    public class SceneManager : Manager<SceneManager>, ISceneManager
    {
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
        /// Gets the camera.
        /// </summary>
        /// <value>The camera.</value>
        public Camera2D Camera { get; private set; }

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
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            _content = content;
            _graphics = graphics;

            ViewportAdapter = new ScalingViewportAdapter(graphics, 320, 240);
            Camera = new Camera2D(ViewportAdapter);

            IsInitialized = true;
        }

        #endregion

        #region SeraphimScene Methods

        /// <summary>
        /// Switches the scene.
        /// </summary>
        /// <param name="sceneId">The scene identifier.</param>
        /// <exception cref="SceneManagerInitializationException"></exception>
        public void SwitchScene(string sceneId)
        {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            CurrentScene?.Unload();
            CurrentScene = LoadScene(null);
        }

        /// <summary>
        /// Switches the scene.
        /// </summary>
        /// <param name="sceneType">Type of the scene.</param>
        /// <exception cref="SceneManagerInitializationException"></exception>
        public void SwitchScene(Type sceneType)
        {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            //Try to unload the current scene if it exists. If it doesn't, it's likely that we haven't init'd the initial scene yet
            CurrentScene?.Unload();

            //Set the new scene and run the load routine
            CurrentScene = LoadScene(sceneType);
        }

        /// <summary>
        /// Loads the scene.
        /// </summary>
        /// <returns>IScene.</returns>
        /// <exception cref="SceneManagerInitializationException"></exception>
        private IScene LoadScene(Type t)
        {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            IScene scene = (IScene) Activator.CreateInstance(t, _graphics);
            if (scene == null)
                throw new SceneInitializationException();

            scene.Load();
            return scene;
        }

        #endregion

        #region Game Loop Methods

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="SceneManagerInitializationException"></exception>
        /// <exception cref="SceneInitializationException"></exception>
        public void Update(GameTime gameTime)
        {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            if (CurrentScene == null)
                throw new SceneInitializationException();

            CurrentScene.Update(gameTime);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="SceneManagerInitializationException"></exception>
        /// <exception cref="SceneInitializationException"></exception>
        public void Draw(GameTime gameTime)
        {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            if (CurrentScene == null)
                throw new SceneInitializationException();

            CurrentScene.Draw(gameTime);
        }

        #endregion
    }
}