using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace SeraphimEngine {
    public interface ISceneManager : IManager, IUpdate, IDraw {
        ViewportAdapter ViewportAdapter { get; }
        IScene CurrentScene { get; }
        
        void SwitchScene(string sceneId);
    }
}