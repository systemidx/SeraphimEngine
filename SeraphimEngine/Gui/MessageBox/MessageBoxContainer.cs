using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SeraphimEngine.Definitions;
using SeraphimEngine.Helpers.Asset;
using SeraphimEngine.Helpers.Definitions;
using SeraphimEngine.Managers.Game;
using SeraphimEngine.Managers.Gui;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Gui.MessageBox
{
    /// <summary>
    /// Class MenuContainer.
    /// </summary>
    public class MessageBoxContainer : IDraw
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
        /// Initializes a new instance of the <see cref="MessageBoxContainer"/> class.
        /// </summary>
        /// <param name="textSize">Size of the text.</param>
        public MessageBoxContainer(Vector2? textSize = null)
        {
            Vector2 containerTextureDimensions = new Vector2(GuiManager.Instance.GuiContainerTexture.Width, GuiManager.Instance.GuiContainerTexture.Height);
            Vector3 textDimensions = textSize.HasValue ? 
                new Vector3(textSize.Value.X, textSize.Value.Y, 1) : 
                new Vector3(SceneManager.Instance.Camera.Resolution.X - containerTextureDimensions.X, SceneManager.Instance.Camera.Resolution.Y / 5, 2);

            _sourceRectangles = GuiManager.Instance.GuiContainerTexture.GetMatrix();
            _destinationRectangles = MessageHelper.CreateContainerDestinationMatrix(Vector2.Zero, containerTextureDimensions, textDimensions);
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
