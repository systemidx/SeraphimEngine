using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Definitions;
using SeraphimEngine.Helpers.Definitions;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Map
{
    /// <summary>
    /// Class SeraphimTileFrame.
    /// </summary>
    public class SeraphimTileFrame : ISeraphimTileFrame
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SeraphimTileFrame"/> class.
        /// </summary>
        /// <param name="sourceTexture">The source texture.</param>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <param name="destinationRectangle">The destination rectangle.</param>
        public SeraphimTileFrame(Texture2D sourceTexture, Rectangle sourceRectangle, Rectangle destinationRectangle)
        {
            SourceTexture = sourceTexture;
            SourceRectangle = sourceRectangle.ToRectangleF();
            DestinationRectangle = destinationRectangle.ToRectangleF();
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            if (SceneManager.Instance.Camera.IsInCameraView(DestinationRectangle))
                GameManager.Instance.SpriteBatch.Draw(SourceTexture, DestinationRectangle.ToRectangle(), SourceRectangle.ToRectangle(), Color.White, 0.0f, SceneManager.Instance.Camera.Origin, SpriteEffects.None, 0.0f);
        }

        public Texture2D SourceTexture { get; }
        public RectangleF SourceRectangle { get; }
        public RectangleF DestinationRectangle { get; }
    }
}
