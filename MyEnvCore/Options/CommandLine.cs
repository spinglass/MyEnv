using MyEnvCore.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Options
{
    public class CommandLine
    {
        public string Script
        {
            get { return m_Script; }
        }

        public CommandLineOptions Options
        {
            get { return m_Options; }
        }

        public CommandLine()
        {
            m_Options = new CommandLineOptions();
        }

        public bool Parse(string[] args)
        {
            if (args.Length > 0)
            {
                // Script comes first
                m_Script = args[0].ToLower();

                for (int i = 1; i < args.Length; ++i)
                {
                    string arg = args[i].ToLower();
                    if (arg[0] == '/')
                    {
                        int splitIndex = arg.IndexOf(':');
                        if (splitIndex == -1)
                        {
                            // Add flag
                            string name = arg.Substring(1);
                            m_Options.Add(name, true);
                        }
                        else
                        {
                            // Add value
                            string name = arg.Substring(1, splitIndex - 1);
                            string value = arg.Substring(splitIndex + 1);
                            m_Options.Add(name, value);
                        }
                    }
                    else
                    {
                        // Concatenate the remaining args into the special 'target' option.
                        string value = String.Join("", args.Skip(i));
                        m_Options.Add("target", value);
                        break;
                    }
                }

                return true;
            }

            return false;
        }

        private string m_Script;
        private CommandLineOptions m_Options;
    }
}
