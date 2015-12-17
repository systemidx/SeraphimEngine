using System;
using SeraphimEngine.Scene;
using Microsoft.Xna.Framework;
using MonoGame.Framework;

public class Scene_Test : SceneScript {
    private int myVar = 0;

    public Scene_Test() {
        Console.WriteLine("new instance");
    }
    
    public override void Update(GameTime gameTime) {
        Console.WriteLine($"Scene_Test Update");
    }

    public override void Draw(GameTime gameTime) {
        Console.WriteLine($"Scene_Test Draw");
    }
}