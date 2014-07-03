using Common.Logging;
using ScriptCs;
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
            var logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            var scriptServicesBuilder = new ScriptServicesBuilder(new ScriptConsole(), logger).
                LogLevel(LogLevel.Info).Cache(false).Repl(false).ScriptEngine<RoslynScriptEngine>();

            Root = scriptServicesBuilder.Build();
        }
    }
}