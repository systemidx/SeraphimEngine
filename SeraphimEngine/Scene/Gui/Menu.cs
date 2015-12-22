using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Input;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Script;

namespace SeraphimEngine.Scene.Gui
{
    public class Menu : IMenu
    {
        private SpriteFont _font;
        private Texture2D _cursor;

        private readonly IList<MenuChoice> _choices = new List<MenuChoice>();
        private int _maxIndex;
        private int _index = 0;

        public Menu()
        {
            
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < _choices.Count; ++i)
            {
                Vector2 position = new Vector2(0, i * _font.MeasureString(_choices[i].Text).Y);

                GameManager.Instance.SpriteBatch.DrawString(_font, _choices[i].Text, position, Color.White);
            }
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsActionDown(InputAction.Down))
            {
                _index = _index == _maxIndex ? 0 : _index + 1;
            }

            if (InputManager.Instance.IsActionDown(InputAction.Up))
            {
                _index = _index == 0 ? _maxIndex : _index - 1;
            }

            if (InputManager.Instance.IsActionDown(InputAction.Accept))
            {
                ScriptManager.Instance.StartScript(_choices[1].ScriptName, ScriptType.Scene, true);
            }
        }

        public void Initialize(params MenuChoice[] choices)
        {
            _font = AssetManager.Instance.GetAsset<SpriteFont>("fonts/default");
            _cursor = AssetManager.Instance.GetAsset<Texture2D>("textures/cursor");
            
            foreach (MenuChoice choice in choices)
                _choices.Add(choice);

            _maxIndex = _choices.Count - 1;
        }
    }
}
