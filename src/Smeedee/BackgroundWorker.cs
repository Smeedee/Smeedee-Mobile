using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Smeedee
{
    public class BackgroundWorker : IBackgroundWorker
    {
        public void Invoke(Action fn)
        {
            new Thread(() => fn()).Start();
        }
    }
}
