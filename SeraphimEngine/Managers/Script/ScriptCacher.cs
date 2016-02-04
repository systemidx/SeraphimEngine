using System;
using System.IO;
using System.Reflection;

namespace SeraphimEngine.Managers.Script
{
    public class ScriptCacher : IScriptCacher
    {
        private const string CACHE_DIRECTORY = "cache";
        private readonly DirectoryInfo _cacheDirectory = new DirectoryInfo(CACHE_DIRECTORY);

        public ScriptCacher()
        {
            if (!_cacheDirectory.Exists)
                _cacheDirectory.Create();
        }
        
        public byte[] GetCachedScriptBytes(string id)
        {
            if (!File.Exists(_cacheDirectory.FullName + "/" + id + ".bin"))
                return null;

            FileInfo cachedFile = new FileInfo(_cacheDirectory.FullName + "/" + id + ".bin");
            

            if (new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTimeUtc > cachedFile.LastWriteTimeUtc)
            {
                Console.WriteLine("Creating new cache file");
                return null;
            }

            Console.WriteLine("Returning cached file");
            return File.ReadAllBytes(_cacheDirectory.FullName + "/" + id + ".bin");
        }
        
        public void AddScriptToCache(string id, Stream scriptInstance, out byte[] bytes)
        {
            byte[] script = StreamToByteArray(scriptInstance);
            File.WriteAllBytes(_cacheDirectory.FullName + "/" + id + ".bin", script);

            bytes = script;
        }

        private byte[] StreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}
