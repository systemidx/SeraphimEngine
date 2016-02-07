using System.Xml;

namespace SeraphimEngine.ContentPipeline.TiledMap.Serialization
{
    public abstract class XmlDeserializer<TModel>
    {
        public abstract TModel Retrieve(XmlElement element);
    }
}
