using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Helpers;
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
            SourceRectangle = sourceRectangle;
            DestinationRectangle = destinationRectangle;
        }

        public void Draw(GameTime gameTime)
        {
            if (SceneManager.Instance.Camera.IsInCameraView(DestinationRectangle))
                GameManager.Instance.SpriteBatch.Draw(SourceTexture, destinationRectangle: DestinationRectangle, sourceRectangle: SourceRectangle, origin: SceneManager.Instance.Camera.Origin);
        }

        public Texture2D SourceTexture { get; }
        public Rectangle SourceRectangle { get; }
        public Rectangle DestinationRectangle { get; }
    }
}
