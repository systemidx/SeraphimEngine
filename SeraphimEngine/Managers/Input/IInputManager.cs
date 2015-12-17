using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;

namespace SeraphimEngine.Managers.Input
{
    public interface IInputManager : IUpdate {
        bool IsKeyDown(Keys key);
    }
}
