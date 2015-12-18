using System;
using SeraphimEngine.Scene;
using Microsoft.Xna.Framework;
using MonoGame.Framework;

public class Second_Scene_Test : SceneScript {
    private int myVar = 0;

    public Second_Scene_Test() {
        Console.WriteLine("new instance");
    }
    
    public override void Update(GameTime gameTime) {
        Console.WriteLine($"Second_Scene_Test Update");
    }

    public override void Draw(GameTime gameTime) {
        Console.WriteLine($"Second_Scene_Test Draw");
    }
}