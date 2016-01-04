using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine.Helpers.SpriteBatch
{
    public static class SpriteBatchExtensions
    {
        public static void DrawShadowedText(this Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, string text, SpriteFont font, Vector2 position, Color color)
        {
            spriteBatch.DrawString(font, text, new Vector2(position.X - 1, position.Y + 1), Color.Black * 0.7f);
            spriteBatch.DrawString(font, text, new Vector2(position.X - 1, position.Y - 1), Color.Black * 0.7f);
            spriteBatch.DrawString(font, text, new Vector2(position.X - 1, position.Y), Color.Black * 0.7f);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1, position.Y + 1), Color.Black * 0.7f);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1, position.Y - 1), Color.Black * 0.7f);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1, position.Y), Color.Black * 0.7f);
            spriteBatch.DrawString(font, text, position, color);
        }
    }
}
