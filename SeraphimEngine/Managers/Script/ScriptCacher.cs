using System;
using System.IO;
using System.Reflection;

namespace SeraphimEngine.Managers.Script
{
    /// <summary>
    /// Class ScriptCacher.
    /// </summary>
    public class ScriptCacher : IScriptCacher
    {
        #region Constants

        /// <summary>
        /// The cache directory
        /// </summary>
        private const string CACHE_DIRECTORY = "cache";

        /// <summary>
        /// The cache file pattern
        /// </summary>
        private const string CACHE_FILE_PATTERN = "{0}\\{1}.bin";

        #endregion

        #region Private Variables

        /// <summary>
        /// The cache directory info
        /// </summary>
        private readonly DirectoryInfo _cacheDirectory = new DirectoryInfo(CACHE_DIRECTORY);

        /// <summary>
        /// The last build time. This is used to determine cache invalidation.
        /// </summary>
        private readonly DateTime _lastBuildTime =
            new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTimeUtc;
        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptCacher"/> class.
        /// </summary>
        public ScriptCacher()
        {
            if (!_cacheDirectory.Exists)
                _cacheDirectory.Create();
        }

        #endregion

        /// <summary>
        /// Gets the cached script bytes.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>System.Byte[].</returns>
        public byte[] GetCachedScriptBytes(string id)
        {
            string cacheFilePath = string.Format(CACHE_FILE_PATTERN, _cacheDirectory.FullName, id);
            if (!File.Exists(cacheFilePath))
                return null;

            FileInfo cachedFile = new FileInfo(cacheFilePath);
            if (_lastBuildTime > cachedFile.LastWriteTimeUtc)
                return null;
            
            return File.ReadAllBytes(cacheFilePath);
        }

        /// <summary>
        /// Adds the script to cache.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="scriptInstance">The script instance.</param>
        /// <param name="bytes">The bytes.</param>
        public void AddScriptToCache(string id, Stream scriptInstance, out byte[] bytes)
        {
            string cacheFilePath = string.Format(CACHE_FILE_PATTERN, _cacheDirectory.FullName, id);
            bytes = StreamToByteArray(scriptInstance);

            if ((bytes == null) || (bytes.Length == 0))
                return;

            File.WriteAllBytes(cacheFilePath, bytes);
        }

        /// <summary>
        /// Streams to byte array.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>System.Byte[].</returns>
        private byte[] StreamToByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }
    }
}
