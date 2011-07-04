using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Smeedee
{
    public class BackgroundWorker : IBackgroundWorker
    {
        public void Invoke(Action unitOfWork)
        {
            new Thread(() => unitOfWork()).Start();
        }
    }
}
