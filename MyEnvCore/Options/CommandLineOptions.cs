using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Options
{
    public class CommandLineOptions
    {
        public IReadOnlyDictionary<string, bool> Flags
        {
            get { return m_Flags; }
        }

        public IReadOnlyDictionary<string, string> Values
        {
            get { return m_Values; }
        }

        public CommandLineOptions()
        {
            m_Flags = new Dictionary<string, bool>();
            m_Values = new Dictionary<string, string>();
        }

        public void Add(string name, bool value)
        {
            DuplicateCheck(name);

            m_Flags[name] = value;
        }

        public void Add(string name, string value)
        {
            DuplicateCheck(name);

            m_Values[name] = value;
        }

        private void DuplicateCheck(string name)
        {
            if (m_Flags.ContainsKey(name) || m_Values.ContainsKey(name))
            {
                throw new MyEnvException("Trying to add duplicate option '{0}'", name);
            }
        }

        private Dictionary<string, bool> m_Flags;
        private Dictionary<string, string> m_Values;
    }
}
