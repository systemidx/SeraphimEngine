using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Scene.Gui;

namespace SeraphimEngine.TestGame.Scenes
{
    public class StartMenu : Scene.Scene
    {
        private Texture2D _textureBackground;

        public StartMenu(GraphicsDevice graphics, ViewportAdapter viewport) : base(graphics, viewport)
        {

        }

        public override void Load()
        {
            _textureBackground = AssetManager.Instance.GetAsset<Texture2D>("textures/scenes/StartMenu/bg");

            RegisterMenu(new Menu("start", 
                            new MenuPosition(MenuPositionHorizontal.Center, MenuPositionVertical.Center, true),
                            true,
                            new MenuChoice("Start the Game", "scene-start-menu-start"),
                            new MenuChoice("Exit the Game", "scene-start-menu-exit")));
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameManager.Instance.SpriteBatch.Draw(_textureBackground, Vector2.Zero, Color.White);
            base.Update(gameTime);
        }
    }
}