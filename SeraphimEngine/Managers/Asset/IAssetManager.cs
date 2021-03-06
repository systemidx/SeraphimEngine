﻿using System.Collections.Generic;

namespace SeraphimEngine.Managers.Asset
{
    /// <summary>
    /// Interface IAssetManager
    /// </summary>
    public interface IAssetManager : IManager
    {
        /// <summary>
        /// Gets the object cache.
        /// </summary>
        /// <value>The object cache.</value>
        IDictionary<string, object> ObjectCache { get; }

        /// <summary>
        /// Gets the asset.
        /// </summary>
        /// <typeparam name="TAssetType">The type of the t asset type.</typeparam>
        /// <param name="assetDirectory">The asset directory.</param>
        /// <param name="assetId">The asset identifier.</param>
        /// <returns>TAssetType.</returns>
        TAssetType GetAsset<TAssetType>(string assetDirectory, string assetId);

        /// <summary>
        /// Gets all assets.
        /// </summary>
        /// <typeparam name="TAssetType">The type of the t asset type.</typeparam>
        /// <param name="assetDirectory">The asset directory.</param>
        /// <returns>TAssetType[].</returns>
        TAssetType[] GetAllAssets<TAssetType>(string assetDirectory);

        /// <summary>
        /// Unloads the asset.
        /// </summary>
        /// <param name="assetId">The asset identifier.</param>
        void UnloadAsset(string assetId);
    }
}