using System.Collections.Generic;

namespace SeraphimEngine.ContentPipeline.TiledMap
{
    public class TiledMap
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int TileWidth { get; set; }
        public int TileHeight { get; set; }

        public List<TiledMapTileset> Tilesets { get; set; } = new List<TiledMapTileset>();
        public List<TiledMapLayer> Layers { get; set; } = new List<TiledMapLayer>();
    }
}
