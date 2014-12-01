using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Script
{
    public class ScriptFactory
    {
        public IEnumerable<string> AvailableScripts
        {
            get { return m_ScriptTypes.Keys; }
        }

        public ScriptFactory()
        {
            m_ScriptTypes = new Dictionary<string, Type>();
        }

        public void AddScripts(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.CustomAttributes.FirstOrDefault(x => x.AttributeType == typeof(ScriptAttribute)) != null)
                {
                    m_ScriptTypes[type.Name.ToLower()] = type;
                }
            }
        }

        public IScript Create(string name)
        {
            ScriptBase script = null;
            Type type;
            if (m_ScriptTypes.TryGetValue(name, out type))
            {
                script = (ScriptBase)Activator.CreateInstance(type);
                script.Initialise(this);
            }
            return script;
        }

        private Dictionary<string, Type> m_ScriptTypes;
    }
}
