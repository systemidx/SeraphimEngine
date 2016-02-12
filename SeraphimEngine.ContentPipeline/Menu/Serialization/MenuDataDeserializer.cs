using System;
using System.Xml;

namespace SeraphimEngine.ContentPipeline.Menu.Serialization
{
    public class MenuDataDeserializer : XmlDeserializer<MenuData>
    {
        public override MenuData Retrieve(XmlElement element)
        {
            MenuData data = new MenuData
            {
                Id = element["Id"].InnerText,
                OnActionClose = Convert.ToBoolean(element.GetAttribute("OnActionClose")),
                X = element["Position"].GetAttribute("X"),
                Y = element["Position"].GetAttribute("Y"),
                Center = Convert.ToBoolean(element["Position"].GetAttribute("Center"))
            };

            XmlNodeList choiceNodeList = element["Choices"].SelectNodes("Choice");
            if (choiceNodeList.Count > 0)
            {
                foreach (XmlElement choiceElement in choiceNodeList)
                { 
                    data.Choices.Add(new MenuChoiceData
                    {
                        Script = choiceElement.GetAttribute("Script"),
                        ScriptNamespace = choiceElement.GetAttribute("ScriptNamespace"),
                        Text = choiceElement.InnerText
                    });
                }
            }

            return data;
        }
    }
}
