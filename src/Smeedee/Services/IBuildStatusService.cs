using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
    public interface IBuildStatusService
    {
        void Load(Action<IEnumerable<Build>> callback);
    }
}