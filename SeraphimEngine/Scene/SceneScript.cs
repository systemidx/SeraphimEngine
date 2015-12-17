using Microsoft.Xna.Framework;

namespace SeraphimEngine.Scene {
    public abstract class SceneScript : ISceneScript {

        public bool IsRunning { get; private set; }

        public void Start() {
            IsRunning = true;
        }

        public void Stop() {
            IsRunning = false;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}