using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        /// <param name="assetDirectory">The asset directory.</param>
        /// <param name="assetId">The asset identifier.</param>
        /// <returns>TAssetType.</returns>
        /// <exception cref="AssetManagerInitializationException"></exception>
        public TAssetType GetAsset<TAssetType>(string assetDirectory, string assetId)
        {
            if (!IsInitialized)
                throw new AssetManagerInitializationException();

            if (assetDirectory != null)
            { 
                if (assetDirectory.StartsWith("Content"))
                    assetDirectory = assetDirectory.Replace("Content", "");

                if (assetDirectory.StartsWith("/"))
                    assetDirectory = assetDirectory.Remove(0, 1);

                if (!assetDirectory.EndsWith("/"))
                    assetDirectory += "/";
            }

            string fullAssetPath = assetDirectory + assetId;

            if (ObjectCache.ContainsKey(assetId))
                return (TAssetType)ObjectCache[assetId];
            
            TAssetType asset = _content.Load<TAssetType>(fullAssetPath);

            ObjectCache.Add(assetId, asset);

            return asset;
        }

        /// <summary>
        /// Gets all assets.
        /// </summary>
        /// <typeparam name="TAssetType">The type of the t asset type.</typeparam>
        /// <param name="assetDirectory">The asset directory.</param>
        /// <returns>TAssetType[].</returns>
        /// <exception cref="AssetManagerInitializationException"></exception>
        public TAssetType[] GetAllAssets<TAssetType>(string assetDirectory)
        {
            if (!IsInitialized)
                throw new AssetManagerInitializationException();

            DirectoryInfo contentDirectory = new DirectoryInfo(assetDirectory);
            FileInfo[] files = contentDirectory.GetFiles("*.*", SearchOption.AllDirectories);

            ConcurrentBag<TAssetType> assets = new ConcurrentBag<TAssetType>();
            Parallel.ForEach(files, f =>
            {
                assets.Add(GetAsset<TAssetType>(assetDirectory + MakeRelativePath(contentDirectory.FullName, Path.GetDirectoryName(f.FullName)),
                Path.GetFileNameWithoutExtension(f.Name)));
            });

            return assets.ToArray();
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

        #region Private Methods

        /// <summary>
        /// Makes the relative path.
        /// </summary>
        /// <param name="fromPath">From path.</param>
        /// <param name="toPath">To path.</param>
        /// <returns>System.String.</returns>
        private string MakeRelativePath(string fromPath, string toPath)
        {
            if (string.IsNullOrEmpty(fromPath))
                throw new ArgumentNullException(nameof(fromPath));

            if (string.IsNullOrEmpty(toPath))
                throw new ArgumentNullException(nameof(toPath));

            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme)
                return toPath;

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.ToUpperInvariant() == "FILE")
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            return relativePath;
        }

        #endregion
    }
}