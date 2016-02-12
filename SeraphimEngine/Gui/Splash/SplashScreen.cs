using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SeraphimEngine.Helpers.Asset;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Gui;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Gui.Splash
{
    /// <summary>
    /// Class SplashScreen.
    /// </summary>
    public class SplashScreen : ISplashScreen
    {
        #region Private Variables

        /// <summary>
        /// The background music
        /// </summary>
        private readonly Song _music;

        /// <summary>
        /// The background image
        /// </summary>
        private readonly Texture2D _image;

        /// <summary>
        /// The on finish action. This will likely be a scene switch event or showing another splash screen
        /// </summary>
        private readonly Action _onFinish;

        /// <summary>
        /// The texture fader
        /// </summary>
        private readonly TextureFader _fader = new TextureFader();

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this instance is showing.
        /// </summary>
        /// <value><c>true</c> if this instance is showing; otherwise, <c>false</c>.</value>
        public bool IsShowing { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen"/> class.
        /// </summary>
        /// <param name="onFinish">The on finish.</param>
        /// <param name="musicName">Name of the music.</param>
        /// <param name="musicPath">The music path.</param>
        /// <param name="bgImagePath">The bg image path.</param>
        /// <param name="bgImageName">Name of the bg image.</param>
        public SplashScreen(Action onFinish, string musicName, string musicPath, string bgImagePath, string bgImageName)
        {
            _onFinish = onFinish;
            _music = AssetManager.Instance.GetAsset<Song>(musicPath, musicName);
            _image = AssetManager.Instance.GetAsset<Texture2D>(bgImagePath, bgImageName);
        }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            if (!IsShowing)
                return;

            GameManager.Instance.SpriteBatch.Draw(
                texture: _image,
                color: Color.White * _fader.FadeAlpha, 
                destinationRectangle: new Rectangle(0, 0, (int)SceneManager.Instance.Camera.VirtualResolution.X, (int)SceneManager.Instance.Camera.VirtualResolution.Y));
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (!IsShowing)
                return;

            GuiManager.Instance.PlayMusic(_music);

            _fader.Update(gameTime);

            if (!_fader.DoneFading)
                return;

            if (!_fader.HasChangedDirections)
            { 
                _fader.ChangeDirection();
                return;
            }

            IsShowing = false;
            _onFinish.Invoke();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Shows this instance.
        /// </summary>
        public void Show()
        {
            IsShowing = !IsShowing;
        }

        #endregion
    }
}
