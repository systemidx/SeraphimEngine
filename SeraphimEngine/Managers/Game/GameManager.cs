using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Gui;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Managers.Script;

namespace SeraphimEngine.Managers.Game
{
    /// <summary>
    /// Class GameManager.
    /// </summary>
    public class GameManager : Manager<GameManager>, IGameManager
    {
        #region Private Variables

        /// <summary>
        /// The graphics device
        /// </summary>
        private GraphicsDevice _graphics;

        /// <summary>
        /// The rasterizer state
        /// </summary>
        private RasterizerState _rasterizerState;

        /// <summary>
        /// The global variables
        /// </summary>
        private readonly IDictionary<string, object> _globalVariables = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets a value indicating whether [should exit].
        /// </summary>
        /// <value><c>true</c> if [should exit]; otherwise, <c>false</c>.</value>
        public bool ShouldExit { get; private set; } = false;

        /// <summary>
        /// Gets the sprite batch.
        /// </summary>
        /// <value>The sprite batch.</value>
        public SpriteBatch SpriteBatch { get; private set; }
        
        /// <summary>
        /// Gets or sets the is initialized.
        /// </summary>
        /// <value>The is initialized.</value>
        public override bool IsInitialized { get; protected set; }

        #endregion

        #region Event Life Cycle

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            _graphics = graphics;
            
            InputManager.Instance.Initialize(content, graphics);
            SceneManager.Instance.Initialize(content, graphics);
            AssetManager.Instance.Initialize(content, graphics);
            ScriptManager.Instance.Initialize(content, graphics);
            GuiManager.Instance.Initialize(content, graphics);

            _rasterizerState = new RasterizerState { MultiSampleAntiAlias = true };
            SpriteBatch = new SpriteBatch(graphics);

            IsInitialized = true;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            InputManager.Instance.Update(gameTime);
            ScriptManager.Instance.Update(gameTime);
            SceneManager.Instance.Update(gameTime);
        }
        
        /// <summary>
        /// Starts the drawing.
        /// </summary>
        public void StartDrawing()
        {
            _graphics.Clear(Color.TransparentBlack);

            SpriteBatch.Begin(transformMatrix: SceneManager.Instance.Camera.ViewMatrix, rasterizerState: _rasterizerState);
        }

        /// <summary>
        /// Stops the drawing.
        /// </summary>
        public void StopDrawing()
        {
            SpriteBatch.End();
        }

        /// <summary>
        /// Exits this instance.
        /// </summary>
        public void Exit()
        {
            ShouldExit = true;
        }

        #endregion

        #region Game Variable Methods

        /// <summary>
        /// Gets the game variable.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>TGameVarOutputType.</returns>
        public dynamic GetGameVariable(string key)
        {
            return _globalVariables.ContainsKey(key) ? _globalVariables[key] : null;
        }

        /// <summary>
        /// Sets the game variable.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void SetGameVariable(string key, object value)
        {
            if (_globalVariables.ContainsKey(key))
                _globalVariables[key] = value;
            _globalVariables.Add(key, value);
        }

        #endregion

    }
}
