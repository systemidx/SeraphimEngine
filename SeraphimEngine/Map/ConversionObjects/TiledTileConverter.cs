using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.TiledMap;
using SeraphimEngine.Managers.Asset;

namespace SeraphimEngine.Map.ConversionObjects
{
    /// <summary>
    /// Class TiledTileConverter.
    /// </summary>
    public class TiledTileConverter
    {
        /// <summary>
        /// Converts the specified tilesets.
        /// </summary>
        /// <param name="tilesets">The tilesets.</param>
        /// <param name="layerTile">The layer tile.</param>
        /// <param name="textures">The textures.</param>
        /// <param name="column">The column.</param>
        /// <param name="row">The row.</param>
        /// <returns>ISeraphimTile.</returns>
        /// <exception cref="System.Exception"></exception>
        public ISeraphimTile Convert(List<TiledMapTileset> tilesets, TiledMapLayerTile layerTile, Dictionary<string, Texture2D> textures, int column, int row)
        {
            //If it is a "null" tile, we want to return something that doesn't render.
            if (layerTile.Gid == 0)
                return new SeraphimTile(0, new List<ISeraphimTileFrame>());

            TiledMapTileset tileset = tilesets.Find(x => layerTile.Gid >= x.FirstGid && layerTile.Gid <= x.FirstGid + x.TileCount - 1);
            if (tileset == null)
                throw new Exception();

            //Get the cached texture
            Texture2D texture = textures[tileset.ImageSource];

            //Get the destination we are outputting to
            Rectangle destination = new Rectangle(tileset.TileWidth * column, tileset.TileHeight * row, tileset.TileWidth, tileset.TileHeight);

            //Init a tile
            ISeraphimTile tile = new SeraphimTile(0, new List<ISeraphimTileFrame>());
            tile.TileSize = new Point(tileset.TileWidth, tileset.TileHeight);

            //If we have a static tile
            if (tileset.Tiles.All(x => x.TileId != layerTile.Gid))
            {
                tile.Frames.Add(GetStaticTileFrame(layerTile.Gid, tileset, texture, destination));
                return tile;
            }

            //If we have an animated tile OR a custom tile
            TiledMapTilesetTile customTile = tileset.Tiles.First(x => x.TileId == layerTile.Gid);

            //Update Collision Matrix
            tile.CollisionDimensions = new Vector2(customTile.CollisionDimensions.X, customTile.CollisionDimensions.Y);
            tile.CollisionOrigin = new Vector2(customTile.CollisionOrigin.X, customTile.CollisionOrigin.Y);

            //Animated Tile
            if (customTile.Frames.Count > 0)
            {
                //Set the interval
                tile.AnimationInterval = System.Convert.ToInt32(customTile.Frames.Average(x => x.Duration));

                //And create the frames
                tile.Frames = customTile.Frames.Select(frame => GetAnimatedTileFrame(frame.TileId, tileset, texture, destination)).ToList();
            }

            return tile;
        }

        /// <summary>
        /// Gets the static tile frame.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="tileset">The tileset.</param>
        /// <param name="tilesetTexture">The tileset texture.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>ISeraphimTileFrame.</returns>
        private ISeraphimTileFrame GetStaticTileFrame(int id, TiledMapTileset tileset, Texture2D tilesetTexture, Rectangle destination)
        {
            id = id - tileset.FirstGid;

            int idx = id > tileset.TileCount ? id - tileset.TileCount - 1 : id;
            int tilesetColumn = (idx%tileset.Columns);
            int tilesetRow = idx / tileset.Columns;

            Vector2 sourcePosition = new Vector2(tilesetColumn * tileset.TileWidth, tilesetRow * tileset.TileHeight);
            Rectangle source = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, tileset.TileWidth, tileset.TileHeight);

            return new SeraphimTileFrame(tilesetTexture, source, destination);
        }

        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="tileset">The tileset.</param>
        /// <param name="tilesetTexture">The tileset texture.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>ISeraphimTileFrame.</returns>
        private ISeraphimTileFrame GetAnimatedTileFrame(int id, TiledMapTileset tileset, Texture2D tilesetTexture, Rectangle destination)
        {
            int idx = id > tileset.TileCount ? id - tileset.TileCount - 1 : id;
            int tilesetColumn = (idx%tileset.Columns);
            int tilesetRow = idx / tileset.Columns;
            
            Vector2 sourcePosition = new Vector2(tilesetColumn * tileset.TileWidth, tilesetRow * tileset.TileHeight);
            Rectangle source = new Rectangle((int)sourcePosition.X, (int)sourcePosition.Y, tileset.TileWidth, tileset.TileHeight);

            return new SeraphimTileFrame(tilesetTexture, source, destination);
        }
    }
}
