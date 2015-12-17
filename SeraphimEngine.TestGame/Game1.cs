using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeraphimEngine.Managers;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Managers.Script;

namespace SeraphimEngine.TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            InputManager.Instance.Initialize(Content, _graphics.GraphicsDevice);
            SceneManager.Instance.Initialize(Content, _graphics.GraphicsDevice);
            AssetManager.Instance.Initialize(Content, _graphics.GraphicsDevice);
            ScriptManager.Instance.Initialize(Content, _graphics.GraphicsDevice);
            
            //ScriptRunner runner = new ScriptRunner();
            //runner.RunScript("test");

            _graphics.PreferredBackBufferWidth = 600;
            _graphics.PreferredBackBufferHeight = 480;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            SceneManager.Instance.SwitchScene("");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            InputManager.Instance.Update(gameTime);
            ScriptManager.Instance.Update(gameTime);
            SceneManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ScriptManager.Instance.Draw(gameTime);
            SceneManager.Instance.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
