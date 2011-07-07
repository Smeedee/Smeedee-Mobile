using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    public interface IModelService
    {
        IEnumerable<T> Get<T>();
        T GetSingle<T>();
    }
}
