using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Maps.Tiled;
using SeraphimEngine.Managers.Asset;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.TestGame.Scenes
{
    public class InitialSeraphimScene : Scene.SeraphimScene
    {
        public InitialSeraphimScene(GraphicsDevice graphics) : base(graphics) {}

        public override void Load()
        {
        }

        public override void Unload()
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
