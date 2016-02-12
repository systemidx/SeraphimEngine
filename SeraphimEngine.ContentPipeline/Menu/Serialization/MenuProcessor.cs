using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SeraphimEngine.ContentPipeline.Menu.Serialization
{
    /// <summary>
    /// Class ScriptProcessor.
    /// </summary>
    [ContentProcessor(DisplayName = "Seraphim Processor - Menu")]
    public class MenuProcessor : ContentProcessor<string, MenuData>
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>MenuMetaData.</returns>
        public override MenuData Process(string input, ContentProcessorContext context)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            MenuDataDeserializer deserializer = new MenuDataDeserializer();
            return deserializer.Retrieve(document.DocumentElement);
        }
    }
}