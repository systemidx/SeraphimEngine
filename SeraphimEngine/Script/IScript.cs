using MonoGame.Extended;

namespace SeraphimEngine.Script {
    public interface IScript : IUpdate, IDraw{
        bool IsRunning { get; }
        void Start();
        void Stop();
    }
}