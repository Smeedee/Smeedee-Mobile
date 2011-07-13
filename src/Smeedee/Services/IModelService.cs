﻿using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface IModelService<T> where T : IModel
    {
        IEnumerable<T> Get(IDictionary<string, string> args);
        T GetSingle(IDictionary<string, string> args);
    }
}