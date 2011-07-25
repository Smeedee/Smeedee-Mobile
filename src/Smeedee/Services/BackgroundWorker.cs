using System;
using System.Threading;

namespace Smeedee
{
    public class BackgroundWorker : IBackgroundWorker
    {
        public void Invoke(Action unitOfWork)
        {
            ThreadPool.QueueUserWorkItem(t => unitOfWork());
        }
    }
}
