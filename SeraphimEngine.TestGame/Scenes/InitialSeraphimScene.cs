using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Managers.Asset;
using Microsoft.Xna.Framework;
using SeraphimEngine.ContentPipeline.Menu;
using SeraphimEngine.ContentPipeline.Message;
using SeraphimEngine.Gui.Menu;
using SeraphimEngine.Gui.Menu.ConversionObjects;
using SeraphimEngine.Gui.MessageBox;
using SeraphimEngine.Gui.MessageBox.ConversionObjects;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Map;
using SeraphimEngine.Map.ConversionObjects;

namespace SeraphimEngine.TestGame.Scenes
{
    public class InitialSeraphimScene : Scene.SeraphimScene
    {
        private ISeraphimMap _map;

        private IMenuGui _menu;
        private IMessageBoxGui _messageBox;


        public InitialSeraphimScene(GraphicsDevice graphics) : base(graphics) {}

        public override void Load()
        {
            GameManager.Instance.SetGameVariable("gold", 1000);
            //_messageBox = new MessageBoxGui("Some text", new Rectangle(0, 0, 120, 32), null);
            _messageBox =
                new MessageGuiConverter().Convert(AssetManager.Instance.GetAsset<MessageData>("Message", "TestMessage"));
            _map = new TiledMapConverter().Convert(AssetManager.Instance.GetAsset<string>("Maps", "Test2"));

            _menu = new MenuConverter().Convert(AssetManager.Instance.GetAsset<MenuData>("Menu", "StartMenu"));
            _menu.Initialize();
        }

        public override void Unload() {}

        public override void Update(GameTime gameTime)
        {
            _map.Update(gameTime);
            _map.MoveBy(new Vector2(0.1f, 0));

            _menu.Update(gameTime);
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _map.Draw(gameTime);
            _messageBox.Draw(gameTime);
            _menu.Draw(gameTime);
            base.Draw(gameTime);
        }
    }
}
