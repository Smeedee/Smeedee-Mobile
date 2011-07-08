using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Utilities
{
    public static class Guard
    {
        public static void NotNull(params object[] args)
        {
            if (args.Any(arg => arg == null))
                throw new ArgumentNullException();
        }
    }
}
