﻿using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
    public interface ITopCommittersService
    {
        void LoadTopCommiters(TimePeriod time, Action<IEnumerable<Committer>> callback);
    }
}
