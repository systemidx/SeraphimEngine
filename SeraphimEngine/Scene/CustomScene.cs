using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Script;

namespace SeraphimEngine.Scene
{
    public class CustomScene : Scene
    {
        public CustomScene(GraphicsDevice graphics, ViewportAdapter viewport) : base(graphics, viewport) {}

        public override void Load() {
            ScriptManager.Instance.StartScript("scripts/scene/scene-test");
        }

        public override void Unload() {
            ScriptManager.Instance.StopScript("scripts/scene/scene-test");
        }

        public override void Update(GameTime gameTime) {
            if (InputManager.Instance.IsKeyDown(Keys.F12))
               ScriptManager.Instance.StopScript($"scripts/scene/scene-test");
        }

        public override void Draw(GameTime gameTime) {
            
        }
    }
}
