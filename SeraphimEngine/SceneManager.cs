using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;

namespace SeraphimEngine {
    public class SceneManager : ISceneManager {
        private GraphicsDevice _graphics;
        private ContentManager _content;

        public ViewportAdapter ViewportAdapter { get; private set; }
        public IScene CurrentScene { get; private set; }
        public bool IsInitialized { get; private set; } = false;


        private SceneManager() {}

        public static readonly SceneManager Instance = new Lazy<SceneManager>(() => new SceneManager()).Value;

        public void Initialize(ContentManager content, GraphicsDevice graphics) {
            _content = content;
            _graphics = graphics;
            ViewportAdapter = new ScalingViewportAdapter(_graphics, 800, 600);

            IsInitialized = true;
        }

        public void SwitchScene(string sceneId) {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            CurrentScene?.Exit();
            CurrentScene = LoadScene();
            CurrentScene.Enter();
        }

        private IScene LoadScene() {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            return new Scene(_graphics, ViewportAdapter);
        }

        public void Update(GameTime gameTime) {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            if (CurrentScene == null)
                throw new SceneInitializationException();

            CurrentScene.Update(gameTime);
        }

        public void Draw(GameTime gameTime) {
            if (!IsInitialized)
                throw new SceneManagerInitializationException();

            if (CurrentScene == null)
                throw new SceneInitializationException();

            CurrentScene.Draw(gameTime);
        }
    }
}