using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.TiledMap;

namespace SeraphimEngine.Map.ConversionObjects
{
    /// <summary>
    /// Class TiledLayerConverter.
    /// </summary>
    public class TiledLayerConverter
    {
        /// <summary>
        /// Converts the specified tiled layer.
        /// </summary>
        /// <param name="tiledLayer">The tiled layer.</param>
        /// <param name="tilesets">The tilesets.</param>
        /// <returns>ISeraphimLayer.</returns>
        public ISeraphimLayer Convert(TiledMapLayer tiledLayer, List<TiledMapTileset> tilesets, Dictionary<string, Texture2D> textures)
        {
            return new SeraphimLayer(true, true, GetTiles(tiledLayer, tilesets, textures));
        }

        /// <summary>
        /// Gets the tiles.
        /// </summary>
        /// <param name="tiledlayer">The tiledlayer.</param>
        /// <param name="tilesets">The tilesets.</param>
        /// <param name="textures">The textures.</param>
        /// <returns>List&lt;ISeraphimTile&gt;.</returns>
        private List<ISeraphimTile> GetTiles(TiledMapLayer tiledlayer, List<TiledMapTileset> tilesets, Dictionary<string, Texture2D> textures)
        {
            List<ISeraphimTile> tiles = new List<ISeraphimTile>();

            TiledTileConverter tileConverter = new TiledTileConverter();

            for (int idx = 0; idx < tiledlayer.Width * tiledlayer.Height; ++idx)
            {
                int row = idx % tiledlayer.Width;
                int col = idx / tiledlayer.Width;

                tiles.Add(tileConverter.Convert(tilesets, tiledlayer.Tiles[idx], textures, row, col));
            }

            return tiles;
        }
    }
}
