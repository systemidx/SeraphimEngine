using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;
using SeraphimEngine.Input;

namespace SeraphimEngine.Managers.Input
{
    /// <summary>
    /// Class InputManager.
    /// </summary>
    public class InputManager : Manager<InputManager>, IInputManager
    {
        #region Read Only Member Variables

        /// <summary>
        /// The collection of keys which are being pressed.
        /// </summary>
        private readonly HashSet<Keys> _keysDown = new HashSet<Keys>();

        /// <summary>
        /// The previous keys down
        /// </summary>
        private readonly HashSet<Keys> _previousKeysDown = new HashSet<Keys>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public override bool IsInitialized { get; protected set; }

        /// <summary>
        /// Gets the input method.
        /// </summary>
        /// <value>The input method.</value>
        public ActionInputMethod InputMethod { get; private set; } = ActionInputMethod.None;

        #endregion

        #region Game Loop Methods

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            IsInitialized = true;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (InputMethod != ActionInputMethod.None)
            {
                //Reset Previous Keys
                _previousKeysDown.Clear();
                foreach (Keys k in _keysDown)
                    _previousKeysDown.Add(k);

                _keysDown.Clear();
                //Get New Keys
                foreach (Keys k in Keyboard.GetState().GetPressedKeys())
                    _keysDown.Add(k);
            }

            if (Keyboard.GetState().GetPressedKeys().Any()) { 
                InputMethod = ActionInputMethod.Keyboard;
                return;
            }

            if (GamePad.GetState(PlayerIndex.One).PacketNumber > 0) { 
                InputMethod = ActionInputMethod.Controller;
            }
        }

        #endregion

        #region Publicly Exposed Input Methods

        /// <summary>
        /// Determines whether a specified key is held down.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if [is key down] [the specified key]; otherwise, <c>false</c>.</returns>
        public bool IsKeyDown(Keys key)
        {
            return _keysDown.Contains(key) && !_previousKeysDown.Contains(key);
        }

        /// <summary>
        /// Determines whether [is key pressed] [the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if [is key pressed] [the specified key]; otherwise, <c>false</c>.</returns>
        public bool IsKeyHeld(Keys key)
        {
            return _previousKeysDown.Contains(key) && _keysDown.Contains(key);
        }

        /// <summary>
        /// Determines whether [is action down] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if [is action down] [the specified action]; otherwise, <c>false</c>.</returns>
        public bool IsActionDown(IInputAction action)
        {
            return _keysDown.Contains(action.ActionKey) && !_previousKeysDown.Contains(action.ActionKey);
        }

        /// <summary>
        /// Determines whether [is action held] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns><c>true</c> if [is action held] [the specified action]; otherwise, <c>false</c>.</returns>
        public bool IsActionHeld(IInputAction action)
        {
            return _previousKeysDown.Contains(action.ActionKey) && _keysDown.Contains(action.ActionKey);
        }

        #endregion
    }
}