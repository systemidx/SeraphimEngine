using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using SeraphimEngine.Gui.Menu.Enumerations;
using SeraphimEngine.Helpers.SpriteBatch;
using SeraphimEngine.Input.Enumerations;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Gui;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Gui.Menu
{
    /// <summary>
    /// Class MenuGui.
    /// </summary>
    public class MenuGui : IMenuGui
    {
        private MenuContainer _container;

        private readonly MenuPosition _position;
        private readonly IList<MenuChoice> _choices = new List<MenuChoice>();
        private readonly IList<MenuChoiceMetaData> _metaData = new List<MenuChoiceMetaData>();
        private readonly bool _unloadMenuOnAction;

        private int _index;
        private Vector2 _containerBuffer;

        // ReSharper disable once InconsistentNaming
        private int _choiceCount => _choices.Count - 1;

        public string Id { get; }

        public bool IsVisible { get; set; } = true;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuGui" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="position">The position.</param>
        /// <param name="unloadMenuOnAction">if set to <c>true</c> [unload menu on action].</param>
        /// <param name="choices">The choices.</param>
        public MenuGui(string id, [NotNull]MenuPosition position, bool unloadMenuOnAction = false, params MenuChoice[] choices)
        {
            Id = id;

            foreach (MenuChoice choice in choices)
                _choices.Add(choice);

            _position = position;
            _unloadMenuOnAction = unloadMenuOnAction;
        }

        #endregion

        #region Life Cycle Events
        /// <summary>
        /// Initializes the specified choices.
        /// </summary>
        public void Initialize()
        {
            int maxMessageWidth = (int)_choices.Max(x => GuiManager.Instance.GuiFont.MeasureString(x.Text).X);
            int maxMessageHeight = (int)_choices.Sum(x => GuiManager.Instance.GuiFont.MeasureString(x.Text).Y);

            _container = new MenuContainer(_position, new Vector2(maxMessageWidth, maxMessageHeight));
            _containerBuffer = new Vector2(GuiManager.Instance.GuiContainerTexture.Width / 3, GuiManager.Instance.GuiContainerTexture.Height / 3);

            SetTextAndCursorPositions();
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            if (!IsVisible)
                return;

            _container.Draw(gameTime);

            for (int i = 0; i < _choices.Count; ++i)
            {
                if (_index == i)
                    GameManager.Instance.SpriteBatch.Draw(GuiManager.Instance.GuiCursorTexture, _metaData[i].CursorPosition, Color.White);

                GameManager.Instance.SpriteBatch.DrawShadowedText(_choices[i].Text, GuiManager.Instance.GuiFont, _metaData[i].TextPosition, Color.White);
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

            if (InputManager.Instance.IsActionDown(GameAction.Down))
            {
                GuiManager.Instance.PlaySoundEffect(GuiManager.Instance.GuiRolloverSoundInstance);
                ChangeIndex(MenuIndexAction.Increment);
            }

            if (InputManager.Instance.IsActionDown(GameAction.Up))
            {
                GuiManager.Instance.PlaySoundEffect(GuiManager.Instance.GuiRolloverSoundInstance);
                ChangeIndex(MenuIndexAction.Decrement);
            }

            if (InputManager.Instance.IsActionDown(GameAction.Accept))
            {
                GuiManager.Instance.PlaySoundEffect(GuiManager.Instance.GuiRolloverSelectInstance);
                Accept();
            }
        }

        #endregion

        /// <summary>
        /// Sets the text and cursor positions.
        /// </summary>
        private void SetTextAndCursorPositions()
        {
            for (int i = 0; i < _choices.Count; ++i)
            {
                MenuChoiceMetaData meta = new MenuChoiceMetaData();

                meta.TextSize = GuiManager.Instance.GuiFont.MeasureString(_choices[i].Text);
                meta.TextPosition = new Vector2(_position.Position.X + _containerBuffer.X, i * meta.TextSize.Y + _containerBuffer.Y + _position.Position.Y);
                meta.CursorPosition = new Vector2(_position.Position.X + _containerBuffer.X + meta.TextSize.X, (i * meta.TextSize.Y) + meta.TextSize.Y / 2 + _containerBuffer.Y + _position.Position.Y);

                _metaData.Add(meta);
            }
        }

        /// <summary>
        /// Accepts this instance.
        /// </summary>
        private void Accept()
        {
            _choices[_index].Accept();

            if (_unloadMenuOnAction)
                SceneManager.Instance.CurrentScene.UnloadMenu(this.Id);
        }

        /// <summary>
        /// Changes the index.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ChangeIndex(MenuIndexAction action)
        {
            _index = action == MenuIndexAction.Increment
                ? (_index == _choiceCount ? 0 : _index + 1)
                : (_index == 0 ? _choiceCount : _index - 1);
        }
    }
}
