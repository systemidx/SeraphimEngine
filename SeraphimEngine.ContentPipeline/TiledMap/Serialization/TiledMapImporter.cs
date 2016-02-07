using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SeraphimEngine.ContentPipeline.TiledMap.Serialization
{
    /// <summary>
    /// Class ScriptImporter.
    /// </summary>
    [ContentImporter("tmx", DefaultProcessor = "TiledMapProcessor", DisplayName = "Seraphim Importer - Tiled Map")]
    public class TiledMapImporter : ContentImporter<string>
    {
        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.String.</returns>
        public override string Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing TIDE file: {0}", filename);
            return File.ReadAllText(filename);
        }
    }
}