using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.TiledMap;
using SeraphimEngine.Gui.Splash;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Map;
using SeraphimEngine.Map.ConversionObjects;

namespace SeraphimEngine.TestGame.Scenes
{
    public class Splash : Scene.SeraphimScene
    {
        private SplashScreen _engine;
        private SplashScreen _studio;

        public Splash(GraphicsDevice graphics) : base(graphics)
        {
        }

        private ISeraphimMap map;
        
        public override void Load()
        {
            TiledMap tiledMap = AssetManager.Instance.GetAsset<TiledMap>("Maps", "TestTiledMap");
            map = new TiledMapConverter().Convert(tiledMap);
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            map.Update(gameTime);
            map.MoveBy(new Vector2(1, 1));

            //SceneManager.Instance.Camera.MoveBy(new Vector2(1, 0));
            //_engine.Update(gameTime);
            //_studio.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            map.Draw(gameTime);
            //_engine.Draw(gameTime);
            //_studio.Draw(gameTime);
        }
    }
}