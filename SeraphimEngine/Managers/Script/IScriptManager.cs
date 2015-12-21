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
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="scriptType">Type of the script.</param>
        /// <param name="runOnce">if set to <c>true</c> [run once].</param>
        void StartScript(string scriptId, ScriptType scriptType, bool runOnce = false);

        /// <summary>
        /// Stops the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="scriptType">Type of the script.</param>
        void StopScript(string scriptId, ScriptType scriptType);

        /// <summary>
        /// Determines whether the specified script identifier is running.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="scriptType">Type of the script.</param>
        /// <returns><c>true</c> if the specified script identifier is running; otherwise, <c>false</c>.</returns>
        bool IsRunning(string scriptId, ScriptType scriptType);
    }
}