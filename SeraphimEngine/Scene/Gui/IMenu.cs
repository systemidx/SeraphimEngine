using MonoGame.Extended;

namespace SeraphimEngine.Scene.Gui
{
    public interface IMenu : IDraw, IUpdate
    {
        string Id { get; }
        bool IsVisible { get; set; }
        void Initialize();
    }
}