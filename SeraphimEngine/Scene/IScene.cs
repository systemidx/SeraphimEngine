using System;
using MonoGame.Extended;

namespace SeraphimEngine.Scene {
    public interface IScene : IUpdate, IDraw {
        Camera2D SceneCamera { get; }

        void Load();
        void Unload();
    }
}