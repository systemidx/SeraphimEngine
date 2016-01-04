using System;
using Microsoft.Xna.Framework;
using MonoGame.Framework;
using SeraphimEngine;
using SeraphimEngine.Scene;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.TestGame.Scenes;

public class Scene_Start_Menu_Start : SceneScript {
    public Scene_Start_Menu_Start()
    {
        
    }

    public override void Update(GameTime gameTime)
    {
        Console.WriteLine(SceneManager.Instance);
        SceneManager.Instance.SwitchScene(typeof(InitialScene));
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
    }
}