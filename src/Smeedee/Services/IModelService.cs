using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface IModelService<T> where T : IModel
    {
        T Get();
        T Get(IDictionary<string, string> args);
    }
}
