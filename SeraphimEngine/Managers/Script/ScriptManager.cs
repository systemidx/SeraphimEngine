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

namespace SeraphimEngine.Managers.Script {
    public class ScriptManager : Manager<ScriptManager>, IScriptManager {
        public override bool IsInitialized { get; protected set; }

        private readonly HashSet<MetadataReference> _assemblies = new HashSet<MetadataReference>();

        private readonly IDictionary<string, IScript> _scripts =
            new Dictionary<string, IScript>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        ///     Initializes a new instance of the <see cref="ScriptManager" /> class.
        /// </summary>
        public ScriptManager() {
            SetAssemblies();
        }
        
        /// <summary>
        ///     Starts the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <exception cref="ScriptManagerInitializationException"></exception>
        public void StartScript(string scriptId) {
            if (!IsInitialized)
                throw new ScriptManagerInitializationException();

            if (!_scripts.ContainsKey(scriptId))
                _scripts.Add(scriptId, CreateScript(scriptId));

            Task.Run(() => _scripts[scriptId].Start());
        }

        /// <summary>
        ///     Stops the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        public void StopScript(string scriptId) {
            if (!_scripts.ContainsKey(scriptId))
                return;
            
            _scripts[scriptId].Stop();
        }

        /// <summary>
        /// Initializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="graphics">The graphics.</param>
        public override void Initialize(ContentManager content, GraphicsDevice graphics) {
            IsInitialized = true;
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime) {
            foreach (var script in _scripts.Where(x => x.Value.IsRunning))
                script.Value.Update(gameTime);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime) {
            foreach (var script in _scripts.Where(x => x.Value.IsRunning))
                script.Value.Draw(gameTime);
        }

        /// <summary>
        ///     Sets the assemblies.
        /// </summary>
        /// <returns>AssemblyReferences.</returns>
        private void SetAssemblies() {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _assemblies.Add(MetadataReference.CreateFromFile(typeof (object).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(typeof (Enumerable).Assembly.Location));
            _assemblies.Add(MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\SeraphimEngine.dll").Location));
            _assemblies.Add(
                MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\MonoGame.Extended.dll").Location));
            _assemblies.Add(
                MetadataReference.CreateFromFile(Assembly.LoadFile($"{path}\\MonoGame.Framework.dll").Location));
        }
        
        /// <summary>
        ///     Creates the script.
        /// </summary>
        /// <param name="scriptId">The script identifier.</param>
        /// <returns>CScript.</returns>
        /// <exception cref="SeraphimEngine.Exceptions.AssetManagerInitializationException"></exception>
        private ISceneScript CreateScript(string scriptId) {
            if (!AssetManager.Instance.IsInitialized)
                throw new AssetManagerInitializationException();

            SeraphimScript script = AssetManager.Instance.GetAsset<SeraphimScript>(scriptId);
            SyntaxTree[] syntaxTree = {CSharpSyntaxTree.ParseText(script.Code)};
            CSharpCompilation compilation = CSharpCompilation.Create(Path.GetRandomFileName(), syntaxTree, _assemblies,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (MemoryStream stream = new MemoryStream()) {
                EmitResult result = compilation.Emit(stream);

                if (!result.Success) {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

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
    }
}