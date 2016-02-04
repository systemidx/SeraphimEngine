using System;
using MonoGame.Extended;

namespace SeraphimEngine.Managers.Script
{
    /// <summary>
    /// Interface IScriptManager
    /// </summary>
    public interface IScriptManager : IManager, IUpdate, IDraw
    {
        /// <summary>
        /// Starts the script.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <param name="runOnce">if set to <c>true</c> [run once].</param>
        void StartScript(Type script, bool runOnce = false);

        /// <summary>
        /// Stops the script.
        /// </summary>
        /// <param name="script">The script.</param>
        void StopScript(Type script);

        /// <summary>
        /// Determines whether the specified script identifier is running.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns><c>true</c> if the specified script identifier is running; otherwise, <c>false</c>.</returns>
        bool IsRunning(Type script);
    }
}