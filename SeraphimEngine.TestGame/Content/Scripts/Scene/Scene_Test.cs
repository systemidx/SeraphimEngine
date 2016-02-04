using System;
using Microsoft.Xna.Framework;
using SeraphimEngine.Gui.Menu;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Scene;
using SeraphimEngine.Script;
using SeraphimEngine.TestGame.Scenes;

namespace SeraphimEngine.TestGame.Content.Scripts.Scene
{
    [Serializable]
    public class Scene_Test : SeraphimScript
    {
        public override void Start(bool runOnce = false)
        {
            SceneManager.Instance.CurrentScene.RegisterMenu(
                new MenuGui(
                    "nested-test",
                    new MenuPosition(Vector2.Zero),
                    true,
                    new MenuChoice("Start", () => SceneManager.Instance.SwitchScene(typeof (Splash))),
                    new MenuChoice("Exit", () => GameManager.Instance.Exit())
                    )
                );

            base.Start(runOnce);
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}