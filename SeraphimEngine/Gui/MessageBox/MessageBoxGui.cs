using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using SeraphimEngine.Gui.Menu;
using SeraphimEngine.Helpers.Asset;
using SeraphimEngine.Helpers.Definitions;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Gui;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Gui.MessageBox
{
    public class MessageBoxGui : IMessageBoxGui
    {
        private MessageBoxContainer _container;

        private readonly string _text;
        private readonly Rectangle _dimensions;
        private readonly BitmapFont _font;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxGui" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="dimensions">The dimensions.</param>
        /// <param name="font">The font.</param>
        public MessageBoxGui(string text, Rectangle dimensions, BitmapFont font)
        {
            _text = text;
            _dimensions = dimensions;
            _font = font;

            Initialize();
        }

        public void Draw(GameTime gameTime)
        {
            _container.Draw(gameTime);
            GameManager.Instance.SpriteBatch.DrawShadowedText(_text, GuiManager.Instance.GetFont(), _dimensions.GetPosition(), Color.White);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Initialize()
        {
            _container = new MessageBoxContainer(_dimensions.GetDimensions());
        }

        public void Accept()
        {
        }
    }
}
