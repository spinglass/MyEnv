using MyEnvCore.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Options
{
    public class CommandLineDefinition
    {
        public bool HasOptions
        {
            get
            {
                return (m_Flags.Count > 0 || m_ValueDefinitions.Count > 0);
            }
        }

        public CommandLineDefinition()
        {
            m_Flags = new List<string>();
            m_ValueDefinitions = new Dictionary<string, ValueDefinition>();
        }

        public void Display()
        {
            // Flags
            foreach (string name in m_Flags)
            {
                Log.Add("        /" + name);
            }

            // Values
            foreach (ValueDefinition def in m_ValueDefinitions.Values)
            {
                // Don't display special 'target' option, it has its own usage
                if (def.Name != "target")
                {
                    string option = "        /" + def.Name + ":";
                    if (string.IsNullOrEmpty(def.DefaultValue))
                    {
                        option += "<" + def.Name + ">";
                    }
                    else
                    {
                        option += string.Format("{{{0}}} (default '{1}')", string.Join("|", def.ValidValues), def.DefaultValue);
                    }
                    Log.Add(option);
                }
            }
        }

        public void AddFlag(string name)
        {
            m_Flags.Add(name);
        }

        public void AddOption(string name)
        {
            m_ValueDefinitions[name] = new ValueDefinition(name);
        }

        public void AddOption(string name, params string[] validValues)
        {
            m_ValueDefinitions[name] = new ValueDefinition(name, validValues);
        }

        public bool Validate(CommandLineOptions options)
        {
            // Check for extra flag options
            foreach (string name in options.Flags.Keys)
            {
                if (!m_Flags.Contains(name))
                {
                    Log.Add("Error: Unrecognised option '/{0}'", name);
                    return false;
                }
            }

            // Check values
            foreach (var pair in options.Values)
            {
                // Check for extra and invalid value options
                ValueDefinition def;
                if (m_ValueDefinitions.TryGetValue(pair.Key, out def))
                {
                    if (!def.Validate(pair.Value))
                    {
                        Log.Add("Error: Invalid value '{0}' for option '/{1}'", pair.Value, pair.Key);
                        return false;
                    }
                }
                else
                {
                    Log.Add("Error: Unrecognised option '/{0}'", pair.Key);
                    return false;
                }

                // Check for valid value
            }

            // Add missing flags, defaulting them to false
            foreach (string name in m_Flags)
            {
                if (!options.Flags.ContainsKey(name))
                {
                    options.Add(name, false);
                }
            }

            // Add missing values
            foreach (ValueDefinition def in m_ValueDefinitions.Values)
            {
                if (!options.Values.ContainsKey(def.Name))
                {
                    options.Add(def.Name, def.DefaultValue);
                }
            }

            return true;
        }

        private Dictionary<string, ValueDefinition> m_ValueDefinitions;
        private List<string> m_Flags;
    }
}
