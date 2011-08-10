using System;

namespace Smeedee.UnitTests.Fakes
{
    class NoBackgroundInvocation : IBackgroundWorker
    {
        public void Invoke(Action unitOfWork)
        {
            unitOfWork();
        }
    }
}
