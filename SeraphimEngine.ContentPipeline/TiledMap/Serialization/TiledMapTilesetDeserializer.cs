using System;
using System.Diagnostics;
using System.Xml;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.ContentPipeline.TiledMap.Serialization
{
    public class TiledMapTilesetDeserializer : XmlDeserializer<TiledMapTileset>
    {
        public override TiledMapTileset Retrieve(XmlElement element)
        {
            TiledMapTileset tileset = new TiledMapTileset
            {
                FirstGid = Convert.ToInt32(element.GetAttribute("firstgid")),
                Name = element.GetAttribute("name"),
                TileWidth = Convert.ToInt32(element.GetAttribute("tilewidth")),
                TileHeight = Convert.ToInt32(element.GetAttribute("tileheight")),
                TileCount = Convert.ToInt32(element.GetAttribute("tilecount")),
                Columns = Convert.ToInt32(element.GetAttribute("columns")),
                ImageSource = element["image"].GetAttribute("source"),
                ImageSourceHeight = Convert.ToInt32(element["image"].GetAttribute("height")),
                ImageSourceWidth = Convert.ToInt32(element["image"].GetAttribute("width"))
            };
            
            XmlNodeList tilesetTileNodeList = element.SelectNodes("tile");
            if (tilesetTileNodeList.Count > 0)
            {
                foreach (XmlElement tilesetTileElement in tilesetTileNodeList)
                {
                    TiledMapTilesetTile tile = new TiledMapTilesetTile
                    {
                        TileId = Convert.ToInt32(tilesetTileElement.GetAttribute("id")) + 1
                    };
                    
                    XmlNodeList objectGroupNodeList = tilesetTileElement.SelectNodes("objectgroup");
                    if (objectGroupNodeList.Count > 0)
                    {
                        float.TryParse(objectGroupNodeList[0]["object"].GetAttribute("x"), out tile.CollisionOriginX);
                        float.TryParse(objectGroupNodeList[0]["object"].GetAttribute("y"), out tile.CollisionOriginY);
                        float.TryParse(objectGroupNodeList[0]["object"].GetAttribute("width"), out tile.CollisionDimensionsX);
                        float.TryParse(objectGroupNodeList[0]["object"].GetAttribute("height"), out tile.CollisionDimensionsY);
                    }

                    //Get the animation frames
                    XmlNodeList tileFrameNodeList = tilesetTileElement.SelectNodes("animation/frame");
                    if (tileFrameNodeList.Count > 0)
                    { 
                        foreach (XmlElement frame in tileFrameNodeList)
                        {
                            tile.Frames.Add(new TiledMapTilesetTileFrame
                            {
                                TileId = Convert.ToInt32(frame.GetAttribute("tileid")),
                                Duration = Convert.ToInt32(frame.GetAttribute("duration"))
                            });
                        }
                    }

                    tileset.Tiles.Add(tile);
                }
            }

            return tileset;
        }
    }
}
