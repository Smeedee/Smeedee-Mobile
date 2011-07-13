using System;

namespace Smeedee
{
    public class NoBackgroundWorker : IBackgroundWorker
    {
        public void Invoke(Action unitOfWork)
        {
            unitOfWork();
        }
    }
}
