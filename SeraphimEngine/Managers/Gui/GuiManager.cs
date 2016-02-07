using JetBrains.Annotations;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using SeraphimEngine.Managers.Asset;

namespace SeraphimEngine.Managers.Gui
{
    public class GuiManager : Manager<GuiManager>, IGuiManager
    {
        public SpriteFont GuiFont;
        public Texture2D GuiCursorTexture;
        public Texture2D GuiContainerTexture;
        public SoundEffect GuiRolloverSound;
        public SoundEffect GuiSelectSound;
        public SoundEffectInstance GuiRolloverSoundInstance;
        public SoundEffectInstance GuiRolloverSelectInstance;

        private Song _currentSong;

        public override bool IsInitialized { get; protected set; }

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            GuiFont = AssetManager.Instance.GetAsset<SpriteFont>("fonts", "default");

            GuiContainerTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/menu", "menu");
            GuiCursorTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/menu", "cursor");

            GuiRolloverSound = AssetManager.Instance.GetAsset<SoundEffect>("sounds", "rollover");
            GuiRolloverSoundInstance = GuiRolloverSound.CreateInstance();

            GuiSelectSound = AssetManager.Instance.GetAsset<SoundEffect>("sounds", "select");
            GuiRolloverSelectInstance = GuiSelectSound.CreateInstance();

            IsInitialized = true;
        }

        public void PlaySoundEffect(SoundEffectInstance instance)
        {
            instance?.Stop();
            instance?.Play();
        }

        /// <summary>
        /// Plays the music.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="repeat">The repeat.</param>
        public void PlayMusic([NotNull]Song song, bool repeat = true)
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
    }
}
