using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using ScriptCs;
using ScriptCs.Contracts;

namespace ScriptCsWebHost.Controllers
{
    public class ScriptController : ApiController
    {
        private readonly ScriptServices _scriptcs;

        public ScriptController()
        {
            _scriptcs = new ScriptCsService().Root;
            _scriptcs.Executor.Initialize(new[] { "System.Web" }, Enumerable.Empty<IScriptPack>());
            _scriptcs.Executor.AddReferences(Assembly.GetExecutingAssembly());
        }

        public dynamic Post(Script script)
        {
            if (script == null || script.Code == null) throw new HttpResponseException(HttpStatusCode.BadRequest);

            if (script.Namespaces != null)
            {
                _scriptcs.Executor.ImportNamespaces(script.Namespaces);
            }

            if (script.Assemblies != null)
            {
                _scriptcs.Executor.AddReferences(script.Assemblies);
            }

            var result = _scriptcs.Executor.ExecuteScript(script.Code, script.Args);
            _scriptcs.Executor.Terminate();

            if (result.CompileExceptionInfo != null)
                return new {error = result.CompileExceptionInfo.SourceException.Message};
            if (result.ExecuteExceptionInfo != null)
                return new { error = result.ExecuteExceptionInfo.SourceException.Message};

            return result.ReturnValue;
        }
    }
}
