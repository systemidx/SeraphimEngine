using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.TiledMap;
using SeraphimEngine.Gui.Splash;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Scene;
using SeraphimEngine.Map;
using SeraphimEngine.Map.ConversionObjects;

namespace SeraphimEngine.TestGame.Scenes
{
    public class Splash : Scene.SeraphimScene
    {
        private SplashScreen _engine;
        private SplashScreen _studio;
        

        public Splash(GraphicsDevice graphics) : base(graphics) {}
        
        public override void Load()
        {
            _engine = new SplashScreen(() => _studio.Show(), "splash", "Music", "Textures/Scenes/Splash", "engine");
            _studio = new SplashScreen(() => SceneManager.Instance.SwitchScene(typeof(InitialSeraphimScene)), "splash", "Music", "Textures/Scenes/Splash", "studio");

            _engine.Show();
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _engine.Update(gameTime);
            _studio.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _engine.Draw(gameTime);
            _studio.Draw(gameTime);
        }
    }
}