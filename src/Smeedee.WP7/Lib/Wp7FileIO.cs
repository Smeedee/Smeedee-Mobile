using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Smeedee.Lib;

namespace Smeedee.WP7.Lib
{
    public class Wp7FileIO : IFileIO
    {
        public byte[] ReadAllBytes(string fileName)
        {
            var wu = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();
            var file = wu.OpenFile(fileName, FileMode.OpenOrCreate);
            return file.ReadToEnd();
        }

        public void WriteAllBytes(string fileName, byte[] bytes)
        {
            var wu = System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForApplication();
            var file = wu.OpenFile(fileName, FileMode.Create);
            file.Write(bytes, 0, bytes.Length);
        }
    }
}
