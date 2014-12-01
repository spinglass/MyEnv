using MyEnvCore.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Script
{
    public interface IScript
    {
        string Name { get; }
        string Description { get; }

        void DisplayHelp();
        void Run(CommandLineOptions options);
    }
}
