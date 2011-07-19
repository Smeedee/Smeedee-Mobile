using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.UnitTests.Fakes
{
    class NoBackgroundInvokation : IBackgroundWorker
    {
        public void Invoke(Action unitOfWork)
        {
            unitOfWork();
        }
    }
}
