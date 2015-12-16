using MonoGame.Extended;

namespace SeraphimEngine {
    public interface IScene : IUpdate, IDraw {
        Camera2D SceneCamera { get; }
        void Enter();
        void Exit();
    }
}