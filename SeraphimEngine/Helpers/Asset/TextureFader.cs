using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace SeraphimEngine.Helpers.Asset
{
    /// <summary>
    ///     Class TextureFader.
    /// </summary>
    public class TextureFader : IUpdate
    {
        #region Properties

        /// <summary>
        /// Gets a value indicating whether [done fading].
        /// </summary>
        /// <value><c>true</c> if [done fading]; otherwise, <c>false</c>.</value>
        public bool DoneFading { get; private set; }

        /// <summary>
        /// Gets the fade alpha.
        /// </summary>
        /// <value>The fade alpha.</value>
        public float FadeAlpha { get; private set; }

        /// <summary>
        /// Gets the fade increment.
        /// </summary>
        /// <value>The fade increment.</value>
        public float FadeIncrement { get; }

        /// <summary>
        /// Gets the fade delay.
        /// </summary>
        /// <value>The fade delay.</value>
        public double FadeDelay { get; private set; }

        /// <summary>
        /// Gets the fade direction.
        /// </summary>
        /// <value>The fade direction.</value>
        public TextureFadeDirection FadeDirection { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance has changed directions.
        /// </summary>
        /// <value><c>true</c> if this instance has changed directions; otherwise, <c>false</c>.</value>
        public bool HasChangedDirections { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="TextureFader" /> class.
        /// </summary>
        /// <param name="fadeIncrement">The fade increment.</param>
        /// <param name="fadeDelay">The fade delay.</param>
        /// <param name="fadeDirection">The fade direction.</param>
        public TextureFader(float fadeIncrement = 0.025f, double fadeDelay = 1.0,
            TextureFadeDirection fadeDirection = TextureFadeDirection.In)
        {
            FadeIncrement = fadeIncrement;
            FadeDelay = fadeDelay;
            FadeDirection = fadeDirection;

            if (FadeDirection == TextureFadeDirection.Out)
                FadeAlpha = 1.0f;
        }

        #endregion

        #region Game Flow Methods

        /// <summary>
        ///     Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (DoneFading)
                return;

            if (FadeAlpha > 1)
            {
                DoneFading = true;
                FadeAlpha = 1.0f;
                return;
            }

            if (FadeAlpha < 0)
            {
                DoneFading = true;
                FadeAlpha = 0.0f;
                return;
            }

            FadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;
            if (FadeDelay > 0)
                return;

            FadeDelay = 0.1;
            FadeAlpha = FadeDirection == TextureFadeDirection.In ? FadeAlpha + FadeIncrement : FadeAlpha - FadeIncrement;
        }

        #endregion

        /// <summary>
        ///     Changes the direction.
        /// </summary>
        public void ChangeDirection()
        {
            HasChangedDirections = true;
            if (FadeDirection == TextureFadeDirection.In)
            {
                FadeDirection = TextureFadeDirection.Out;
                FadeAlpha = 1.0f;
                DoneFading = false;
            }
            else
            {
                FadeDirection = TextureFadeDirection.In;
                FadeAlpha = 0.0f;
                DoneFading = false;
            }
        }
    }
}