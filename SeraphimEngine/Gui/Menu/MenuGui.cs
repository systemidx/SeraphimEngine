using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using MonoGame.Extended.BitmapFonts;
using SeraphimEngine.Gui.Menu.Enumerations;
using SeraphimEngine.Helpers.Asset;
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
        #region Variables

        /// <summary>
        /// The menu container
        /// </summary>
        private MenuContainer _container;

        /// <summary>
        /// The guiPosition
        /// </summary>
        private Vector2 _containerPosition;

        /// <summary>
        /// The choices
        /// </summary>
        private readonly IList<MenuChoice> _choices = new List<MenuChoice>();

        /// <summary>
        /// The meta data
        /// </summary>
        private readonly IList<MenuChoiceMetaData> _metaData = new List<MenuChoiceMetaData>();

        /// <summary>
        /// The flag which determines to close the menu on an action event
        /// </summary>
        private readonly bool _unloadMenuOnAction;

        /// <summary>
        /// The choice index
        /// </summary>
        private int _choiceIndex;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visible.
        /// </summary>
        /// <value><c>true</c> if this instance is visible; otherwise, <c>false</c>.</value>
        public bool IsVisible { get; set; } = true;

        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Gets or sets the choice count.
        /// </summary>
        /// <value>The _choice count.</value>
        private int _choiceCount => _choices.Count - 1;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuGui" /> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="position">The position.</param>
        /// <param name="center">The center.</param>
        /// <param name="unloadMenuOnAction">The unload menu on action.</param>
        /// <param name="choices">The choices.</param>
        public MenuGui(string id, Vector2 position, bool center = false, bool unloadMenuOnAction = false, params MenuChoice[] choices)
        {
            Id = id;

            foreach (MenuChoice choice in choices)
                _choices.Add(choice);

            _containerPosition = center ? SceneManager.Instance.Camera.GetCenterCoordinates() : position;
            _unloadMenuOnAction = unloadMenuOnAction;
        }

        #endregion

        #region Life Cycle Events
        /// <summary>
        /// Initializes the specified choices.
        /// </summary>
        public void Initialize()
        {
            float maxMessageWidth = _choices.Max(x => GuiManager.Instance.GetFont().GetStringRectangle(x.Text, Vector2.Zero).Width);
            float maxMessageHeight = _choices.Sum(x => GuiManager.Instance.GetFont().LineHeight) + GuiManager.Instance.GetFont().LineHeight;

            _container = new MenuContainer(_containerPosition, new Vector3(maxMessageWidth, maxMessageHeight, 1));

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
                GameManager.Instance.SpriteBatch.DrawShadowedText(_choices[i].Text, GuiManager.Instance.GetFont(), _metaData[i].TextPosition, Color.White);
            GameManager.Instance.SpriteBatch.Draw(GuiManager.Instance.GuiCursorTexture, _metaData[_choiceIndex].CursorPosition, Color.White);
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
                Vector2 textSize = GuiManager.Instance.GetFont().GetTextSize(_choices[i].Text);
                Vector2 buffer = new Vector2(GuiManager.Instance.GuiContainerTexture.Width, GuiManager.Instance.GuiContainerTexture.Height * (i + 1));
                Vector2 textPosition = new Vector2(_containerPosition.X + buffer.X, i * textSize.Y + buffer.Y + _containerPosition.Y);
                Vector2 cursorPosition = new Vector2(textPosition.X + textSize.X + GuiManager.Instance.GuiCursorTexture.Width, textPosition.Y + textSize.Y);

                _metaData.Add(new MenuChoiceMetaData
                {
                    TextSize = textSize,
                    TextPosition = textPosition,
                    CursorPosition = cursorPosition
                });
            }
        }

        /// <summary>
        /// Accepts this instance.
        /// </summary>
        private void Accept()
        {
            _choices[_choiceIndex].Accept();

            if (_unloadMenuOnAction)
                SceneManager.Instance.CurrentScene.UnloadMenu(this.Id);
        }

        /// <summary>
        /// Changes the index.
        /// </summary>
        /// <param name="action">The action.</param>
        private void ChangeIndex(MenuIndexAction action)
        {
            _choiceIndex = action == MenuIndexAction.Increment
                ? (_choiceIndex == _choiceCount ? 0 : _choiceIndex + 1)
                : (_choiceIndex == 0 ? _choiceCount : _choiceIndex - 1);
        }
    }
}
