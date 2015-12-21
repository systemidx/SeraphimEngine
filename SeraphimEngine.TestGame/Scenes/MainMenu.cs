using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Scene.Gui;

namespace SeraphimEngine.TestGame.Scenes
{
    public class MainMenu : Scene.Scene
    {
        private readonly Menu _menu = new Menu();
        private Texture2D _textureBackground;

        public MainMenu(GraphicsDevice graphics, ViewportAdapter viewport) : base(graphics, viewport)
        {

        }

        public override void Load()
        {
            _textureBackground = AssetManager.Instance.GetAsset<Texture2D>("textures/mainmenu_background");

            _menu.Initialize(new []
            {
                new MenuChoice("Start", "scene-main-menu-start"),
                new MenuChoice("Exit", "scene-main-menu-exit"),
            });
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            _menu.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //SpriteBatch.Begin(transformMatrix: SceneCamera.GetViewMatrix());
            GameManager.Instance.SpriteBatch.Draw(_textureBackground, Vector2.Zero, Color.White);
            _menu.Draw(gameTime);
            //SpriteBatch.End();
        }
    }
}