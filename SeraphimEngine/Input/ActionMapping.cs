using Microsoft.Xna.Framework.Input;

namespace SeraphimEngine.Input
{
    public class ActionMapping
    {
        public readonly Buttons ActionButton;
        public readonly Keys ActionKey;

        public ActionMapping(Keys actionKey, Buttons actionButton)
        {
            ActionButton = actionButton;
            ActionKey = actionKey;
        }
    }
}
