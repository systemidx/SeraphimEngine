using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.Script;
using SeraphimEngine.Exceptions;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Script;

namespace SeraphimEngine.Managers.Script
{
    /// <summary>
    /// Class ScriptManager.
    /// </summary>
    public class ScriptManager : Manager<ScriptManager>, IScriptManager
    {
        #region Constants

        /// <summary>
        /// The content directory
        /// </summary>
        private const string CONTENT_DIRECTORY = "Content/Scripts/";

        #endregion

        #region Override Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public override bool IsInitialized { get; protected set; }

        #endregion

        #region Read Only Member Variables

        /// <summary>
        /// The assembly reference collection. This collection holds all of the metadata references used by the Roslyn compiler (which we are
        /// choosing to expose)
        /// </summary>
        private readonly HashSet<MetadataReference> _assemblies = new HashSet<MetadataReference>();

        /// <summary>
        /// The  options which are used to compile the scriptMetaData code.
        /// </summary>
        private readonly CSharpCompilationOptions _compilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

        /// <summary>
        /// The cacher instance
        /// </summary>
        private readonly IScriptCacher _cacher = new ScriptCacher();

        /// <summary>
        /// The compilation tasks collection. This collection holds all of the compilation tasks, so if they are not finished compiling when called,
        /// we know to wait.
        /// </summary>
        private readonly IDictionary<string, Task> _compilationTasks = new Dictionary<string, Task>();

        #endregion

        #region Variables

        /// <summary>
        /// The scripts list. This holds references to scripts which are already registered, compiled, and cached.
        /// </summary>
        private readonly IList<IScript> _scripts = new List<IScript>();

        /// <summary>
        /// The scripts that are serialized from the Content
        /// </summary>
        private ScriptMetaData[] _scriptMetaDataMetaData;

        #endregion

        #region Exposed SeraphimScript Control Methods

        /// <summary>
        /// Starts the scriptMetaData.
        /// </summary>
        /// <param name="script">The scriptMetaData.</param>
        /// <param name="runOnce">if set to <c>true</c> [run once].</param>
        /// <exception cref="ScriptManagerInitializationException"></exception>
        public void StartScript(Type script, bool runOnce = false)
        {
            if (!IsInitialized)
                throw new ScriptManagerInitializationException();

            //Get the index of the script
            int idx = ((List<IScript>) _scripts).FindIndex(x => x.GetType().Name == script.Name);
            if (idx > -1)
            {
                //Check to see if task is still in the collection and remove it
                _compilationTasks.Remove(script.Name);

                Task.Run(() => _scripts[idx].Start(runOnce));
                return;
            }

            //If we can't find it, let's look to see if it's still compiling
            Task task =
                _compilationTasks.FirstOrDefault(
                    x =>
                        x.Value.Status == TaskStatus.Running &&
                        string.Compare(x.Key, script.Name, StringComparison.OrdinalIgnoreCase) > 0).Value;

            if (task == null)
                throw new ScriptNotRegisteredException();

            //If it's still compiling, wait...
            while (task.Status == TaskStatus.Running)
                Thread.Sleep(10);

            //Recurse and try again
            StartScript(script, runOnce);
        }

        /// <summary>
        /// Stops the scriptMetaData.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <exception cref="ScriptManagerInitializationException"></exception>
        public void StopScript(Type script)
        {
            if (!IsInitialized)
                throw new ScriptManagerInitializationException();

            int idx = ((List<IScript>) _scripts).FindIndex(x => x.GetType().Name == script.Name);
            if (idx > -1)
                return;

            _scripts[idx].Stop();
        }

        /// <summary>
        /// Determines whether the specified scriptMetaData identifier is running.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns><c>true</c> if the specified scriptMetaData identifier is running; otherwise, <c>false</c>.</returns>
        /// <exception cref="ScriptManagerInitializationException"></exception>
        public bool IsRunning(Type script)
        {
            if (!IsInitialized)
                throw new ScriptManagerInitializationException();

            int idx = ((List<IScript>) _scripts).FindIndex(x => x.GetType().Name == script.Name);
            return idx > -1 && _scripts[idx].IsRunning;
        }

        #endregion

        #region Exposed Game Flow Methods

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            SetAssemblies();

            DateTime start = DateTime.Now;
            PreloadScripts();

