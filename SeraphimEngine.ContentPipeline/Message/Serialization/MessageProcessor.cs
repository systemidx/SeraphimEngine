using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SeraphimEngine.ContentPipeline.Message.Serialization
{
    /// <summary>
    /// Class ScriptProcessor.
    /// </summary>
    [ContentProcessor(DisplayName = "Seraphim Processor - Message")]
    public class MessageProcessor : ContentProcessor<string, MessageData>
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>MenuMetaData.</returns>
        public override MessageData Process(string input, ContentProcessorContext context)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            MessageDataDeserializer deserializer = new MessageDataDeserializer();
            return deserializer.Retrieve(document.DocumentElement);
        }
    }
}