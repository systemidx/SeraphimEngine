using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.ContentPipeline.TiledMap.Serialization
{
    public class TiledMapLayerDeserializer : XmlDeserializer<TiledMapLayer>
    {
        public override TiledMapLayer Retrieve(XmlElement element)
        {
            TiledMapLayer layer = new TiledMapLayer
            {
                Name = element.GetAttribute("name"),
                Width = Convert.ToInt32(element.GetAttribute("width")),
                Height = Convert.ToInt32(element.GetAttribute("height"))
            };

            XmlNodeList dataNodeList = element.SelectNodes("data/tile");
            if (dataNodeList.Count > 0)
            {
                foreach (XmlElement dataElement in dataNodeList)
                {
                    layer.Tiles.Add(new TiledMapLayerTile
                    {
                        Gid = Convert.ToInt32(dataElement.GetAttribute("gid"))
                    });
                }  
            }
            return layer;
        }
    }
}
