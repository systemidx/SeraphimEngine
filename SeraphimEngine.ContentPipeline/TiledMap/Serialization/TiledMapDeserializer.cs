using System;
using System.Diagnostics;
using System.Xml;

namespace SeraphimEngine.ContentPipeline.TiledMap.Serialization
{
    public class TiledMapDeserializer : XmlDeserializer<TiledMap>
    {
        public override TiledMap Retrieve(XmlElement element)
        {
            TiledMap map = new TiledMap
            {
                Width = Convert.ToInt32(element.GetAttribute("width")),
                Height = Convert.ToInt32(element.GetAttribute("height")),
                TileWidth = Convert.ToInt32(element.GetAttribute("tilewidth")),
                TileHeight = Convert.ToInt32(element.GetAttribute("tileheight"))
            };

            XmlNodeList tilesetNodeList = element.SelectNodes("tileset");
            if (tilesetNodeList.Count > 0)
            {
                TiledMapTilesetDeserializer deserializer = new TiledMapTilesetDeserializer();
                foreach (XmlElement tileset in tilesetNodeList)
                    map.Tilesets.Add(deserializer.Retrieve(tileset));
            }

            XmlNodeList layerNodeList = element.SelectNodes("layer");
            if (layerNodeList.Count > 0)
            {
                TiledMapLayerDeserializer deserializer = new TiledMapLayerDeserializer();
                foreach (XmlElement layer in layerNodeList)
                    map.Layers.Add(deserializer.Retrieve(layer));
            }
            

            return map;
        }
    }
}
