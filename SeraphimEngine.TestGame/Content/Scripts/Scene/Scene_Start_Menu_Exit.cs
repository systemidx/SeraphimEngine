using System;
using Microsoft.Xna.Framework;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Scene;
using SeraphimEngine.Script;

namespace SeraphimEngine.TestGame.Content.Scripts.Scene
{
    [Serializable]
    public class Scene_Start_Menu_Exit : SeraphimScript
    {
        public override void Update(GameTime gameTime)
        {
            GameManager.Instance.Exit();
        }

        public override void Draw(GameTime gameTime)
        {
        }
    }
}