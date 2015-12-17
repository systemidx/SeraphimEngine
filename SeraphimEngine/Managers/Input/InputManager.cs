using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.InputListeners;

namespace SeraphimEngine.Managers.Input
{
    public class InputManager : Manager<InputManager>, IInputManager
    {
        private readonly InputListenerManager _manager = new InputListenerManager();
        private readonly KeyboardListener _listener;
        private readonly HashSet<Keys> _keysDown = new HashSet<Keys>();

        public InputManager() {
            _listener = _manager.AddListener(new KeyboardListenerSettings());
            
        }

        public override bool IsInitialized { get; protected set; }
        public override void Initialize(ContentManager content, GraphicsDevice graphics) {
            _listener.KeyPressed += (sender, args) => {
                if (_keysDown.Contains(args.Key))
                    return;
                _keysDown.Add(args.Key);

                Console.WriteLine($"{args.Key} down");
            };

            _listener.KeyReleased += (sender, args) => {
                _keysDown.RemoveWhere(x => x == args.Key);
                Console.WriteLine($"{args.Key} up");
            };

            IsInitialized = true;
        }

        public void Update(GameTime gameTime) {
            _manager.Update(gameTime);
        }

        public bool IsKeyDown(Keys key) {
            return _keysDown.Contains(key);
        }
    }
}
