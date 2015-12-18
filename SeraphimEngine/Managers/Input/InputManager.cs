using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;

namespace SeraphimEngine.Managers.Input
{
    /// <summary>
    /// Class InputManager.
    /// </summary>
    public class InputManager : Manager<InputManager>, IInputManager
    {
        #region Read Only Member Variables

        /// <summary>
        /// The input listener manager. This listens on the specified input and gets updated during the 
        /// game loop.
        /// </summary>
        private readonly InputListenerManager _manager = new InputListenerManager();

        /// <summary>
        /// The specified input listener, which attaches to the manager.
        /// </summary>
        private readonly KeyboardListener _listener;

        /// <summary>
        /// The collection of keys which are being pressed.
        /// </summary>
        private readonly HashSet<Keys> _keysDown = new HashSet<Keys>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public override bool IsInitialized { get; protected set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="InputManager"/> class.
        /// </summary>
        public InputManager()
        {
            _listener = _manager.AddListener(new KeyboardListenerSettings());
        }

        #endregion

        #region Game Loop Methods

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            //Key Down Event (Adds to HashSet)
            _listener.KeyPressed += (sender, args) =>
            {
                if (_keysDown.Contains(args.Key))
                    return;
                _keysDown.Add(args.Key);

                Console.WriteLine($"{args.Key} down");
            };

            //Key Up Event (Removes from HashSet)
            _listener.KeyReleased += (sender, args) =>
            {
                _keysDown.RemoveWhere(x => x == args.Key);
                Console.WriteLine($"{args.Key} up");
            };

            IsInitialized = true;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            _manager.Update(gameTime);
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
            return _keysDown.Contains(key);
        }

        #endregion
    }
}