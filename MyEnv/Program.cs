using MyEnvCore;
using MyEnvCore.Logging;
using MyEnvCore.Options;
using MyEnvCore.Script;
using MyEnvScripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyEnv
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CommandLine commandLine = new CommandLine();
                if (commandLine.Parse(args))
                {
                    // Get the assembly that provides the scripts.
                    Assembly assembly = System.Reflection.Assembly.Load("MyEnvScripts");

                    // Start-up the script factory.
                    ScriptFactory factory = new ScriptFactory();
                    factory.AddScripts(assembly);

                    // Look for the requested script.
                    IScript script = factory.Create(commandLine.Script);
                    if (script != null)
                    {
                        script.Run(commandLine.Options);
                    }
                    else
                    {
                        Log.Add("Error: Unknown script '{0}'", commandLine.Script);
                    }
                }
                else
                {
                    Log.Add("Error: Invalid command line. Try 'me help'.");
                }
            }
            catch (MyEnvException ex)
            {
                Log.Add("Error: " + ex.Message);
            }
        }
    }
}
