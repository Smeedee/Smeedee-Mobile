using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    public class ServiceLocator
    {
        private Object store;

        public void Bind<T>(T arg)
        {
            store = arg;
        }
    }
}
