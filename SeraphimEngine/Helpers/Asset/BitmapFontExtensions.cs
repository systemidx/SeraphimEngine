using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;

namespace SeraphimEngine.Helpers.Asset
{
    /// <summary>
    /// Class BitmapFontExtensions.
    /// </summary>
    public static class BitmapFontExtensions
    {
        /// <summary>
        /// Gets the size of the text.
        /// </summary>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <returns>Vector2.</returns>
        public static Vector2 GetTextSize(this BitmapFont font, string text)
        {
            IDictionary<char, BitmapFontRegion> regions = new Dictionary<char, BitmapFontRegion>();

            int width = 0;
            int highestHeight = 0;

            for (int i = 0; i < text.Length; ++i)
            {
                if (!regions.ContainsKey(text[i]))
                    regions.Add(text[i], font.GetCharacterRegion(text[i]));

                BitmapFontRegion region = regions[text[i]];
                width += region.Width;
                if (highestHeight < region.Height)
                    highestHeight = region.Height;
            }

            return new Vector2(width, highestHeight);
        }
    }
}
