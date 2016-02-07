using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace SeraphimEngine.ContentPipeline.Script
{
    /// <summary>
    /// Class ScriptProcessor.
    /// </summary>
    [ContentProcessor(DisplayName = "Seraphim Processor - Script")]
    public class ScriptProcessor : ContentProcessor<string, ScriptMetaData>
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>ScriptMetaData.</returns>
        public override ScriptMetaData Process(string input, ContentProcessorContext context)
        {
            return new ScriptMetaData
            {
                Id = Path.GetFileNameWithoutExtension(context.OutputFilename),
                Code = input
            };
        }
    }
}