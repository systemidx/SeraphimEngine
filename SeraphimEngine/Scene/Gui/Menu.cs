using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Input;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Script;
using SeraphimEngine.Helpers.SpriteBatch;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Scene.Gui
{
    public class Menu : IMenu
    {
        private SpriteFont _font;

        private Texture2D _cursor;
        private Texture2D _containerTexture;

        private SoundEffect _cursorRollover;
        private SoundEffect _cursorSelect;
        private SoundEffectInstance _cursorRolloverInstance;
        private SoundEffectInstance _cursorSelectInstance;

        private MenuContainer _container;
        private readonly MenuPosition _position;

        private readonly IList<MenuChoice> _choices = new List<MenuChoice>();
        private int _maxIndex;
        private int _index = 0;

        private readonly bool _unloadMenuOnAction = false;

        public string Id { get; private set; }
        public bool IsVisible { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="position">The position.</param>
        /// <param name="unloadMenuOnAction">if set to <c>true</c> [unload menu on action].</param>
        /// <param name="choices">The choices.</param>
        public Menu(string id, [NotNull]MenuPosition position, bool unloadMenuOnAction = false, params MenuChoice[] choices)
        {
            Id = id;
            _position = position;
            _unloadMenuOnAction = unloadMenuOnAction;

            foreach (MenuChoice choice in choices)
                _choices.Add(choice);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            if (!IsVisible)
                return;

            int bufferX = _containerTexture.Width / 3;
            int bufferY = _containerTexture.Width / 3;

            _container.Draw(gameTime);
            for (int i = 0; i < _choices.Count; ++i)
            {
                Vector2 textSize = _font.MeasureString(_choices[i].Text);
                Vector2 textPosition = new Vector2(_position.Position.X + bufferX, i * textSize.Y + bufferY + _position.Position.Y);
                Vector2 cursorPosition = new Vector2(_position.Position.X + bufferX + textSize.X, (i * textSize.Y) + textSize.Y / 2 + bufferY + _position.Position.Y);

                if (_index == i)
                    GameManager.Instance.SpriteBatch.Draw(_cursor, cursorPosition, Color.White);

                GameManager.Instance.SpriteBatch.DrawShadowedText(_choices[i].Text, _font, textPosition, Color.White);
            }
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (!IsVisible)
                return;

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

                if (_unloadMenuOnAction)
                    SceneManager.Instance.CurrentScene.UnloadMenu(this.Id);
            }
        }

        /// <summary>
        /// Initializes the specified choices.
        /// </summary>
        public void Initialize()
        {
            _font = AssetManager.Instance.GetAsset<SpriteFont>("fonts/default");

            _containerTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/menu/menu");
            _cursor = AssetManager.Instance.GetAsset<Texture2D>("textures/menu/cursor");

            _cursorRollover = AssetManager.Instance.GetAsset<SoundEffect>("sounds/rollover");
            _cursorSelect = AssetManager.Instance.GetAsset<SoundEffect>("sounds/select");

            _cursorRolloverInstance = _cursorRollover.CreateInstance();
            _cursorSelectInstance = _cursorSelect.CreateInstance();
            

            int maxMessageWidth = (int)_choices.Max(x => _font.MeasureString(x.Text).X);
            int maxMessageHeight = (int)_choices.Sum(x => _font.MeasureString(x.Text).Y);
            
            _container = new MenuContainer(_position, new Vector2(maxMessageWidth, maxMessageHeight), _containerTexture);

            _maxIndex = _choices.Count - 1;
        }
    }
}
