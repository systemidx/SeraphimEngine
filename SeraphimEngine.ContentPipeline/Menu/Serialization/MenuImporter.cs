using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SeraphimEngine.ContentPipeline.Menu.Serialization
{
    /// <summary>
    /// Class ScriptImporter.
    /// </summary>
    [ContentImporter("xml", DefaultProcessor = "ScriptProcessor", DisplayName = "Seraphim Importer - Menu")]
    public class MenuImporter : ContentImporter<string>
    {
        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.String.</returns>
        public override string Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing XML file: {0}", filename);
            return File.ReadAllText(filename);
        }
    }
}