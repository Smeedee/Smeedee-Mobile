using System;
using System.Linq;

namespace Smeedee
{
    public static class Guard
    {
        public static void NotNull(params object[] args)
        {
            if (args.Any(arg => arg == null))
                throw new ArgumentNullException();
        }

        public static void NotNullOrEmpty(params string[] args)
        {
            if (args.Any(string.IsNullOrEmpty))
                throw new ArgumentException("Argument cannot be null or empty");
        }
    }
}