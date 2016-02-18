using Microsoft.Xna.Framework;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Gui.Menu
{
    /// <summary>
    /// Class MenuGuiPosition.
    /// </summary>
    public class MenuGuiPosition
    {
        #region Public Variables

        /// <summary>
        /// The position
        /// </summary>
        public readonly Vector2 Position;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuGuiPosition"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public MenuGuiPosition(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuGuiPosition"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public MenuGuiPosition(float x, float y)
        {
            Position = new Vector2(x, y);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuGuiPosition"/> class.
        /// </summary>
        /// <param name="center">if set to <c>true</c> [center].</param>
        public MenuGuiPosition(bool center)
        {
            Position = center ? GetCenterCoordinates() : Vector2.Zero;
        }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Does the calculate to determine fine-tuned position. This method is not needed if using a Vector2 to place object.
        /// </summary>
        private Vector2 GetCenterCoordinates()
        {
            float widthSlice = SceneManager.Instance.Camera.VirtualResolution.X / 3;
            float heightSlice = SceneManager.Instance.Camera.VirtualResolution.Y / 3;

            return new Vector2(widthSlice, heightSlice);
        }

        #endregion
    }
}