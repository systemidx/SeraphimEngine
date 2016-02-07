using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.ContentPipeline.TiledMap
{
    public class TiledMapTilesetTile
    {
        public int TileId { get; set; }

        public Vector2 CollisionOrigin { get; set; }
        public Vector2 CollisionDimensions { get; set; }

        public List<TiledMapTilesetTileFrame> Frames { get; set; } = new List<TiledMapTilesetTileFrame>();
    }
}
