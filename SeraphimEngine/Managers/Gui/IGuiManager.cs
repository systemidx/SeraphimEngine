using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.BitmapFonts;

namespace SeraphimEngine.Managers.Gui
{
    /// <summary>
    /// Interface IInputManager
    /// </summary>
    public interface IGuiManager
    {
        #region Fonts

        /// <summary>
        /// Gets the default font.
        /// </summary>
        /// <returns>BitmapFont.</returns>
        BitmapFont GetFont();

        /// <summary>
        /// Gets the font.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="size">The size.</param>
        /// <returns>BitmapFont.</returns>
        BitmapFont GetFont(string name, int size);

        /// <summary>
        /// Gets the fonts.
        /// </summary>
        /// <value>The fonts.</value>
        IDictionary<string, BitmapFont> Fonts { get; }

        #endregion

        #region Sound Effect Methods

        /// <summary>
        /// Plays the sound effect.
        /// </summary>
        /// <param name="instance">The instance.</param>
        void PlaySoundEffect(SoundEffectInstance instance);

        #endregion

        #region Music Methods

        /// <summary>
        /// Plays the music.
        /// </summary>
        /// <param name="song">The song.</param>
        /// <param name="repeat">if set to <c>true</c> [repeat].</param>
        void PlayMusic(Song song, bool repeat);

        /// <summary>
        /// Stops the music.
        /// </summary>
        void StopMusic();

        #endregion
    }
}