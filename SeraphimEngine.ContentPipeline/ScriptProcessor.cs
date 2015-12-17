using Microsoft.Xna.Framework.Content.Pipeline;
using SeraphimEngine.ContentPipeline.ContentObjects;

namespace SeraphimEngine.ContentPipeline {
    [ContentProcessor(DisplayName = "Seraphim Processor - Script")]
    public class ScriptProcessor : ContentProcessor<string, SeraphimScript> {
        public override SeraphimScript Process(string input, ContentProcessorContext context) {
            return new SeraphimScript { Code = input };
        }
    }
}