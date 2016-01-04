using System;
using Microsoft.Xna.Framework;
using SeraphimEngine.Scene;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Scene.Gui;

public class Scene_Test : SceneScript
{
    public Scene_Test()
    {
    }

    public override void Start(bool runOnce = false)
    {
        Console.WriteLine("Starting");
        SceneManager.Instance.CurrentScene.RegisterMenu(
            new Menu(
                "nested-test",
                new MenuPosition(Vector2.Zero),
                true,
                new MenuChoice("Start", "scene-start-menu-start"),
                new MenuChoice("Exit", "scene-start-menu-exit")
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