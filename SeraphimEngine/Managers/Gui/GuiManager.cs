using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Managers.Gui
{
    /// <summary>
    /// Class GuiManager.
    /// </summary>
    public class GuiManager : Manager<GuiManager>, IGuiManager
    {
        #region Public Assets

        /// <summary>
        /// The GUI cursor texture
        /// </summary>
        public Texture2D GuiCursorTexture;

        /// <summary>
        /// The GUI container texture
        /// </summary>
        public Texture2D GuiContainerTexture;

        /// <summary>
        /// The GUI rollover sound
        /// </summary>
        public SoundEffect GuiRolloverSound;

        /// <summary>
        /// The GUI select sound
        /// </summary>
        public SoundEffect GuiSelectSound;

        /// <summary>
        /// The GUI rollover sound instance
        /// </summary>
        public SoundEffectInstance GuiRolloverSoundInstance;

        /// <summary>
        /// The GUI rollover select instance
        /// </summary>
        public SoundEffectInstance GuiRolloverSelectInstance;

        #endregion

        #region Private Assets

        /// <summary>
        /// The current song
        /// </summary>
        private Song _currentSong;

        /// <summary>
        /// The default font
        /// </summary>
        private BitmapFont _defaultFont;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the fonts.
        /// </summary>
        /// <value>The fonts.</value>
        public IDictionary<string, BitmapFont> Fonts { get; } = new Dictionary<string, BitmapFont>();

        /// <summary>
        /// Gets or sets the is initialized.
        /// </summary>
        /// <value>The is initialized.</value>
        public override bool IsInitialized { get; protected set; }

        /// <summary>
        /// Gets or sets the maximum width of the text.
        /// </summary>
        /// <value>The maximum width of the text.</value>
        public int MaxTextWidth => (int)SceneManager.Instance.Camera.Resolution.X - (GuiContainerTexture.Width / 3 * 2);

        /// <summary>
        /// Gets or sets the maximum height of the text.
        /// </summary>
        /// <value>The maximum height of the text.</value>
        public int MaxTextHeight => (int)SceneManager.Instance.Camera.Resolution.Y - (GuiContainerTexture.Height / 3 * 2);
        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            GuiContainerTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/menu", "menu");
            GuiCursorTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/menu", "cursor");

            GuiRolloverSound = AssetManager.Instance.GetAsset<SoundEffect>("sounds", "rollover");
            GuiRolloverSoundInstance = GuiRolloverSound.CreateInstance();

            GuiSelectSound = AssetManager.Instance.GetAsset<SoundEffect>("sounds", "select");
            GuiRolloverSelectInstance = GuiSelectSound.CreateInstance();

            Fonts.Add("default_32", AssetManager.Instance.GetAsset<BitmapFont>("fonts", "default_32"));
            Fonts.Add("default_64", AssetManager.Instance.GetAsset<BitmapFont>("fonts", "default_64"));

            _defaultFont = Fonts["default_32"];

            IsInitialized = true;
        }

        #endregion

        #region Font Methods

        /// <summary>
        /// Gets the font.
        /// </summary>
        /// <returns>MonoGame.Extended.BitmapFonts.BitmapFont.</returns>
        public BitmapFont GetFont()
        {
            return _defaultFont;
        }

        /// <summary>
        /// Gets the font.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="size">The size.</param>
        /// <returns>MonoGame.Extended.BitmapFonts.BitmapFont.</returns>
        public BitmapFont GetFont(string name, int size)
        {
            string key = $"{name}_{size}";

            return Fonts.ContainsKey(key) ? Fonts[key] : _defaultFont;
        }

        #endregion

        #region Sound Effect Methods

        /// <summary>
        /// Plays the sound effect.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public void PlaySoundEffect(SoundEffectInstance instance)
        {
            instance?.Stop();
            instance?.Play();
        }

        #endregion

        #region Music Methods

        /// <summary>
        /// Plays the music.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="repeat">The repeat.</param>
        public void PlayMusic(Song song, bool repeat = true)
        {
            if (_currentSong == song)
                return;

            _currentSong = song;
            MediaPlayer.Stop();

            if (MediaPlayer.State != MediaState.Stopped)
                return;

            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = repeat;
        }

        /// <summary>
        /// Stops the music.
        /// </summary>
        public void StopMusic()
        {
            _currentSong = null;
            MediaPlayer.Stop();
        }

        #endregion

        /// <summary>
        /// Gets the GUI container dimensions.
        /// </summary>
        /// <returns>Microsoft.Xna.Framework.Vector2.</returns>
        public Vector2 GetGuiContainerDimensions()
        {
            return new Vector2(GuiContainerTexture.Width, GuiContainerTexture.Height);
        }
    }
}
