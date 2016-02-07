using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.TiledMap;
using SeraphimEngine.Managers.Asset;

namespace SeraphimEngine.Map.ConversionObjects
{
    public class TiledMapConverter
    {
        public ISeraphimMap Convert(TiledMap tiledMap)
        {
            TiledLayerConverter converter = new TiledLayerConverter();

            List<ISeraphimLayer> layers = new List<ISeraphimLayer>();
            for (int idx = 0; idx < tiledMap.Layers.Count; idx++)
                layers.Add(converter.Convert(tiledMap.Layers[idx], tiledMap.Tilesets));
            
            return new SeraphimMap(layers, new Vector2(tiledMap.Width * tiledMap.TileWidth, tiledMap.Height * tiledMap.TileHeight));
        }
    }

    public class TiledLayerConverter
    {
        public ISeraphimLayer Convert(TiledMapLayer tiledLayer, List<TiledMapTileset> tilesets)
        {
            return new SeraphimLayer(true, true, GetTiles(tiledLayer, tilesets));
        }

        private List<ISeraphimTile> GetTiles(TiledMapLayer tiledlayer, List<TiledMapTileset> tilesets)
        {
            List<ISeraphimTile> tiles = new List<ISeraphimTile>();

            TiledTileConverter tileConverter = new TiledTileConverter();
            for (int idx = 0; idx < tiledlayer.Width * tiledlayer.Height; ++idx)
            {
                int row = idx % tiledlayer.Width;
                int col = idx / tiledlayer.Width;

                tiles.Add(tileConverter.Convert(tilesets, tiledlayer.Tiles[idx], row, col));
            }

            return tiles;
        }
    }

    public class TiledTileConverter
    {
        /// <summary>
        /// Converts the specified tilesets.
        /// </summary>
        /// <param name="tilesets">The tilesets.</param>
        /// <param name="layerTile">The layer tile.</param>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        /// <returns>ISeraphimTile.</returns>
        /// <exception cref="System.Exception"></exception>
        public ISeraphimTile Convert(List<TiledMapTileset> tilesets, TiledMapLayerTile layerTile, int column, int row)
        {
            //If it is a "null" tile, we want to return something that doesn't render.
            if (layerTile.Gid == 0)
                return new SeraphimTile(0, new List<ISeraphimTileFrame>());

            TiledMapTileset tileset = tilesets.Find(x => layerTile.Gid >= x.FirstGid && layerTile.Gid <= x.FirstGid + x.TileCount - 1);
            if (tileset == null)
                throw new Exception();

            //Get the cached texture
            Texture2D texture = AssetManager.Instance.GetAsset<Texture2D>("Maps", tileset.ImageSource);

            //Get the destination we are outputting to
            Rectangle destination = new Rectangle(tileset.TileWidth * column, tileset.TileHeight * row, tileset.TileWidth, tileset.TileHeight);

            //Init a tile
            ISeraphimTile tile = new SeraphimTile(0, new List<ISeraphimTileFrame>());
            tile.TileSize = new Point(tileset.TileWidth, tileset.TileHeight);

            //If we have a static tile
            if (tileset.Tiles.All(x => x.TileId != layerTile.Gid)) { 
                tile.Frames.Add(GetTileFrame(layerTile.Gid, tileset, texture, destination));
                return tile;
            }

            //If we have an animated tile OR a custom tile
            TiledMapTilesetTile customTile = tileset.Tiles.First(x => x.TileId == layerTile.Gid);

            //Update Collision Matrix
            tile.CollisionDimensions = customTile.CollisionDimensions;
            tile.CollisionOrigin = customTile.CollisionOrigin;
            
            //Animated Tile
            if (customTile.Frames.Count > 0)
            {
                //Set the interval
                tile.AnimationInterval = System.Convert.ToInt32(customTile.Frames.Average(x => x.Duration));

                //And create the frames
                tile.Frames = customTile.Frames.Select(frame => GetTileFrame(frame.TileId, tileset, texture, destination)).ToList();
            }

            return tile;
        }

        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="tileset">The tileset.</param>
        /// <param name="tilesetTexture">The tileset texture.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>ISeraphimTileFrame.</returns>
        private ISeraphimTileFrame GetTileFrame(int id, TiledMapTileset tileset, Texture2D tilesetTexture, Rectangle destination)
        {
            int idx = id > tileset.TileCount  ? id - tileset.TileCount - 1 : id;
            int tilesetColumn = idx % tileset.Columns;
            int tilesetRow = idx / tileset.Columns;

            Vector2 sourcePosition = new Vector2(tilesetColumn * tileset.TileWidth, tilesetRow * tileset.TileHeight);
            Rectangle source = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, tileset.TileWidth, tileset.TileHeight);

            return new SeraphimTileFrame(tilesetTexture, source, destination);
        }
    }
}