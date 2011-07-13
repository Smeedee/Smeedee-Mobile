using System;

namespace Smeedee.Services
{
    public class NoBackgroundWorker : IBackgroundWorker
    {
        public void Invoke(Action unitOfWork)
        {
            unitOfWork();
        }
    }
}
