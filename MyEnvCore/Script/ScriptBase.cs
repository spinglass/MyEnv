using MyEnvCore.Logging;
using MyEnvCore.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Script
{
    public abstract class ScriptBase : IScript
    {
        public string Name
        {
            get
            {
                return GetType().Name.ToLower();
            }
        }

        public string Description
        {
            get { return m_Def.Description; }
        }

        public ScriptBase()
        {
            m_Def = new ScriptDefinition();
        }

        public void Initialise(ScriptFactory factory)
        {
            m_Factory = factory;

            Setup(m_Def);
        }

        public void DisplayHelp()
        {
            Log.Add("    {0} - {1}", Name, m_Def.Description);
            Log.Add("");
            if (m_Def.CommandLine.HasOptions)
            {
                Log.Add("    Usage: me {0} [options] {1}", Name, m_Def.TargetUsage);
            }
            else
            {
                Log.Add("    Usage: me {0} {1}", Name, m_Def.TargetUsage);
            }

            if (m_Def.CommandLine.HasOptions)
            {
                Log.Add("");
                Log.Add("    Options:");
                Log.Add("");
                m_Def.CommandLine.Display();
            }
        }

        public void Run(CommandLineOptions options)
        {
            // Add the special 'target' option for validation
            m_Def.CommandLine.AddOption("target");

            if (m_Def.CommandLine.Validate(options))
            {
                m_Options = options;
                DisplayOptions();
                Run();
            }
        }

        public abstract void Run();

        protected ScriptFactory Factory
        {
            get { return m_Factory; }
        }

        protected CommandLineOptions Options
        {
            get { return m_Options; }
        }

        protected abstract void Setup(ScriptDefinition def);

        private void DisplayOptions()
        {
            // Suppress option display for help script
            if (Name != "help")
            {
                Log.Add("======================================================================");
                Log.Add("MyEnv running script '{0}'", Name);
                if (m_Def.CommandLine.HasOptions)
                {
                    Log.Add("");
                    Log.Add("Options:");
                    foreach (var value in Options.Values)
                    {
                        Log.Add("    {0}={1}", value.Key, value.Value);
                    }
                    foreach (var flag in Options.Flags)
                    {
                        Log.Add("    {0}={1}", flag.Key, flag.Value);
                    }
                }
                Log.Add("======================================================================");
                Log.Add("");
            }
        }

        private ScriptFactory m_Factory;
        private ScriptDefinition m_Def;
        private CommandLineOptions m_Options;
    }
}
