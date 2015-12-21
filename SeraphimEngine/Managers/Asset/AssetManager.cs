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
    public class AssetManager : Manager<AssetManager>, IAssetManager
    {
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

        #region public Properties

        /// <summary>
        /// The in-memory cache for storing assets. These should not be added, removed, or cleared from the cache manually.
        /// </summary>
        /// <value>The object cache.</value>
        public IDictionary<string, object> ObjectCache { get; } = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public override bool IsInitialized { get; protected set; }

        #endregion

        #region Asset Methods

        /// <summary>
        /// Gets the asset from cache. If the asset is not in cache, it fetches it from the content handler.
        /// </summary>
        /// <typeparam name="TAssetType">The type of the t asset type.</typeparam>
        /// <param name="assetId">The asset identifier.</param>
        /// <returns>TAssetType.</returns>
        /// <exception cref="AssetManagerInitializationException"></exception>
        public TAssetType GetAsset<TAssetType>(string assetId)
        {
            if (!IsInitialized)
                throw new AssetManagerInitializationException();

            if (ObjectCache.ContainsKey(assetId))
                return (TAssetType) ObjectCache[assetId];

            TAssetType asset = _content.Load<TAssetType>(assetId);
            ObjectCache.Add(assetId, asset);

            return asset;
        }

        /// <summary>
        /// Unloads the asset.
        /// </summary>
        /// <param name="assetId">The asset identifier.</param>
        public void UnloadAsset(string assetId)
        {
            if (!IsInitialized)
                throw new AssetManagerInitializationException();

            if (ObjectCache.ContainsKey(assetId))
                ObjectCache.Remove(assetId);
        }

        #endregion

        #region Game Flow Methods

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            _content = content;
            _graphics = graphics;

            IsInitialized = true;
        }

        #endregion
    }
}