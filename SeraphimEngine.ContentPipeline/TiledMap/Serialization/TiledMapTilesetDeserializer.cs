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
                        tile.CollisionOrigin = new Vector2(
                            float.Parse(objectGroupNodeList[0]["object"].GetAttribute("x")), 
                            float.Parse(objectGroupNodeList[0]["object"].GetAttribute("y")));

                        tile.CollisionDimensions = new Vector2(
                            float.Parse(objectGroupNodeList[0]["object"].GetAttribute("width")),
                            float.Parse(objectGroupNodeList[0]["object"].GetAttribute("height")));
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
