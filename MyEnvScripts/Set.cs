using MyEnvCore.Script;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvScripts
{
    [ScriptAttribute]
    public class Set : ScriptBase
    {
        public override void Run()
        {
        }

        protected override void Setup(ScriptDefinition def)
        {
            def.Description = "Set variables for the environment";
            def.TargetUsage = "<variable>=<value>";
        }
    }
}
