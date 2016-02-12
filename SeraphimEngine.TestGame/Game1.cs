using System;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.TiledMap;
using SeraphimEngine.Input;
using SeraphimEngine.Input.Enumerations;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Managers.Script;
using SeraphimEngine.Map;
using SeraphimEngine.Map.ConversionObjects;
using SeraphimEngine.TestGame.Scenes;

namespace SeraphimEngine.TestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private readonly Point _resolution = new Point(1280, 720);

        public Game1() {
            Content.RootDirectory = "Content";

            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = _resolution.X,
                PreferredBackBufferHeight = _resolution.Y,
                PreferMultiSampling = true
            };
            _graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            GameManager.Instance.Initialize(Content, _graphics.GraphicsDevice);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            SceneManager.Instance.SwitchScene(typeof(InitialSeraphimScene));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GameManager.Instance.ShouldExit)
            {
                try
                {
                    Exit();
                }
                //hack: This is necessary due to a bug in SharpDX in which Exit() causes a null ref exception upon unloading
                catch (NullReferenceException) { }
            }

            if (InputManager.Instance.IsActionDown(GameAction.FullScreen))
            {
                _graphics.IsFullScreen = !_graphics.IsFullScreen;
                _graphics.ApplyChanges();
            }

            GameManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GameManager.Instance.StartDrawing();
            SceneManager.Instance.Draw(gameTime);
            ScriptManager.Instance.Draw(gameTime);
            GameManager.Instance.StopDrawing();

            base.Draw(gameTime);
        }
    }
}
