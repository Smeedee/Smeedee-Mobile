﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee
{
    public static class Csv
    {
        private const char lineSeparator = '\a'; //unprintable character alert
        private const char columnSeparator = '\f'; //unprintable character form feed

        public static IEnumerable<string[]> FromCsv(string str)
        {
            return str.Split(lineSeparator).Select(s => s.Split(columnSeparator));
        }

        public static string ToCsv(IEnumerable<string[]> csv)
        {
            var rows = csv.Select(s => String.Join(columnSeparator.ToString(), s));
            return String.Join(lineSeparator.ToString(), rows);
        }
    }
}