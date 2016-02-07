using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine.Helpers.SpriteBatch
{
    public static class SpriteBatchExtensions
    {
        public static void DrawShadowedText(this Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, string text, SpriteFont font, Vector2 position, Color color)
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
