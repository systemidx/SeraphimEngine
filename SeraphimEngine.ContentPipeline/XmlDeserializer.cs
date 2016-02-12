using System.Xml;

namespace SeraphimEngine.ContentPipeline
{
    public abstract class XmlDeserializer<TModel>
    {
        public abstract TModel Retrieve(XmlElement element);
    }
}
