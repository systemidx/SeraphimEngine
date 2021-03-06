﻿using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SeraphimEngine.ContentPipeline.Script
{
    /// <summary>
    /// Class ScriptImporter.
    /// </summary>
    [ContentImporter("cs", DefaultProcessor = "ScriptProcessor", DisplayName = "Seraphim Importer - Script")]
    public class ScriptImporter : ContentImporter<string>
    {
        /// <summary>
        /// Imports the specified file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="context">The context.</param>
        /// <returns>System.String.</returns>
        public override string Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing CS file: {0}", filename);
            return File.ReadAllText(filename);
        }
    }
}