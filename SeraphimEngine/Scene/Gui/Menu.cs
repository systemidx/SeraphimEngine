using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private SoundEffect _cursorRollover;
        private SoundEffect _cursorSelect;
        private SoundEffectInstance _cursorRolloverInstance;
        private SoundEffectInstance _cursorSelectInstance;

        private readonly IList<MenuChoice> _choices = new List<MenuChoice>();
        private int _maxIndex;
        private int _index = 0;
        
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < _choices.Count; ++i)
            {
                Vector2 textSize = _font.MeasureString(_choices[i].Text);
                Vector2 textPosition = new Vector2(0, i * textSize.Y);
                Vector2 cursorPosition = new Vector2(textSize.X, (i * textSize.Y) + textSize.Y / 2);
                
                if (_index == i)
                    GameManager.Instance.SpriteBatch.Draw(_cursor, cursorPosition, Color.White);

                GameManager.Instance.SpriteBatch.DrawString(_font, _choices[i].Text, textPosition, Color.White);
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
                _cursorRolloverInstance.Stop();
                _cursorRolloverInstance.Play();
                _index = _index == _maxIndex ? 0 : _index + 1;
            }

            if (InputManager.Instance.IsActionDown(InputAction.Up))
            {
                _cursorRolloverInstance.Stop();
                _cursorRolloverInstance.Play();

                _index = _index == 0 ? _maxIndex : _index - 1;
            }

            if (InputManager.Instance.IsActionDown(InputAction.Accept))
            {
                _cursorSelectInstance.Stop();
                _cursorSelectInstance.Play();

                ScriptManager.Instance.StartScript(_choices[_index].ScriptName, ScriptType.Scene, true);
            }
        }

        public void Initialize(params MenuChoice[] choices)
        {
            _font = AssetManager.Instance.GetAsset<SpriteFont>("fonts/default");
            _cursor = AssetManager.Instance.GetAsset<Texture2D>("textures/cursor");

            _cursorRollover = AssetManager.Instance.GetAsset<SoundEffect>("sounds/rollover");
            _cursorSelect = AssetManager.Instance.GetAsset<SoundEffect>("sounds/select");

            _cursorRolloverInstance = _cursorRollover.CreateInstance();
            _cursorSelectInstance = _cursorSelect.CreateInstance();

            foreach (MenuChoice choice in choices)
                _choices.Add(choice);

            _maxIndex = _choices.Count - 1;
        }
    }
}
