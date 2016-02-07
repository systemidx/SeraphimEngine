using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SeraphimEngine.ContentPipeline.TiledMap.Serialization
{
    /// <summary>
    /// Class ScriptProcessor.
    /// </summary>
    [ContentProcessor(DisplayName = "Seraphim Processor - Tiled Map")]
    public class TiledMapProcessor : ContentProcessor<string, TiledMap>
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>ScriptMetaData.</returns>
        public override TiledMap Process(string input, ContentProcessorContext context)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(input);

            TiledMapDeserializer deserializer = new TiledMapDeserializer();
            return deserializer.Retrieve(document.DocumentElement);
        }
    }
}