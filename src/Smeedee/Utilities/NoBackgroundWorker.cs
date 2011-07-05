using System;

namespace Smeedee.Utilities
{
    public class NoBackgroundWorker : IBackgroundWorker
    {
        public void Invoke(Action unitOfWork)
        {
            unitOfWork();
        }
    }
}
