using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.ContentPipeline.TiledMap
{
    public class TiledMapTilesetTile
    {
        public int TileId { get; set; }

        public float CollisionOriginX;
        public float CollisionOriginY;

        public float CollisionDimensionsX;
        public float CollisionDimensionsY;

        public Vector2 CollisionOrigin => new Vector2(CollisionOriginX, CollisionOriginY);
        public Vector2 CollisionDimensions => new Vector2(CollisionDimensionsX, CollisionDimensionsY);

        public List<TiledMapTilesetTileFrame> Frames { get; set; } = new List<TiledMapTilesetTileFrame>();
    }
}
