using Microsoft.Xna.Framework;

namespace SeraphimEngine.Scene
{
    /// <summary>
    /// Class SceneScript.
    /// </summary>
    public abstract class SceneScript : ISceneScript
    {
        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// Gets a value indicating whether [run once].
        /// </summary>
        /// <value><c>true</c> if [run once]; otherwise, <c>false</c>.</value>
        public bool RunOnce { get; private set; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="runOnce">if set to <c>true</c> [run once].</param>
        public void Start(bool runOnce = false)
        {
            IsRunning = true;
            RunOnce = runOnce;
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            IsRunning = false;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public virtual void Update(GameTime gameTime)
        {
            if (RunOnce)
                Stop();
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public abstract void Draw(GameTime gameTime);
    }
}