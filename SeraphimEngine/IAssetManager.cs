using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine {
    public interface IAssetManager : IManager {
        IDictionary<string, object> ObjectCache { get; }

        TAssetType GetAsset<TAssetType>(string assetId);
    }
}