            Console.WriteLine($"{(DateTime.Now - start).TotalMilliseconds} ms");
            IsInitialized = true;
        }

        /// <summary>
        /// Sends the update command to all active scripts
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (_scripts == null)
                return;

            foreach (var script in _scripts.Where(x => x.IsRunning))
                script.Update(gameTime);
        }

        /// <summary>
        /// Sends the draw command to all active scripts
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            if (_scripts == null)
                return;

            foreach (var script in _scripts.Where(x => x.IsRunning))
                script.Draw(gameTime);
        }

        #endregion

        #region Compilation Methods

        /// <summary>
        /// Sets the assemblies.
        /// </summary>
        /// <returns>AssemblyReferences.</returns>
        private void SetAssemblies()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _assemblies.Add(MetadataReference.CreateFromFile(typeof (object).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(typeof (Enumerable).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(Assembly.GetEntryAssembly().Location));
            _assemblies.Add(MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\SeraphimEngine.dll").Location));
            _assemblies.Add(
                MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\MonoGame.Extended.dll").Location));
            _assemblies.Add(
                MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\MonoGame.Framework.dll").Location));
        }

        /// <summary>
        /// Creates the scriptMetaData.
        /// </summary>
        /// <param name="scriptMetaData">The scriptMetaData.</param>
        /// <returns>CScript.</returns>
        /// <exception cref="AssetManagerInitializationException"></exception>
        /// <exception cref="ScriptCodeException">
        /// </exception>
        /// <exception cref="SeraphimEngine.Exceptions.AssetManagerInitializationException"></exception>
        private IScript CompileScript([NotNull] ScriptMetaData scriptMetaData)
        {
            if (!AssetManager.Instance.IsInitialized)
                throw new AssetManagerInitializationException();

            //Try to get cached item
            byte[] scriptBytes = _cacher.GetCachedScriptBytes(scriptMetaData.Id);

            //If we were able to get a cached version, create an instance from that
            if (scriptBytes != null)
                return CreateScriptFromSource(scriptBytes);

            //Build the syntax tree
            SyntaxTree[] syntaxTree =
            {
                CSharpSyntaxTree.ParseText(scriptMetaData.Code)
            };

            //Compile the tree
            CSharpCompilation compilation = CSharpCompilation.Create(scriptMetaData.Id, syntaxTree, _assemblies, _compilationOptions);

            //Stream the results to the internal assembly and create an instance
            using (MemoryStream stream = new MemoryStream())
            {
                //Get the result
                EmitResult result = compilation.Emit(stream);

                //Create an instance and return it
                if (result.Success)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    _cacher.AddScriptToCache(scriptMetaData.Id, stream, out scriptBytes);
                    return CreateScriptFromSource(scriptBytes);
                }

                //Deal with the errors
                IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                    diagnostic.IsWarningAsError ||
                    diagnostic.Severity == DiagnosticSeverity.Error);

                //todo: Figure out a better way to log these failures.
                foreach (Diagnostic diagnostic in failures)
                    Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());

                throw new ScriptCodeException();
            }
        }

        /// <summary>
        /// Creates the script from source.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>IScript.</returns>
        private IScript CreateScriptFromSource(byte[] bytes)
        {
            Assembly asm = Assembly.Load(bytes);
            Type type =
                asm.GetTypes()
                    .First(
                        myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof (SeraphimScript)));

            return (IScript) Activator.CreateInstance(type);
        }

        /// <summary>
        /// Preloads the scripts.
        /// </summary>
        private void PreloadScripts()
        {
            _scriptMetaDataMetaData = AssetManager.Instance.GetAllAssets<ScriptMetaData>(CONTENT_DIRECTORY);

            foreach (ScriptMetaData scriptContent in _scriptMetaDataMetaData)
                _compilationTasks.Add(scriptContent.Id, GetCompileTask(scriptContent));

            foreach (Task task in _compilationTasks.Values)
                task.Start();
        }

        /// <summary>
        /// Gets the compile task.
        /// </summary>
        /// <param name="scriptMetaData">Content of the script.</param>
        /// <returns>Task.</returns>
        private Task GetCompileTask([NotNull] ScriptMetaData scriptMetaData)
        {
            return new Task(() => _scripts.Add(CompileScript(scriptMetaData)));
        }

        #endregion
    }
}