using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.ContentPipeline.ContentObjects;
using SeraphimEngine.Exceptions;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Scene;
using SeraphimEngine.Script;

namespace SeraphimEngine.Managers.Script
{
    /// <summary>
    /// Class ScriptManager.
    /// </summary>
    public class ScriptManager : Manager<ScriptManager>, IScriptManager
    {
        #region Override Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is initialized.
        /// </summary>
        /// <value><c>true</c> if this instance is initialized; otherwise, <c>false</c>.</value>
        public override bool IsInitialized { get; protected set; }

        #endregion

        #region Read Only Member Variables

        /// <summary>
        /// The scripts dictionary. This holds references to scripts which are already registered, compiled, and cached.
        /// </summary>
        private readonly IDictionary<string, IScript> _scripts =
            new Dictionary<string, IScript>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// The script paths. This is a temporary measure to ensure correct pathing for loading scripts and for simplicity when registering them.
        /// </summary>
        //todo: Make this configuration based.
        private readonly IDictionary<ScriptType, string> _scriptPaths = new Dictionary<ScriptType, string>
        {
            {ScriptType.Normal, "scripts/"},
            {ScriptType.Scene, "scripts/scene/"},
        };

        /// <summary>
        /// The assembly reference collection. This collection holds all of the metadata references used by the Roslyn compiler (which we are
        /// choosing to expose)
        /// </summary>
        private readonly HashSet<MetadataReference> _assemblies = new HashSet<MetadataReference>();

        /// <summary>
        /// The  options which are used to compile the script code.
        /// </summary>
        private readonly CSharpCompilationOptions _compilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary);

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScriptManager" /> class.
        /// </summary>
        public ScriptManager()
        {
            SetAssemblies();
        }

        #endregion

        #region Exposed Script Control Methods

        /// <summary>
        /// Starts the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="scriptType">Type of the script.</param>
        /// <exception cref="ScriptManagerInitializationException"></exception>
        public void StartScript(string scriptId, ScriptType scriptType = ScriptType.Normal)
        {
            if (!IsInitialized)
                throw new ScriptManagerInitializationException();

            string key = $"{_scriptPaths[scriptType]}{scriptId}";

            if (!_scripts.ContainsKey(key))
                _scripts.Add(key, CreateScript(key));

            Task.Run(() => _scripts[key].Start());
        }

        /// <summary>
        /// Stops the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <param name="scriptType">Type of the script.</param>
        public void StopScript(string scriptId, ScriptType scriptType = ScriptType.Normal)
        {
            string key = $"{_scriptPaths[scriptType]}{scriptId}";

            if (!_scripts.ContainsKey(key))
                return;

            _scripts[key].Stop();
        }

        #endregion

        #region Exposed Game Flow Methods

        /// <summary>
        /// Initializes the script manager.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics)
        {
            IsInitialized = true;
        }

        /// <summary>
        /// Sends the update command to all active scripts
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            foreach (var script in _scripts.Where(x => x.Value.IsRunning))
                script.Value.Update(gameTime);
        }

        /// <summary>
        /// Sends the draw command to all active scripts
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            foreach (var script in _scripts.Where(x => x.Value.IsRunning))
                script.Value.Draw(gameTime);
        }

        #endregion

        #region Compilation Methods

        /// <summary>
        ///     Sets the assemblies.
        /// </summary>
        /// <returns>AssemblyReferences.</returns>
        private void SetAssemblies()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _assemblies.Add(MetadataReference.CreateFromFile(typeof (object).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(typeof (Enumerable).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\SeraphimEngine.dll").Location));
            _assemblies.Add(MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\MonoGame.Extended.dll").Location));
            _assemblies.Add(MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\MonoGame.Framework.dll").Location));
        }

        /// <summary>
        ///     Creates the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <returns>CScript.</returns>
        /// <exception cref="SeraphimEngine.Exceptions.AssetManagerInitializationException"></exception>
        private ISceneScript CreateScript(string scriptId)
        {
            if (!AssetManager.Instance.IsInitialized)
                throw new AssetManagerInitializationException();

            SeraphimScript script = AssetManager.Instance.GetAsset<SeraphimScript>(scriptId);
            SyntaxTree[] syntaxTree = {CSharpSyntaxTree.ParseText(script.Code)};
            CSharpCompilation compilation = CSharpCompilation.Create(Path.GetRandomFileName(), syntaxTree, _assemblies, _compilationOptions);

            using (MemoryStream stream = new MemoryStream())
            {
                EmitResult result = compilation.Emit(stream);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    //todo: Figure out a better way to log these failures.
                    foreach (Diagnostic diagnostic in failures)
                        Console.Error.WriteLine("{0}: {1}", diagnostic.Id, diagnostic.GetMessage());

                    throw new ScriptCodeException();
                }

                stream.Seek(0, SeekOrigin.Begin);
                Assembly assembly = Assembly.Load(stream.ToArray());

                foreach (
                    Type type in
                        assembly.GetTypes()
                            .Where(
                                myType =>
                                    myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof (SceneScript))))
                    return (ISceneScript) Activator.CreateInstance(type);
            }

            throw new ScriptCodeException();
        }

        #endregion
    }
}