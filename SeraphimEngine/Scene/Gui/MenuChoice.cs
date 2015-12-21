using SeraphimEngine.Exceptions;
using SeraphimEngine.Script;

namespace SeraphimEngine.Scene.Gui
{
    public class MenuChoice
    {
        public readonly string Text;
        public readonly string ScriptName;

        public MenuChoice(string text, string scriptName)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(scriptName))
                throw new GuiException();

            ScriptName = scriptName;
            Text = text;
        }
    }
}
