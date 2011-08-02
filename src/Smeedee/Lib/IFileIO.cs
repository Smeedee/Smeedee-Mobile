using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Lib
{
    interface IFileIO
    {
        byte[] ReadAllBytes(string fileName);
        void WriteAllBytes(string fileName, byte[] bytes);
    }
}
