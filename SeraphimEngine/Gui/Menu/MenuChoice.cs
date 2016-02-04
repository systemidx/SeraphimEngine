using System;
using SeraphimEngine.Exceptions;
using SeraphimEngine.Managers.Script;

namespace SeraphimEngine.Gui.Menu
{
    /// <summary>
    /// Class MenuChoice.
    /// </summary>
    public class MenuChoice : IMenuChoice
    {
        #region Public Variables

        /// <summary>
        /// The text
        /// </summary>
        public readonly string Text;

        /// <summary>
        /// The script
        /// </summary>
        public readonly Type Script;

        /// <summary>
        /// The action which fires on "accept"
        /// </summary>
        public readonly Action OnAccept;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuChoice" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="script">The script.</param>
        /// <exception cref="GuiException"></exception>
        /// s
        public MenuChoice(string text, Type script)
        {
            if (string.IsNullOrEmpty(text) || script == null)
                throw new GuiException();

            Text = text;
            Script = script;

            OnAccept = () => ScriptManager.Instance.StartScript(Script, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuChoice" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="onAccept">The on accept.</param>
        /// <exception cref="GuiException"></exception>
        public MenuChoice(string text, Action onAccept)
        {
            if (string.IsNullOrEmpty(text) || onAccept == null)
                throw new GuiException();

            Text = text;
            OnAccept = onAccept;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Accepts this instance.
        /// </summary>
        public void Accept()
        {
            OnAccept?.Invoke();
        }

        #endregion
    }
}
