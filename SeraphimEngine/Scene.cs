using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Managers;
using SeraphimEngine.Managers.Asset;

namespace SeraphimEngine
{
    public class Scene : IScene {
        public Camera2D SceneCamera { get; }
        private readonly SpriteBatch _spriteBatch;

        public Scene(GraphicsDevice graphics, ViewportAdapter viewport) {
            SceneCamera = new Camera2D(viewport);
            _spriteBatch = new SpriteBatch(graphics);
        }

        public void Update(GameTime gameTime) {
        }

        public void Draw(GameTime gameTime) {
            Matrix tformMatrix = SceneCamera.GetViewMatrix(Vector2.Zero);
            Matrix viewMatrix = SceneCamera.GetViewMatrix();

            _spriteBatch.Begin(transformMatrix: tformMatrix);
            _spriteBatch.Draw(AssetManager.Instance.GetAsset<Texture2D>("spritesheets/ui"), Vector2.Zero, Color.White);
            _spriteBatch.End();
        }

        
        public void Enter() {
        }

        public void Exit() {
        }
    }
}
