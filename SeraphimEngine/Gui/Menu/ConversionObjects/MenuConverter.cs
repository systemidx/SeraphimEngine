using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using SeraphimEngine.ContentPipeline.Menu;
using SeraphimEngine.Gui.Menu.Enumerations;

namespace SeraphimEngine.Gui.Menu.ConversionObjects
{
    public class MenuConverter
    {
        public IMenuGui Convert(MenuData data)
        {
            return new MenuGui(data.Id, GetPosition(data), data.OnActionClose, GetChoices(data));
        }

        private MenuPosition GetPosition(MenuData data)
        {
            float x = -1.0f;
            float y = -1.0f;

            if (float.TryParse(data.X, out x)) { 
                float.TryParse(data.Y, out y);

                return new MenuPosition(new Vector2(x, y));
            }
            
            MenuPositionHorizontal h;
            MenuPositionVertical v;

            Enum.TryParse(data.X, true, out h);
            Enum.TryParse(data.X, true, out v);

            return new MenuPosition(h, v, data.Center);
        }

        private MenuChoice[] GetChoices(MenuData data)
        {
            Assembly gameAssembly = Assembly.GetEntryAssembly();
            return data.Choices.Select(choiceData => new MenuChoice(choiceData.Text, gameAssembly.GetType($"{choiceData.ScriptNamespace}.{choiceData.Script}"))).ToArray();
        }
    }
}
