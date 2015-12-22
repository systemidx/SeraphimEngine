using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        /// The input maps
        /// </summary>
        //todo: Make this configuration driven
        private readonly IDictionary<InputAction, ActionMapping> _maps = new Dictionary<InputAction, ActionMapping>
        {
            {InputAction.Accept, new ActionMapping(Keys.Enter, Buttons.A)},
            {InputAction.Cancel, new ActionMapping(Keys.Escape, Buttons.B)},
            {InputAction.Down, new ActionMapping(Keys.S, Buttons.DPadDown)},
            {InputAction.Up, new ActionMapping(Keys.W, Buttons.DPadUp)},
            {InputAction.Left, new ActionMapping(Keys.A, Buttons.DPadLeft)},
            {InputAction.Right, new ActionMapping(Keys.D, Buttons.DPadRight)}
        };

        /// <summary>
        /// The collection of keys which are being pressed.
        /// </summary>
        private readonly HashSet<InputAction> _keysDown = new HashSet<InputAction>();

        /// <summary>
        /// The previous keys down
        /// </summary>
        private readonly HashSet<InputAction> _previousKeysDown = new HashSet<InputAction>();

        //The reason for this stupidity is because XNA/MonoGame doesn't support enumerating button state
        //and MonoGame doesn't exactly natively support PS4 controllers via PacketNumber just yet.
        private readonly HashSet<Buttons> _gamepadButtons = new HashSet<Buttons>
        {
            Buttons.A,
            Buttons.B,
            Buttons.Back,
            Buttons.BigButton,
            Buttons.DPadDown,
            Buttons.DPadLeft,
            Buttons.DPadRight,
            Buttons.DPadUp,
            Buttons.RightShoulder,
            Buttons.RightStick,
            Buttons.RightThumbstickDown,
            Buttons.RightThumbstickLeft,
            Buttons.RightThumbstickRight,
            Buttons.RightThumbstickUp,
            Buttons.LeftTrigger,
            Buttons.LeftShoulder,
            Buttons.LeftStick,
            Buttons.LeftThumbstickDown,
            Buttons.LeftThumbstickLeft,
            Buttons.LeftThumbstickRight,
            Buttons.LeftThumbstickUp,
            Buttons.LeftTrigger,
            Buttons.Start,
            Buttons.X,
            Buttons.Y
        };

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
                foreach (InputAction action in _keysDown)
                    _previousKeysDown.Add(action);

                _keysDown.Clear();

                //Get New Keys
                switch (InputMethod)
                {
                    case ActionInputMethod.Keyboard:
                        Keys[] currentKeys = Keyboard.GetState().GetPressedKeys();

                        foreach (Keys k in currentKeys)
                        {
                            InputAction[] currentActions =
                                _maps.Where(x => x.Value.ActionKey == k).Select(y => y.Key).ToArray();
                            foreach (InputAction action in currentActions)
                                _keysDown.Add(action);
                        }

                        break;

                    case ActionInputMethod.Controller:
                        IEnumerable<Buttons> currentButtons = GetPressedButtons();

                        foreach (Buttons b in currentButtons)
                        {
                            InputAction[] currentActions =
                                _maps.Where(x => x.Value.ActionButton == b).Select(y => y.Key).ToArray();
                            foreach (InputAction action in currentActions)
                                _keysDown.Add(action);
                        }

                        break;
                }
            }

            PollForInputType();
        }

        /// <summary>
        /// Polls the type of for input.
        /// </summary>
        private void PollForInputType()
        {
            if (Keyboard.GetState().GetPressedKeys().Any())
            {
                InputMethod = ActionInputMethod.Keyboard;
                return;
            }

            GamePadState gpState = GamePad.GetState(PlayerIndex.One);
            if (gpState.IsConnected && GamePadAnyButtonPressed(gpState))
                InputMethod = ActionInputMethod.Controller;
        }

        /// <summary>
        /// This detects whether or not *any* button on the controller has been hit. 
        /// </summary>
        /// <param name="gpState">State of the gp.</param>
        /// <returns>System.Boolean.</returns>
        private bool GamePadAnyButtonPressed(GamePadState gpState)
        {
            return _gamepadButtons.Any(gpState.IsButtonDown);
        }

        /// <summary>
        /// Gets the pressed buttons.
        /// </summary>
        /// <returns>System.Collections.Generic.IEnumerable&lt;Microsoft.Xna.Framework.Input.Buttons&gt;.</returns>
        private IEnumerable<Buttons> GetPressedButtons()
        {
            GamePadState gpState = GamePad.GetState(PlayerIndex.One);
            return _gamepadButtons.Where(gpState.IsButtonDown);
        }

        #endregion

        #region Publicly Exposed Input Methods

        /// <summary>
        /// Determines whether [is action down] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>
        ///   <c>true</c> if [is action down] [the specified action]; otherwise, <c>false</c>.</returns>
        public bool IsActionDown(InputAction action)
        {
            return _keysDown.Contains(action) && !_previousKeysDown.Contains(action);
        }

        /// <summary>
        /// Determines whether [is action held] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>
        ///   <c>true</c> if [is action held] [the specified action]; otherwise, <c>false</c>.</returns>
        public bool IsActionHeld(InputAction action)
        {
            return _keysDown.Contains(action) && _previousKeysDown.Contains(action);
        }

        #endregion
    }
}