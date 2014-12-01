using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore.Logging
{
    public static class Log
    {
        public static void Add(string message)
        {
            Debug.WriteLine(message);
            Console.WriteLine(message);
        }

        public static void Add(string format, params object[] args)
        {
            string message = String.Format(format, args);
            Add(message);
        }
    }
}
