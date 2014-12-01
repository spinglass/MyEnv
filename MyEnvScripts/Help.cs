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
    public class Help : ScriptBase
    {
        public override void Run()
        {
            Log.Add("");

            string target = Options.Values["target"];
            if (String.IsNullOrEmpty(target))
            {
                // Top level help
                Log.Add("    MyEnv - Helper scripts for working in my environment.");
                Log.Add("");
                Log.Add("    me is the command line tool to run the scripts. Try:");
                Log.Add("");
                Log.Add("        me help scripts      List available scripts.");
                Log.Add("        me help <script>     Help on a specific script.");
            }
            else
            {
                if (target == "scripts")
                {
                    // List all available scripts
                    Log.Add("    Available scripts:");
                    Log.Add("");

                    List<string> names = Factory.AvailableScripts.ToList();
                    names.Sort();
                    foreach (string name in names)
                    {
                        IScript script = Factory.Create(name);
                        Log.Add("        {0} - {1}", script.Name, script.Description);
                    }
                }
                else
                {
                    // Help on a specific script
                    IScript script = Factory.Create(target);
                    if (script != null)
                    {
                        script.DisplayHelp();
                    }
                    else
                    {
                        Log.Add("Error: Unknown script '{0}'", target);
                    }
                }
            }
        }

        protected override void Setup(ScriptDefinition def)
        {
            def.Description = "The MyEnv help system";
            def.TargetUsage = "<topic>";
        }
    }
}
