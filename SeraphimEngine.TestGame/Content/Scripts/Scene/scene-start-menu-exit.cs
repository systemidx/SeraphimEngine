using System;
using Microsoft.Xna.Framework;
using MonoGame.Framework;
using SeraphimEngine;
using SeraphimEngine.Scene;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.TestGame.Scenes;

public class Scene_Start_Menu_Exit : SceneScript {
    public override void Update(GameTime gameTime)
    {
        GameManager.Instance.Exit();
    }

    public override void Draw(GameTime gameTime)
    {
    }
}