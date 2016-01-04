using Microsoft.Xna.Framework.Input;

namespace SeraphimEngine.Input
{
    public class ActionMapping
    {
        public readonly InputAction Event;
        public readonly Buttons[] ActionButtons;
        public readonly Keys[] ActionKeys;

        public ActionMapping(InputAction @event, Keys actionKey, Buttons actionButton)
        {
            Event = @event;
            ActionButtons = new Buttons[] {actionButton};
            ActionKeys = new Keys[] { actionKey };
        }

        public ActionMapping(InputAction @event, Keys[] actionKeyses, Buttons[] actionButtonses)
        {
            Event = @event;
            ActionButtons = actionButtonses;
            ActionKeys = actionKeyses;
        }
    }
}
