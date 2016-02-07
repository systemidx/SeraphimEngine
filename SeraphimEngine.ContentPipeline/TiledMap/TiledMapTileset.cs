using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.ContentPipeline.TiledMap
{
    public class TiledMapTileset
    {
        public int FirstGid { get; set; }
        public string Name { get; set; }
        public int TileWidth { get; set; }
        public int TileHeight { get; set; }
        public int TileCount { get; set; }
        public int Columns { get; set; }

        public string ImageSource { get; set; }
        public int ImageSourceWidth { get; set; }
        public int ImageSourceHeight { get; set; }

        public List<TiledMapTilesetTile>  Tiles { get; set; } = new List<TiledMapTilesetTile>();
    }
}
