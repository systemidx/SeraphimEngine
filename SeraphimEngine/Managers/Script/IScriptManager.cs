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
        void StartScript(string scriptId, ScriptType scriptType);

        /// <summary>
        /// Stops the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="scriptType">Type of the script.</param>
        void StopScript(string scriptId, ScriptType scriptType);
    }
}