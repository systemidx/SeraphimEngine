using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using SeraphimEngine.ContentPipeline;
using SeraphimEngine.Exceptions;
using SeraphimEngine.Managers.Game;

namespace SeraphimEngine.Content
{
    /// <summary>
    /// Class VariableConverter. This class cannot be inherited.
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    public sealed class VariableConverter<TModel> : IVariableConvertible<TModel> where TModel : class
    {
        /// <summary>
        /// Converts the variables.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TModel.</returns>
        public TModel ConvertVariables(TModel model)
        {
            if (model == null)
                return null;

            FieldInfo[] fields = model.GetType().GetFields().Where(x => x.IsDefined(typeof(VariableConvertibleAttribute), false)).ToArray();
            PropertyInfo[] properties = model.GetType().GetProperties().Where(x => x.IsDefined(typeof(VariableConvertibleAttribute), false)).ToArray();

            const string PATTERN = "{{(.*)}}";
            Regex regex = new Regex(PATTERN);

            foreach (FieldInfo f in fields)
            {
                string value = f.GetValue(model) as string;
                if (value == null)
                    throw new GuiVariableConversionException();

                MatchCollection matches = regex.Matches(value);
                foreach (Match match in matches)
                {
                    string variableName = match.Value.Replace("{{", "").Replace("}}", "");
                    dynamic variableValue = GameManager.Instance.GetGameVariable(variableName);

                    f.SetValue(model, value.Replace(match.Value, variableValue));
                }
            }

            foreach (PropertyInfo p in properties)
            {
                string value = p.GetValue(model) as string;
                if (value == null)
                    throw new GuiVariableConversionException();

                MatchCollection matches = regex.Matches(value);
                foreach (Match match in matches)
                {
                    string variableName = match.Value.Replace("{{", "").Replace("}}", "");
                    dynamic variableValue = GameManager.Instance.GetGameVariable(variableName);

                    p.SetValue(model, value.Replace(match.Value, Convert.ToString(variableValue)));
                }
            }

            return model;
        }
    }
}
