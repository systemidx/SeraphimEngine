using Microsoft.Xna.Framework;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Scene.Gui
{
    /// <summary>
    /// Class MenuPosition.
    /// </summary>
    public class MenuPosition
    {
        public Vector2 Position;

        private readonly bool _center;
        private readonly MenuPositionHorizontal _horizontalPosition;
        private readonly MenuPositionVertical _verticalPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPosition"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public MenuPosition(Vector2 position)
        {
            Position = position;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuPosition" /> class.
        /// </summary>
        /// <param name="horizontalPosition">The horizontal position.</param>
        /// <param name="verticalPosition">The vertical position.</param>
        /// <param name="center">if set to <c>true</c> [center].</param>
        public MenuPosition(MenuPositionHorizontal horizontalPosition, MenuPositionVertical verticalPosition, bool center = false)
        {
            _horizontalPosition = horizontalPosition;
            _verticalPosition = verticalPosition;
            _center = center;
        }

        /// <summary>
        /// Does the calculate to determine fine-tuned position. This method is not needed if using a Vector2 to place object.
        /// </summary>
        /// <param name="containerShape">The container shape.</param>
        public void Initialize(Vector2 containerShape = default(Vector2))
        {
            int widthSlice = SceneManager.Instance.ViewportAdapter.VirtualWidth / 3;
            int heightSlice = SceneManager.Instance.ViewportAdapter.VirtualHeight / 3;

            int hPosition = (int) _horizontalPosition;
            int vPosition = (int) _verticalPosition;

            int hStart = hPosition * widthSlice;
            int vStart = vPosition * heightSlice;

            if (_center)
            {
                if (containerShape.X <= widthSlice)
                    hStart += (int)(widthSlice - containerShape.X) / 2;
                if (containerShape.Y <= heightSlice)
                    vStart += (int)(heightSlice - containerShape.Y) / 2;
            }

            Position = new Vector2(hStart, vStart);
        }
    }
}