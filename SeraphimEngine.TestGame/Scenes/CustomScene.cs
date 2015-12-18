using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Managers.Script;

namespace SeraphimEngine.TestGame.Scenes
{
    public class CustomScene : Scene.Scene
    {
        private const string SCRIPT_NAME = "scene-test";

        public CustomScene(GraphicsDevice graphics, ViewportAdapter viewport) : base(graphics, viewport)
        {
        }

        public override void Load()
        {
            ScriptManager.Instance.StartScript(SCRIPT_NAME, ScriptType.Scene);
        }

        public override void Unload()
        {
            ScriptManager.Instance.StopScript(SCRIPT_NAME, ScriptType.Scene);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyDown(Keys.F12)) { 
                Unload();
                SceneManager.Instance.SwitchScene(typeof (SecondCustomScene));
            }
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}