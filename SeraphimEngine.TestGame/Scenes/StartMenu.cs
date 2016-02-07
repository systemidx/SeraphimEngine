using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Gui;
using SeraphimEngine.Gui.Menu;
using SeraphimEngine.Gui.Menu.Enumerations;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Game;

namespace SeraphimEngine.TestGame.Scenes
{
    public class StartMenu : Scene.SeraphimScene
    {
        private Texture2D _textureBackground;

        public StartMenu(GraphicsDevice graphics) : base(graphics) { }

        public override void Load()
        {
            _textureBackground = AssetManager.Instance.GetAsset<Texture2D>("textures/scenes/StartMenu", "bg");

            RegisterMenu(new MenuGui("startMenu", 
                            new MenuPosition(MenuPositionHorizontal.Center, MenuPositionVertical.Center, true),
                            true,
                            new MenuChoice("Start the Game", typeof(Content.Scripts.Scene.Scene_Start_Menu_Start)),
                            new MenuChoice("Exit the Game", typeof(Content.Scripts.Scene.Scene_Start_Menu_Exit))));
        }

        public override void Draw(GameTime gameTime)
        {
            GameManager.Instance.SpriteBatch.Draw(_textureBackground, Vector2.Zero, Color.White);

            base.Draw(gameTime);
        }
    }
}