using MyEnvCore.Logging;
using MyEnvCore.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvScripts
{
    [ScriptAttribute]
    public class Find : ScriptBase
    {
        public override void Run()
        {
            bool quiet = Options.Flags["quiet"];
            string type = Options.Values["type"];
            string target = Options.Values["target"];

            if (!quiet)
            {
                Log.Add("Searching for {0} files in '{1}'", type, target);
            }
        }

        protected override void Setup(ScriptDefinition def)
        {
            def.Description = "Find something in the environment";
            def.TargetUsage = "<path>";
            def.CommandLine.AddFlag("quiet");
            def.CommandLine.AddOption("type", "mp3", "wav");
        }
    }
}
