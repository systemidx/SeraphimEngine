using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using SeraphimEngine.Content;
using SeraphimEngine.ContentPipeline.Message;
using SeraphimEngine.Helpers.Asset;
using SeraphimEngine.Managers.Gui;

namespace SeraphimEngine.Gui.MessageBox.ConversionObjects
{
    /// <summary>
    /// Class MessageGuiConverter.
    /// </summary>
    public class MessageGuiConverter : IMessageGuiConverter
    {
        /// <summary>
        /// Gets the converter.
        /// </summary>
        /// <value>The converter.</value>
        public IVariableConvertible<MessageData> VariableConverter { get; } = new VariableConverter<MessageData>();

        /// <summary>
        /// Converts the specified content model.
        /// </summary>
        /// <param name="contentModel">The content model.</param>
        /// <returns>SeraphimEngine.Gui.MessageBox.IMessageBoxGui.</returns>
        public IMessageBoxGui Convert(MessageData contentModel)
        {
            contentModel = VariableConverter.ConvertVariables(contentModel);

            BitmapFont font = GetFont(contentModel);
            return new MessageBoxGui(contentModel.Text, GetDimensions(contentModel, font), font);
        }

        /// <summary>
        /// Gets the font.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>MonoGame.Extended.BitmapFonts.BitmapFont.</returns>
        private BitmapFont GetFont(MessageData data)
        {
            return GuiManager.Instance.GetFont(data.FontName, System.Convert.ToInt32(data.FontSize));
        }

        /// <summary>
        /// Gets the dimensions.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="font">The font.</param>
        /// <returns>Rectangle.</returns>
        private Rectangle GetDimensions(MessageData data, BitmapFont font)
        {
            Point position = new Point(System.Convert.ToInt32(data.X), System.Convert.ToInt32(data.Y));
            Vector2 textSize = font.GetTextSize(data.Text);

            int width;
            if (string.Compare(data.Width, "auto", StringComparison.InvariantCultureIgnoreCase) == 0)
                width = System.Convert.ToInt32(textSize.X);
            else if (string.Compare(data.Width, "total", StringComparison.InvariantCultureIgnoreCase) == 0)
                width = GuiManager.Instance.MaxTextWidth;
            else
                width = System.Convert.ToInt32(data.Width);

            int height;
            if (string.Compare(data.Width, "auto", StringComparison.InvariantCultureIgnoreCase) == 0)
                height = System.Convert.ToInt32(textSize.Y);
            else if (string.Compare(data.Width, "total", StringComparison.InvariantCultureIgnoreCase) == 0)
                height = GuiManager.Instance.MaxTextHeight;
            else
                height = System.Convert.ToInt32(data.Height);

            return new Rectangle(position, new Point(width, height));
        }
    }
}
