using MonoGame.Extended;

namespace SeraphimEngine.Gui.Splash
{
    public interface ISplashScreen : IDraw, IUpdate
    {
        bool IsShowing { get; }

        void Show();
    }
}