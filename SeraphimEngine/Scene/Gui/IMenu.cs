using MonoGame.Extended;

namespace SeraphimEngine.Scene.Gui
{
    public interface IMenu : IDraw, IUpdate
    {
        void Initialize(params MenuChoice[] choices);
    }
}