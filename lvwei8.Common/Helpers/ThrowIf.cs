using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lvwei8.Common.Helpers
{
    public static class ThrowIf
    {
        public static class Argument
        {
            public static void IsNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }
            public static void IsNullOrWhiteSpace(string argument, string argumentName)
            {
                if (String.IsNullOrWhiteSpace(argument))
                {
                    throw new ArgumentNullException(argumentName);
                }
            }
        }
    }
}
