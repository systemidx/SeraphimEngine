using System;
using Microsoft.Xna.Framework;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Scene;
using SeraphimEngine.Script;
using SeraphimEngine.TestGame.Scenes;

namespace SeraphimEngine.TestGame.Content.Scripts.Scene
{
    [Serializable]
    public class Scene_Start_Menu_Start : SeraphimScript
    {
        public override void Update(GameTime gameTime)
        {
            Console.WriteLine(SceneManager.Instance);
            SceneManager.Instance.SwitchScene(typeof (InitialSeraphimScene));
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}