using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SeraphimEngine.Definitions;
using SeraphimEngine.Helpers.Asset;
using SeraphimEngine.Helpers.Definitions;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Gui;

namespace SeraphimEngine.Gui.Menu
{
    /// <summary>
    /// Class MenuContainer.
    /// </summary>
    public class MenuContainer : IDraw
    {
        #region Private Variables
        
        /// <summary>
        /// The destination matrix
        /// </summary>
        private readonly Matrix3<RectangleF> _destinationRectangles;

        /// <summary>
        /// The source matrix
        /// </summary>
        private readonly Matrix3<RectangleF> _sourceRectangles;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuContainer"/> class.
        /// </summary>
        /// <param name="guiPosition">The guiPosition.</param>
        /// <param name="textDimensions">The text dimensions.</param>
        public MenuContainer(Vector2 guiPosition, Vector3 textDimensions)
        {
            Vector2 containerTextureDimensions = GuiManager.Instance.GuiContainerTexture.GetDimensions();

            _sourceRectangles = GuiManager.Instance.GuiContainerTexture.GetMatrix();
            _destinationRectangles = MessageHelper.CreateContainerDestinationMatrix(guiPosition, containerTextureDimensions, textDimensions);
        }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            for (int col = 0; col < 3; ++col)
                for (int row = 0; row < 3; ++row)
                    GameManager.Instance.SpriteBatch.Draw(GuiManager.Instance.GuiContainerTexture, _destinationRectangles[row, col].ToRectangle(), _sourceRectangles[row, col].ToRectangle(), Color.White);
        }

        #endregion
    }
}
