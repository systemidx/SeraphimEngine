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
using SeraphimEngine.ContentPipeline.ContentObjects;
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
        /// The  options which are used to compile the scriptContent code.
        /// </summary>
        private readonly CSharpCompilationOptions _compilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

        private readonly IScriptCacher _cacher = new ScriptCacher();

        #endregion

        #region Variables

        /// <summary>
        /// The scripts list. This holds references to scripts which are already registered, compiled, and cached.
        /// </summary>
        private readonly IList<IScript> _scripts = new List<IScript>();

        /// <summary>
        /// The scripts that are serialized from the Content
        /// </summary>
        private SeraphimScriptContent[] _scriptContentContent;

        #endregion

        #region Exposed SeraphimScript Control Methods

        /// <summary>
        /// Starts the scriptContent.
        /// </summary>
        /// <param name="script">The scriptContent.</param>
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
            {
                Console.Write(".");
                Thread.Sleep(10);
            }
            Console.WriteLine();

            //Recurse and try again
            StartScript(script, runOnce);
        }

        /// <summary>
        /// Stops the scriptContent.
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
        /// Determines whether the specified scriptContent identifier is running.
        /// </summary>
        /// <param name="script">The script.</param>
        /// <returns><c>true</c> if the specified scriptContent identifier is running; otherwise, <c>false</c>.</returns>
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
            foreach (var script in _scripts.Where(x => x.IsRunning))
                script.Update(gameTime);
        }

        /// <summary>
        /// Sends the draw command to all active scripts
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
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
        /// Creates the scriptContent.
        /// </summary>
        /// <param name="scriptContent">The scriptContent.</param>
        /// <returns>CScript.</returns>
        /// <exception cref="AssetManagerInitializationException"></exception>
        /// <exception cref="ScriptCodeException">
        /// </exception>
        /// <exception cref="SeraphimEngine.Exceptions.AssetManagerInitializationException"></exception>
        private IScript CompileScript([NotNull] SeraphimScriptContent scriptContent)
        {
            if (!AssetManager.Instance.IsInitialized)
                throw new AssetManagerInitializationException();

            //Try to get cached item
            byte[] scriptBytes = _cacher.GetCachedScriptBytes(scriptContent.Id);
            if (scriptBytes != null)
                return CreateScriptFromSource(scriptBytes);

            //Build the syntax tree
            SyntaxTree[] syntaxTree =
            {
                CSharpSyntaxTree.ParseText(scriptContent.Code)
            };

            //Compile the tree
            CSharpCompilation compilation = CSharpCompilation.Create(scriptContent.Id, syntaxTree, _assemblies, _compilationOptions);

            //Stream the results to the internal assembly and create an instance
            using (MemoryStream stream = new MemoryStream())
            {
                //Get the result
                EmitResult result = compilation.Emit(stream);

                //Create an instance and return it
                if (result.Success)
                {
                    stream.Seek(0, SeekOrigin.Begin);

                    _cacher.AddScriptToCache(scriptContent.Id, stream, out scriptBytes);
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

        private IScript CreateScriptFromSource(byte[] bytes)
        {
            Assembly asm = Assembly.Load(bytes);
            Type type =
                asm.GetTypes()
                    .First(
                        myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof (SeraphimScript)));

            return (IScript) Activator.CreateInstance(type);
        }

        private readonly IDictionary<string, Task> _compilationTasks = new Dictionary<string, Task>();

        /// <summary>
        /// Preloads the scripts.
        /// </summary>
        private void PreloadScripts()
        {
            _scriptContentContent = AssetManager.Instance.GetAllAssets<SeraphimScriptContent>(CONTENT_DIRECTORY);

            foreach (SeraphimScriptContent scriptContent in _scriptContentContent)
                _compilationTasks.Add(scriptContent.Id, GetCompileTask(scriptContent));

            foreach (Task task in _compilationTasks.Values)
                task.Start();
        }

        /// <summary>
        /// Gets the compile task.
        /// </summary>
        /// <param name="scriptContent">Content of the script.</param>
        /// <returns>Task.</returns>
        private Task GetCompileTask([NotNull] SeraphimScriptContent scriptContent)
        {
            return new Task(() => _scripts.Add(CompileScript(scriptContent)));
        }

        #endregion
    }
}