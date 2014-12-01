using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEnvCore
{
    public class MyEnvException : Exception
    {
        public MyEnvException(string message) :
            base(message)
        {
        }

        public MyEnvException(string format, params object[] args) :
            base(String.Format(format, args))
        {
        }
    }
}
