using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
