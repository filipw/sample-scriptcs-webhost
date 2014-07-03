namespace ScriptCsWebHost.Controllers
{
    public class Script
    {
        public string Code { get; set; }
        public string[] Args { get; set; }
        public string[] Namespaces { get; set; }
        public string[] Assemblies { get; set; }
    }
}