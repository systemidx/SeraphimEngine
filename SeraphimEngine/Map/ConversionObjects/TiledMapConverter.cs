using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.TiledMap;
using SeraphimEngine.ContentPipeline.TiledMap.Serialization;
using SeraphimEngine.Managers.Asset;

namespace SeraphimEngine.Map.ConversionObjects
{
    /// <summary>
    /// Class TiledMapConverter.
    /// </summary>
    public class TiledMapConverter
    {
        /// <summary>
        /// Converts the specified tiled map.
        /// </summary>
        /// <param name="tiledMapXml">The tiled map XML.</param>
        /// <returns>ISeraphimMap.</returns>
        public ISeraphimMap Convert(string tiledMapXml)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(tiledMapXml);

            TiledMap tiledMap = new TiledMapDeserializer().Retrieve(document.DocumentElement);
            TiledLayerConverter converter = new TiledLayerConverter();

            Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
            foreach (TiledMapTileset tileset in tiledMap.Tilesets)
                textures.Add(tileset.ImageSource, AssetManager.Instance.GetAsset<Texture2D>("Maps", tileset.ImageSource));

            List<ISeraphimLayer> layers = new List<ISeraphimLayer>();
            for (int idx = 0; idx < tiledMap.Layers.Count; idx++)
                layers.Add(converter.Convert(tiledMap.Layers[idx], tiledMap.Tilesets, textures));
            
            return new SeraphimMap(layers, new Vector2(tiledMap.Width * tiledMap.TileWidth, tiledMap.Height * tiledMap.TileHeight));
        }
    }
}