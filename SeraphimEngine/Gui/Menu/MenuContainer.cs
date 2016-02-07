using Microsoft.Xna.Framework;
using MonoGame.Extended;
using SeraphimEngine.Helpers.Asset;
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
        /// The menu position
        /// </summary>
        private readonly MenuPosition _position;

        /// <summary>
        /// The container texture's dimensions
        /// </summary>
        private readonly Vector2 _containerTextureDimensions;

        /// <summary>
        /// The text dimensions
        /// </summary>
        private readonly Vector2 _textDimensions;

        /// <summary>
        /// The spritesheet positions
        /// </summary>
        private readonly Rectangle[] _spritesheetPositions = new Rectangle[9];

        /// <summary>
        /// The container slices
        /// </summary>
        private readonly Rectangle[] _containerSlices = new Rectangle[9];

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuContainer"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="textDimensions">The text dimensions.</param>
        public MenuContainer(MenuPosition position, Vector2 textDimensions)
        {
            _position = position;
            _containerTextureDimensions = new Vector2(GuiManager.Instance.GuiContainerTexture.Width, GuiManager.Instance.GuiContainerTexture.Height);
            _textDimensions = textDimensions;

            Vector2 containerDimensions = new Vector2(_containerTextureDimensions.X/3 + _textDimensions.X, _containerTextureDimensions.Y/3 + _textDimensions.Y);
            _position.Initialize(containerDimensions);

            SetContainerMetadata();
        }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 9; ++i)
                GameManager.Instance.SpriteBatch.Draw(GuiManager.Instance.GuiContainerTexture, _containerSlices[i], _spritesheetPositions[i], Color.White);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Sets the container metadata.
        /// </summary>
        private void SetContainerMetadata()
        {
            for (int i = 0; i < 9; i++)
            {
                _spritesheetPositions[i] = ContainerHelper.GetSpritesheetScopeRect(i, _containerTextureDimensions);
                _containerSlices[i] = ContainerHelper.GetContainerRect(i, _position.Position, _containerTextureDimensions, _textDimensions);
            }
        }

        #endregion
    }
}
