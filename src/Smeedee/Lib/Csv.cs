using System;
using System.Collections.Generic;
using System.Linq;

namespace Smeedee.Lib
{
    public static class Csv
    {
        private const char LineSeparator = '\a'; //unprintable character alert
        private const char ColumnSeparator = '\f'; //unprintable character form feed
        
        public static IEnumerable<string[]> FromCsv(string str)
        {
            return str.Split(LineSeparator).Select(s => s.Split(ColumnSeparator));
        }

        public static string ToCsv(IEnumerable<string[]> csv)
        {
            var rows = csv.Select(row => String.Join(ColumnSeparator.ToString(), row.Select(StripSpecialChars).ToArray()));
            return String.Join(LineSeparator.ToString(), rows.ToArray());
        }

        private static string StripSpecialChars(string str)
        {
            return str.Replace(ColumnSeparator.ToString(), "").Replace(LineSeparator.ToString(), "");
        }
    }
}
