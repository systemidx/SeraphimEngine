using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeraphimEngine.ContentPipeline.TiledMap
{
    public class TiledMapLayer
    {
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<TiledMapLayerTile> Tiles { get; set; } = new List<TiledMapLayerTile>();
    }
}
