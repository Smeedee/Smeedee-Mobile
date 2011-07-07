using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public interface IModelService<T> where T : IModel
    {
        IEnumerable<T> Get();
        T GetSingle();
    }
}
