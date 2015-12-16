using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SeraphimEngine {
    public interface IManager {
        bool IsInitialized { get; }
        void Initialize(ContentManager content, GraphicsDevice graphics);
    }
}