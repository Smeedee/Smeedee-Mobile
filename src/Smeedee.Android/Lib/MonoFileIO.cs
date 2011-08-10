using System.IO;
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