using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SeraphimEngine.Input;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Script;

namespace SeraphimEngine.Scene.Gui
{
    public class Menu : IMenu
    {
        private readonly IList<MenuChoice> _choices = new List<MenuChoice>();

        public Menu()
        {
            
        }

        public void Draw(GameTime gameTime)
        {
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsActionDown(InputAction.Accept))
            {
                ScriptManager.Instance.StartScript(_choices[1].ScriptName, ScriptType.Scene, true);
            }
        }

        public void Initialize(params MenuChoice[] choices)
        {
            foreach (MenuChoice choice in choices)
                _choices.Add(choice);
        }
    }
}
