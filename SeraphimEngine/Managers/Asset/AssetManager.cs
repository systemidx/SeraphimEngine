using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Exceptions;

namespace SeraphimEngine.Managers.Asset
{
    /// <summary>
    /// Class AssetManager.
    /// </summary>
    public class AssetManager : IAssetManager {

        #region Private Members

        /// <summary>
        /// The content manager
        /// </summary>
        private ContentManager _content;

        /// <summary>
        /// The graphics device
        /// </summary>
        private GraphicsDevice _graphics;

        #endregion

        public IDictionary<string, object> ObjectCache { get; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        public TAssetType GetAsset<TAssetType>(string assetId) {
            if (!IsInitialized)
                throw new AssetManagerInitializationException();

            if (ObjectCache.ContainsKey(assetId))
                return (TAssetType)ObjectCache[assetId];

            TAssetType asset = _content.Load<TAssetType>(assetId);
            ObjectCache.Add(assetId, asset);

            return asset;
        }

        public static IAssetManager Instance = new Lazy<IAssetManager>(() => new AssetManager()).Value;


        private AssetManager() { }
        public bool IsInitialized { get; private set; } = false;

        public void Initialize(ContentManager content, GraphicsDevice graphics) {
            _content = content;
            _graphics = graphics;

            IsInitialized = true;
        }
    }
}
