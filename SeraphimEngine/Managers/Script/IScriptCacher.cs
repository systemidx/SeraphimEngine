using System.IO;
using JetBrains.Annotations;

namespace SeraphimEngine.Managers.Script
{
    public interface IScriptCacher
    {
        byte[] GetCachedScriptBytes(string id);
        void AddScriptToCache(string id, Stream scriptInstance, out byte[] bytes);
    }
}
