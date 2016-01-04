using System;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended.Maps.Tiled;
using SeraphimEngine.Managers.Asset;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.TestGame.Scenes
{
    public class InitialScene : Scene.Scene
    {
        private TiledMap map;

        public InitialScene(GraphicsDevice graphics, ViewportAdapter viewport) : base(graphics, viewport) {}

        public override void Load()
        {
            map = AssetManager.Instance.GetAsset<TiledMap>("Maps/InitialScene");
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
            map.Draw(this.SceneCamera);

            base.Draw(gameTime);
        }
    }
}
