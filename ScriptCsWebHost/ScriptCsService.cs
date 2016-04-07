using ScriptCs;
using ScriptCs.Contracts;
using ScriptCs.Engine.Roslyn;
using ScriptCs.Hosting;
using LogLevel = ScriptCs.Contracts.LogLevel;

namespace ScriptCsWebHost
{
    public class ScriptCsService
    {
        public ScriptServices Root { get; private set; }

        public ScriptCsService()
        {
            var scriptServicesBuilder = new ScriptServicesBuilder(new ScriptConsole(), new DefaultLogProvider()).
                LogLevel(LogLevel.Info).Cache(false).Repl(false).ScriptEngine<CSharpScriptEngine>();

            Root = scriptServicesBuilder.Build();
        }
    }
}