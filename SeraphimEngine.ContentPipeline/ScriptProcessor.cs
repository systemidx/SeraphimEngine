using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;
using SeraphimEngine.ContentPipeline.ContentObjects;

namespace SeraphimEngine.ContentPipeline
{
    /// <summary>
    /// Class ScriptProcessor.
    /// </summary>
    [ContentProcessor(DisplayName = "Seraphim Processor - Script")]
    public class ScriptProcessor : ContentProcessor<string, SeraphimScript>
    {
        /// <summary>
        /// Processes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="context">The context.</param>
        /// <returns>SeraphimScript.</returns>
        public override SeraphimScript Process(string input, ContentProcessorContext context)
        {
            return new SeraphimScript
            {
                Id = Path.GetFileNameWithoutExtension(context.OutputFilename),
                Code = input
            };
        }
    }
}