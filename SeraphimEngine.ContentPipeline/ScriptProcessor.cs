using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using SeraphimEngine.ContentPipeline.ContentObjects;

namespace SeraphimEngine.ContentPipeline
{
    /// <summary>
    /// Class ScriptProcessor.
    /// </summary>
    [ContentProcessor(DisplayName = "Seraphim Processor - Script")]
    public class ScriptProcessor : ContentProcessor<string, SeraphimScriptContent>
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>SeraphimScriptContent.</returns>
        public override SeraphimScriptContent Process(string input, ContentProcessorContext context)
        {
            return new SeraphimScriptContent
            {
                Id = Path.GetFileNameWithoutExtension(context.OutputFilename),
                Code = input
            };
        }
    }
}