using MonoGame.Extended;

namespace SeraphimEngine.Gui
{
    public interface IMessageGui : IDraw, IUpdate
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize();
    }
}