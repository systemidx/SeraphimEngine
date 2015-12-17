using MonoGame.Extended;

namespace SeraphimEngine.Managers.Script {
    public interface IScriptManager : IManager, IUpdate, IDraw {
        void StartScript(string scriptId);
        void StopScript(string scriptId);
    }
}