using System.Data;
using System.Xml;

namespace SeraphimEngine.ContentPipeline.Message.Serialization
{
    /// <summary>
    /// Class MessageDataDeserializer.
    /// </summary>
    public class MessageDataDeserializer : XmlDeserializer<MessageData>
    {
        /// <summary>
        /// Retrieves the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>MessageData.</returns>
        /// <exception cref="System.Data.DataException">$Dimensions in {element.LocalName} does not have 4 parameters</exception>
        public override MessageData Retrieve(XmlElement element)
        {
            string[] dimensions = element.GetAttribute("Dimensions").Split(',');
            if (dimensions.Length != 4)
                throw new DataException($"Dimensions in {element.LocalName} does not have 4 parameters");

            return new MessageData {
                X = dimensions[0],
                Y = dimensions[1],
                Width = dimensions[2],
                Height = dimensions[3],
                FontName = element.GetAttribute("FontName"),
                FontSize = System.Convert.ToInt32(element.GetAttribute("FontSize")),
                Text = element.InnerText
            };
        }
    }
}
