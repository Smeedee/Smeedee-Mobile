using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Smeedee.Lib;

namespace Smeedee.Android.Lib
{
    public class MonoFileIO : IFileIO
    {
        public byte[] ReadAllBytes(string fileName)
        {
            return File.ReadAllBytes(fileName);
        }

        public void WriteAllBytes(string fileName, byte[] bytes)
        {
            File.WriteAllBytes(fileName, bytes);
        }
    }
}