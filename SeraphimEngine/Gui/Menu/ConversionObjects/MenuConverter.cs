using System.Reflection;
using Microsoft.Xna.Framework;
using SeraphimEngine.ContentPipeline.Menu;

namespace SeraphimEngine.Gui.Menu.ConversionObjects
{
    /// <summary>
    /// Class MenuConverter.
    /// </summary>
    public class MenuConverter
    {
        /// <summary>
        /// Converts the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>IMenuGui.</returns>
        public IMenuGui Convert(MenuData data)
        {
            Vector2 guiPosition = GetPosition(data);
            MenuChoice[] choices = GetChoices(data);
            
            return new MenuGui(data.Id, guiPosition, data.Center, data.OnActionClose, choices);
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Vector2.</returns>
        private Vector2 GetPosition(MenuData data)
        {
            float x;

            if (float.TryParse(data.X, out x))
            {
                float y = float.Parse(data.Y);

                return new Vector2(x, y);
            }

            return new Vector2(-1, -1);
        }

        /// <summary>
        /// Gets the choices.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>MenuChoice[].</returns>
        private MenuChoice[] GetChoices(MenuData data)
        {
            Assembly gameAssembly = Assembly.GetEntryAssembly();

            MenuChoice[] choices = new MenuChoice[data.Choices.Count];
            for (int i = 0; i < choices.Length; ++i)
                choices[i] = new MenuChoice(data.Choices[i].Text, gameAssembly.GetType($"{data.Choices[i].ScriptNamespace}.{data.Choices[i].Script}"));

            return choices;
        }
    }
}
