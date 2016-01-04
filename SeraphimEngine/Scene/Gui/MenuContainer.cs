using JetBrains.Annotations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using SeraphimEngine.Helpers.Asset;
using SeraphimEngine.Managers.Game;

namespace SeraphimEngine.Scene.Gui
{
    public class MenuContainer : IDraw
    {
        private readonly MenuPosition _position;
        
        private readonly Texture2D _containerTexture;
        private readonly Vector2 _containerTextureDimensions;
        private readonly Vector2 _textDimensions;
        private readonly Vector2 _containerDimensions;

        private readonly Rectangle[] _spritesheetPositions = new Rectangle[9];
        private readonly Rectangle[] _containerSlices = new Rectangle[9];

        public MenuContainer(MenuPosition position, Vector2 textDimensions, [NotNull]Texture2D containerTexture)
        {
            _position = position;
            _containerTexture = containerTexture;
            _containerTextureDimensions = new Vector2(_containerTexture.Width, _containerTexture.Height);
            _textDimensions = textDimensions;
            _containerDimensions = new Vector2(_containerTextureDimensions.X/3 + _textDimensions.X, _containerTextureDimensions.Y/3 + _textDimensions.Y);

            _position.Initialize(_containerDimensions);

            SetRectangles();
        }

        private void SetRectangles()
        {
            for (int i = 0; i < 9; i++)
            {
                _spritesheetPositions[i] = ContainerHelper.GetSpritesheetScopeRect(i, _containerTextureDimensions);
                _containerSlices[i] = ContainerHelper.GetContainerRect(i, _position.Position, _containerTextureDimensions, _textDimensions);
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 9; ++i)
                GameManager.Instance.SpriteBatch.Draw(_containerTexture, _containerSlices[i], _spritesheetPositions[i], Color.White);
        }
    }
}
