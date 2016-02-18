using SeraphimEngine.Content;
using SeraphimEngine.ContentPipeline.Message;

namespace SeraphimEngine.Gui.MessageBox.ConversionObjects
{
    /// <summary>
    /// Interface IMessageGuiConverter
    /// </summary>
    public interface IMessageGuiConverter : IContentConvertible<MessageData, IMessageBoxGui>
    {
        /// <summary>
        /// Gets the converter.
        /// </summary>
        /// <value>The converter.</value>
        IVariableConvertible<MessageData> VariableConverter { get; }
    }
}