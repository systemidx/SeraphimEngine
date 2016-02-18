using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace SeraphimEngine.Helpers.Asset
{
    /// <summary>
    /// Class SpriteBatchExtensions.
    /// </summary>
    public static class SpriteBatchExtensions
    {
        /// <summary>
        /// Draws the shadowed text.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        public static void DrawShadowedText(this SpriteBatch spriteBatch, string text, BitmapFont font, Vector2 position, Color color)
        {
            const float OPACITY = 0.7f;
            Color blackWithOpacity = Color.Black * OPACITY;

            spriteBatch.DrawString(font, text, new Vector2(position.X - 1, position.Y + 1), blackWithOpacity);
            spriteBatch.DrawString(font, text, new Vector2(position.X - 1, position.Y - 1), blackWithOpacity);
            spriteBatch.DrawString(font, text, new Vector2(position.X - 1, position.Y), blackWithOpacity);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1, position.Y + 1), blackWithOpacity);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1, position.Y - 1), blackWithOpacity);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1, position.Y), blackWithOpacity);
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
