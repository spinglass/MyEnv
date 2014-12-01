using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Options
{
    public class ValueDefinition
    {
        public string Name
        {
            get { return m_Name; }
        }

        public string DefaultValue
        {
            get
            {
                return (m_ValidValues != null) ? m_ValidValues[0] : String.Empty;
            }
        }

        public string[] ValidValues
        {
            get { return m_ValidValues; }
        }

        public ValueDefinition(string name)
        {
            m_Name = name.ToLower();
            m_ValidValues = null;
        }

        public ValueDefinition(string name, params string[] validValues)
        {
            m_Name = name.ToLower();
            m_ValidValues = validValues;

            for (int i = 0; i < m_ValidValues.Length; ++i)
            {
                m_ValidValues[i] = m_ValidValues[i].ToLower();
            }
        }

        public bool Validate(string value)
        {
            if (m_ValidValues != null)
            {
                return m_ValidValues.Contains(value.ToLower());
            }

            // Any value is valid
            return true;
        }

        private string m_Name;
        private string[] m_ValidValues;
    }
}
