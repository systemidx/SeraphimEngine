using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace SeraphimEngine.Managers.Gui
{
    /// <summary>
    /// Interface IInputManager
    /// </summary>
    public interface IGuiManager
    {
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