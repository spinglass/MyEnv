using MyEnvCore.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Script
{
    public class ScriptDefinition
    {
        public CommandLineDefinition CommandLine
        {
            get { return m_CommandLine; }
        }

        public string Description
        {
            get { return m_Description; }
            set { m_Description = value; }
        }

        public string TargetUsage
        {
            get { return m_TargetUsage; }
            set { m_TargetUsage = value; }
        }

        public ScriptDefinition()
        {
            m_CommandLine = new CommandLineDefinition();
            m_Description = "No description given";
        }

        private CommandLineDefinition m_CommandLine;
        private string m_Description;
        private string m_TargetUsage;
    }
}
