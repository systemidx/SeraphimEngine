using MonoGame.Extended;

namespace SeraphimEngine.Script
{
    /// <summary>
    /// Interface IScript
    /// </summary>
    public interface IScript : IUpdate, IDraw
    {
        /// <summary>
        /// Gets a value indicating whether this instance is running.
        /// </summary>
        /// <value><c>true</c> if this instance is running; otherwise, <c>false</c>.</value>
        bool IsRunning { get; }

        /// <summary>
        /// Gets a value indicating whether [run once].
        /// </summary>
        /// <value><c>true</c> if [run once]; otherwise, <c>false</c>.</value>
        bool RunOnce { get; }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <param name="runOnce">if set to <c>true</c> [run once].</param>
        void Start(bool runOnce = false);

        /// <summary>
        /// Stops this instance.
        /// </summary>
        void Stop();
    }
}