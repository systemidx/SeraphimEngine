using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SeraphimEngine.Input;
using SeraphimEngine.Input.Enumerations;

namespace SeraphimEngine.Managers.Input
{
    /// <summary>
    /// Class InputManager.
    /// </summary>
    public class InputManager : Manager<InputManager>, IInputManager
    {
        #region Read Only Member Variables

        /// <summary>
        /// The keyboard input predicate
        /// </summary>
        private readonly Func<IEnumerable<Keys>> _keyboardInput = () => Keyboard.GetState().GetPressedKeys();

        /// <summary>
        /// The game pad input predicate
        /// </summary>
        private readonly Func<IEnumerable<Buttons>> _gamePadInput = () => GetPressedButtons();

        /// <summary>
        /// The input maps
        /// </summary>
        //todo: Make this configuration driven
        private readonly HashSet<GameActionInputMapping> _maps = new HashSet<GameActionInputMapping>
        {
            new GameActionInputMapping(GameAction.Accept, Keys.Enter, Buttons.A),
            new GameActionInputMapping(GameAction.Cancel, Keys.Escape, Buttons.B),
            new GameActionInputMapping(GameAction.Up, Keys.W, Buttons.DPadUp),
            new GameActionInputMapping(GameAction.Up, Keys.Up, Buttons.LeftThumbstickUp),
            new GameActionInputMapping(GameAction.Left, Keys.A, Buttons.DPadLeft),
            new GameActionInputMapping(GameAction.Left, Keys.Left, Buttons.LeftThumbstickLeft),
            new GameActionInputMapping(GameAction.Right, Keys.D, Buttons.DPadRight),
            new GameActionInputMapping(GameAction.Right, Keys.Right, Buttons.LeftThumbstickRight),
            new GameActionInputMapping(GameAction.Down, Keys.S, Buttons.DPadDown),
            new GameActionInputMapping(GameAction.Down, Keys.Down, Buttons.LeftThumbstickDown)
        };

        /// <summary>
        /// The collection of keys which are being pressed.
        /// </summary>
        private HashSet<GameAction> _keysDown = new HashSet<GameAction>();

        /// <summary>
        /// The previous keys down
        /// </summary>
        private readonly HashSet<GameAction> _previousKeysDown = new HashSet<GameAction>();

        //The reason for this stupidity is because XNA/MonoGame doesn't support enumerating button state
        //and MonoGame doesn't exactly natively support PS4 controllers via PacketNumber just yet.
        private static readonly HashSet<Buttons> GamepadButtons = new HashSet<Buttons>
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
        public GameActionInputDevice InputDevice { get; private set; } = GameActionInputDevice.None;

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
            //Poll for input type if we haven't established it
            if (InputDevice == GameActionInputDevice.None)
                PollForInputType();
            
            //Reset Previous Keys
            _previousKeysDown.Clear();
            foreach (GameAction action in _keysDown)
                _previousKeysDown.Add(action);

            //Reset the current keys before we recreate the collection
            _keysDown.Clear();
            
            //Get New Keys or Buttons
            if (InputDevice == GameActionInputDevice.Keyboard)
            {
                _keysDown = new HashSet<GameAction>(
                    _maps.Where(x => x.ActionKeys.Intersect(_keyboardInput.Invoke()).Any())
                        .Select(x => x.GameActionEvent));
            }
            else
            {
                _keysDown = new HashSet<GameAction>(
                    _maps.Where(x => x.ActionButtons.Intersect(_gamePadInput.Invoke()).Any())
                        .Select(x => x.GameActionEvent));
            }
        }

        /// <summary>
        /// Polls the type of for input.
        /// </summary>
        private void PollForInputType()
        {
            if (Keyboard.GetState().GetPressedKeys().Any())
            {
                InputDevice = GameActionInputDevice.Keyboard;
                return;
            }

            GamePadState gpState = GamePad.GetState(PlayerIndex.One);
            if (gpState.IsConnected && GamePadAnyButtonPressed(gpState))
                InputDevice = GameActionInputDevice.Controller;
        }

        /// <summary>
        /// This detects whether or not *any* button on the controller has been hit. 
        /// </summary>
        /// <param name="gpState">State of the gp.</param>
        /// <returns>System.Boolean.</returns>
        private bool GamePadAnyButtonPressed(GamePadState gpState)
        {
            return GamepadButtons.Any(gpState.IsButtonDown);
        }

        /// <summary>
        /// Gets the pressed buttons.
        /// </summary>
        /// <returns>System.Collections.Generic.IEnumerable&lt;Microsoft.Xna.Framework.Input.Buttons&gt;.</returns>
        private static IEnumerable<Buttons> GetPressedButtons()
        {
            GamePadState gpState = GamePad.GetState(PlayerIndex.One);
            return GamepadButtons.Where(gpState.IsButtonDown);
        }

        #endregion

        #region Publicly Exposed Input Methods

        /// <summary>
        /// Determines whether [is action down] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>
        ///   <c>true</c> if [is action down] [the specified action]; otherwise, <c>false</c>.</returns>
        public bool IsActionDown(GameAction action)
        {
            return _keysDown.Contains(action) && !_previousKeysDown.Contains(action);
        }

        /// <summary>
        /// Determines whether [is action held] [the specified action].
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns>
        ///   <c>true</c> if [is action held] [the specified action]; otherwise, <c>false</c>.</returns>
        public bool IsActionHeld(GameAction action)
        {
            return _keysDown.Contains(action) && _previousKeysDown.Contains(action);
        }

        #endregion
    }
}