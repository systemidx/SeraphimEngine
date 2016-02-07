using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Input;
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

        #region Public Methods

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
            _graphics.Clear(Color.Black);
            SpriteBatch.Begin(transformMatrix: SceneManager.Instance.Camera.ViewMatrix);
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
    }
}
